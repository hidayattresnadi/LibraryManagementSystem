﻿using LibrarySystem.Application.DTO.WorkflowDTO;
using LibrarySystem.Application.DTO.WorkflowSequence;
using LibrarySystem.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowSequenceController : ControllerBase
    {
        private readonly IWorkflowSequenceService _workflowSequenceService;
        public WorkflowSequenceController(IWorkflowSequenceService workflowSequenceService)
        {
            _workflowSequenceService = workflowSequenceService;
        }
        [HttpPost]
        public async Task<IActionResult> AddingWorkflowSequence([FromBody] WorkflowSequenceAdd workflowSequenceAdd)
        {
            string message = "success created flow sequence";
            var isAddWorkflow = await _workflowSequenceService.AddWorkflowSequence(workflowSequenceAdd);
            if (isAddWorkflow != true)
            {
                return BadRequest("Sorry, the process failed");

            }
            return Ok(message);
        }
    }
}
