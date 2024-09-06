using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Infrastructure.Context;
using LibrarySystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyDbContext>(options => options.UseNpgsql(connection));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
            services.AddScoped<IWorkflowSequenceRepository, WorkflowSequenceRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IWorkflowActionRepository, WorkflowActionRepository>();
            services.AddScoped<IProcessRepository, ProcessRepository>();
            services.AddScoped<INextStepRulesRepository, NextStepRulesRepository>();
            services.AddScoped<IBookRequestRepository, BookRequestRepository>();
        }
    }
}
