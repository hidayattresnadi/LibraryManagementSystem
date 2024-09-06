using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Context
{
    public partial class MyDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<WorkflowSequence> WorkflowSequences { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<NextStepRules> NextStepsRules { get; set; }
        public DbSet<BookRequest> BookRequests { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("book_pkey");

                entity.ToTable("book");

                entity.HasIndex(e => e.ISBN, "book_isbn_key").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id_book");
                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasColumnName("author");
                entity.Property(e => e.AvailableBooks)
                    .HasDefaultValue(0)
                    .HasColumnName("availablebook");
                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ISBN)
                    .HasMaxLength(20)
                    .HasColumnName("isbn");
                entity.Property(e => e.LibraryLocation)
                    .HasMaxLength(255)
                    .HasColumnName("location");
                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");
                entity.Property(e => e.Publisher)
                    .HasMaxLength(255)
                    .HasColumnName("publisher");
                entity.Property(e => e.PurchaseDate).HasColumnName("purchasedate");
                entity.Property(e => e.DeleteReasoning).HasColumnName("reason");
                entity.Property(e => e.DeleteStamp)
                    .HasColumnName("delete_stamp");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
                entity.Property(e => e.Language)
                    .HasMaxLength(100)
                    .HasColumnName("language");
            });

            modelBuilder.Entity<Borrowing>(entity =>
            {
                entity.HasKey(e => e.BorrowingId).HasName("borrow_pkey");

                entity.ToTable("borrow");

                entity.Property(e => e.BorrowingId).HasColumnName("id_borrow");
                entity.Property(e => e.BorrowedDate).HasColumnName("date_borrow");
                entity.Property(e => e.ReturnDate).HasColumnName("date_return");
                entity.Property(e => e.BookId).HasColumnName("id_book");
                entity.Property(e => e.UserId).HasColumnName("id_user");
                entity.Property(e => e.PenaltyAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("penalty");

                entity.HasOne(d => d.Book).WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("borrow_id_book_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("borrow_id_user_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("user_pkey");

                entity.ToTable("user");

                entity.HasIndex(e => e.LibraryCardNumber, "user_librarycard_key").IsUnique();

                entity.Property(e => e.UserId)
                .HasColumnName("id_user")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.LibraryCardExpiringDate).HasColumnName("cardexp");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("fname");
                entity.Property(e => e.LibraryCardNumber)
                    .HasMaxLength(50)
                    .HasColumnName("librarycard");
                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("lname");
                entity.Property(e => e.Notes)
                    .HasColumnName("notes");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");
                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .HasColumnName("position");
                entity.Property(e => e.Penalty)
                    .HasMaxLength(50)
                    .HasColumnName("penalty");
            });
            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("request_pkey");

                entity.ToTable("request");

                entity.Property(e => e.RequestId).HasColumnName("id_request");
                entity.Property(e => e.ProcessName).HasMaxLength(255).HasColumnName("processname");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.StartDate).HasColumnName("startdate");
                entity.Property(e => e.EndDate).HasColumnName("enddate");
            });
            modelBuilder.Entity<Process>(entity =>
            {
                entity.HasKey(e => e.ProcessId).HasName("process_pkey");

                entity.ToTable("process");

                entity.Property(e => e.ProcessId).HasColumnName("id_process");
                entity.Property(e => e.RequestId).HasColumnName("id_request");
                entity.Property(e => e.WorkflowId).HasColumnName("id_workflow");
                entity.Property(e => e.RequesterId).HasColumnName("id_requester");
                entity.Property(e => e.RequestType).HasColumnName("request_type");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CurrentStepId).HasColumnName("id_current_step");
                entity.Property(e => e.RequestDate).HasColumnName("request_date");

                entity.HasOne(p => p.Workflow)
                     .WithMany(w => w.Processes)
                     .HasForeignKey(p => p.WorkflowId)
                     .HasConstraintName("FK_Process_Workflow");

                entity.HasOne(p => p.Requester)
                     .WithMany(r => r.Processes)
                     .HasForeignKey(p => p.RequesterId)
                     .HasConstraintName("FK_Process_Requester");

                entity.HasOne(p => p.Request)
                     .WithOne(r => r.Process)
                     .HasForeignKey<Process>(p => p.RequestId)
                     .HasConstraintName("FK_Process_Request");
            });
            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId).HasName("workflow_pkey");

                entity.ToTable("workflow");

                entity.Property(e => e.WorkflowId).HasColumnName("id_workflow");
                entity.Property(e => e.WorkflowName).HasMaxLength(255).HasColumnName("workflowname");
                entity.Property(e => e.Description).HasColumnName("description");
            });
            modelBuilder.Entity<WorkflowSequence>(entity =>
            {
                entity.HasKey(e => e.StepId).HasName("workflow_sequence_pkey");

                entity.ToTable("workflow_sequence");

                entity.Property(e => e.StepId).HasColumnName("step_id");
                entity.Property(e => e.WorkflowId).HasColumnName("id_workflow");
                entity.Property(e => e.StepOrder).HasColumnName("steporder");
                entity.Property(e => e.StepName).HasMaxLength(255).HasColumnName("stepname");
                entity.Property(e => e.RequiredRole).HasMaxLength(255).HasColumnName("requiredrole");

                entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowSequences)
                    .HasForeignKey(d => d.WorkflowId)
                    .HasConstraintName("workflow_sequence_id_workflow_fkey");
            });
            modelBuilder.Entity<WorkflowAction>(entity =>
            {
                entity.HasKey(e => e.ActionId).HasName("workflow_action_pkey");

                entity.ToTable("workflow_action");

                entity.Property(e => e.ActionId).HasColumnName("id_action");
                entity.Property(e => e.ProcessId).HasColumnName("id_proceess");
                entity.Property(e => e.StepId).HasColumnName("id_step");
                entity.Property(e => e.ActorId).HasColumnName("id_actor");
                entity.Property(e => e.Action).HasMaxLength(255).HasColumnName("action");
                entity.Property(e => e.ActionDate).HasColumnName("actiondate");
                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.HasOne(wf => wf.Process).WithMany(p => p.WorkflowActions)
                    .HasForeignKey(wf => wf.ProcessId)
                    .HasConstraintName("workflow_action_id_request_fkey");

                entity.HasOne(e => e.Actor).WithMany(u => u.WorkflowActions)
                    .HasForeignKey(e => e.ActorId)
                    .HasConstraintName("FK_WorkflowAction_User");
            });
            modelBuilder.Entity<NextStepRules>(entity =>
            {
                entity.HasKey(e => e.RuleId).HasName("next_step_rule_pkey");

                entity.ToTable("next_step_rule");

                entity.Property(e => e.RuleId).HasColumnName("id_rule");
                entity.Property(e => e.CurrentStepId).HasColumnName("id_currentstep");
                entity.Property(e => e.NextStepId).HasColumnName("id_nextstep");
                entity.Property(e => e.ConditionType)
                    .HasMaxLength(100)
                    .HasColumnName("conditiontype");
                entity.Property(e => e.ConditionValue)
                    .HasMaxLength(255)
                    .HasColumnName("conditionvalue");

                // Relasi ke WorkflowSequence sebagai current step
                entity.HasOne(d => d.CurrentStep)
                    .WithMany()
                    .HasForeignKey(d => d.CurrentStepId)
                    .HasConstraintName("next_step_rule_id_currentstep_fkey")
                    .OnDelete(DeleteBehavior.Restrict);

                // Relasi ke WorkflowSequence sebagai next step
                entity.HasOne(d => d.NextStep)
                    .WithMany()
                    .HasForeignKey(d => d.NextStepId)
                    .HasConstraintName("next_step_rule_id_nextstep_fkey")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
