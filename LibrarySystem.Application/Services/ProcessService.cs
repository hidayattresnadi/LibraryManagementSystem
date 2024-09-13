using LibrarySystem.Application.DTO;
using LibrarySystem.Domain.DTO.ProcessDTO;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Mail;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Application.Roles;
using LibrarySystem.Domain.DTO.Dashboard;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Services
{
    public class ProcessService : IProcessService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWorkflowActionRepository _workflowActionRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IProcessRepository _processRepository;
        private readonly IWorkflowSequenceRepository _workflowSequenceRepository;
        private readonly INextStepRulesRepository _nextStepRulesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookRequestRepository _bookRequestRepository;
        public ProcessService(IRequestRepository requestRepository, IProcessRepository processRepository,
                           IWorkflowActionRepository workflowActionRepository, IHttpContextAccessor httpContextAccessor, IWorkflowSequenceRepository workflowSequenceRepository,
                           RoleManager<IdentityRole> roleManager, INextStepRulesRepository nextStepRulesRepository, IEmailService emailService, UserManager<AppUser> userManager,
                           IBookRequestRepository bookRequestRepository)
        {
            _requestRepository = requestRepository;
            _processRepository = processRepository;
            _workflowActionRepository = workflowActionRepository;
            _httpContextAccessor = httpContextAccessor;
            _workflowSequenceRepository = workflowSequenceRepository;
            _roleManager = roleManager;
            _nextStepRulesRepository = nextStepRulesRepository;
            _emailService = emailService;
            _userManager = userManager;
            _bookRequestRepository = bookRequestRepository;
        }
        private string ReplacePlaceholders(string template,BookRequest request, string name, string comment)
        {
            template = template.Replace("{{Name}}", name);
            template = template.Replace("{{Title}}", request.Title);
            template = template.Replace("{{Author}}", request.Author);
            template = template.Replace("{{Publisher}}", request.Publisher);
            template = template.Replace("{{ISBN}}", request.ISBN);
            template = template.Replace("{{Comment}}", comment);
            return template;
        }
        public async Task<Response> ReviewRequest(int processId, RequestApproval requestApproval)
        {
            var userRoleIds = _httpContextAccessor.HttpContext?.User?.Claims
                .Where(c => c.Type == "RoleId")
                .Select(c => c.Value)
                .ToList();
            var process = await _processRepository.GetFirstOrDefaultAsync(p => p.ProcessId == processId);
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var workflowSequence = await _workflowSequenceRepository.GetFirstOrDefaultAsync(wfs => wfs.StepId == process.CurrentStepId);
            bool isValidRole = userRoleIds.Contains(workflowSequence.RequiredRole);
            var role = await _roleManager.FindByIdAsync(workflowSequence.RequiredRole);
            if (!isValidRole)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Role is not valid"
                };
            }
            if (requestApproval.RequestStatus == "Request Approved" && role.Name == Roles.Roles.Role_Librarian)
            {
                var foundLibraryUser = await _userManager.FindByIdAsync(process.RequesterId);
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                var firstUser = usersInRole.FirstOrDefault();
                var nextRoleEmail = firstUser.Email;
                var nextStepId = await _nextStepRulesRepository.GetFirstOrDefaultAsync(n => n.CurrentStepId == process.CurrentStepId && n.ConditionValue == "Approved");
                process.CurrentStepId = nextStepId.NextStepId;
                process.Status = "Pending Library Manager Review";
                await _processRepository.SaveAsync();

                var newWorkflowAction = new WorkflowAction
                {
                    ProcessId = process.ProcessId,
                    StepId = nextStepId.CurrentStepId,
                    ActorId = userId,
                    Action = "Request Approved",
                    ActionDate = DateTime.UtcNow,
                    Comments = "Request approved from librarian, now pending Library Manager Review"
                };
                await _workflowActionRepository.AddAsync(newWorkflowAction);
                await _workflowActionRepository.SaveAsync();

                var bookRequest = await _bookRequestRepository.GetFirstOrDefaultAsync(b => b.ProcessId == process.ProcessId);
                var htmlTemplate = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/ApprovedBookRequestLibrarian.html");
                htmlTemplate = ReplacePlaceholders(htmlTemplate, bookRequest,foundLibraryUser.UserName, requestApproval.Comment);

                var mailData = new MailData
                {
                    EmailToName = foundLibraryUser.UserName,
                    EmailSubject = "Leave request is approved by Librarian",
                };

                mailData.EmailToIds.Add(nextRoleEmail);
                mailData.EmailToIds.Add(foundLibraryUser.Email);
                mailData.EmailBody = htmlTemplate;

                var emailResult = _emailService.SendEmailAsync(mailData);

                var workflowSequenceNext = await _workflowSequenceRepository.GetFirstOrDefaultAsync(wfs => wfs.StepId == nextStepId.NextStepId);
                var nextRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == workflowSequenceNext.RequiredRole);
                var usersInRoleNext = await _userManager.GetUsersInRoleAsync(nextRole.Name);
                var hrUser = usersInRoleNext.FirstOrDefault();

                var htmlTemplateHRManager = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/RequestApprovedToLibraryManager.html");
                htmlTemplateHRManager = ReplacePlaceholders(htmlTemplateHRManager, bookRequest, hrUser.UserName, requestApproval.Comment);

                var mailDataHRManager = new MailData
                {
                    EmailToName = hrUser.UserName,
                    EmailSubject = "Book request is approved by Librarian",
                };

                mailDataHRManager.EmailToIds.Add(hrUser.Email);
                mailDataHRManager.EmailBody = htmlTemplateHRManager;
                _emailService.SendEmailAsync(mailDataHRManager);
            }
            else if (requestApproval.RequestStatus == "Request Approved" && role.Name == Roles.Roles.Role_Library_Manager)
            {
                var nextStepId = await _nextStepRulesRepository.GetFirstOrDefaultAsync(n => n.CurrentStepId == process.CurrentStepId && n.ConditionValue == "Approved");
                var newWorkflowAction = new WorkflowAction
                {
                    ProcessId = process.ProcessId,
                    StepId = process.CurrentStepId,
                    ActorId = process.RequesterId,
                    Action = "Request Approved",
                    ActionDate = DateTime.UtcNow,
                    Comments = "Request approved from library Manager"
                };
                await _workflowActionRepository.AddAsync(newWorkflowAction);
                await _workflowActionRepository.SaveAsync();

                process.CurrentStepId = nextStepId.NextStepId;
                process.Status = "Accepted";
                await _processRepository.SaveAsync();

                var newWorkflowActionAccepted = new WorkflowAction
                {
                    ProcessId = process.ProcessId,
                    StepId = process.CurrentStepId,
                    ActorId = process.RequesterId,
                    Action = "Request Approved",
                    ActionDate = DateTime.UtcNow,
                    Comments = "Request Approved"
                };
                await _workflowActionRepository.AddAsync(newWorkflowActionAccepted);
                await _workflowActionRepository.SaveAsync();

                var request = await _requestRepository.GetFirstOrDefaultAsync(r => r.RequestId == process.RequestId);
                request.EndDate = DateTime.UtcNow;
                await _requestRepository.SaveAsync();
            }
            else if (requestApproval.RequestStatus == "Request Rejected")
            {
                var nextStepId = await _nextStepRulesRepository.GetFirstOrDefaultAsync(n => n.CurrentStepId == process.CurrentStepId && n.ConditionValue == "Rejected");
                var newWorkflowActionRejected = new WorkflowAction
                {
                    ProcessId = process.ProcessId,
                    StepId = process.CurrentStepId,
                    ActorId = process.RequesterId,
                    Action = "Request Rejected",
                    ActionDate = DateTime.UtcNow,
                    Comments = "Request rejected"
                };

                await _workflowActionRepository.AddAsync(newWorkflowActionRejected);
                await _workflowActionRepository.SaveAsync();

                process.Status = "Rejected";
                process.CurrentStepId = nextStepId.NextStepId;
                await _processRepository.SaveAsync();

                var newWorkflowAction = new WorkflowAction
                {
                    ProcessId = process.ProcessId,
                    StepId = process.CurrentStepId,
                    ActorId = process.RequesterId,
                    Action = "Request Rejected",
                    ActionDate = DateTime.UtcNow,
                    Comments = "Request rejected"
                };
                await _workflowActionRepository.AddAsync(newWorkflowAction);
                await _workflowActionRepository.SaveAsync();

                var request = await _requestRepository.GetFirstOrDefaultAsync(r => r.RequestId == process.RequestId);
                request.EndDate = DateTime.UtcNow;
                await _requestRepository.SaveAsync();

            }
            return new Response
            {
                Status = "Success",
                Message = "Process review result is out"
            };
        }
        public async Task<IEnumerable<ProcessDetailDTO>> GetProcessCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var processUsers = await _processRepository.GetAllAsync(p => p.RequesterId == userId, "Requester,WorkflowSequence,Workflow");
            var processUsersDTO = processUsers.Select(c => new ProcessDetailDTO
            {
                ProcessId = c.ProcessId,
                WorkflowName = c.Workflow.WorkflowName,
                Requester = c.Requester.UserName,
                RequestDate = c.RequestDate,
                Status = c.Status,
                CurrentStep = c.WorkflowSequence.StepName
            }).ToList();
            return processUsersDTO;
        }
    }
}
