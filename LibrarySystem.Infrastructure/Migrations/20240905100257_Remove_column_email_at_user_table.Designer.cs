﻿// <auto-generated />
using System;
using LibrarySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibrarySystem.Infrastructure.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240905100257_Remove_column_email_at_user_table")]
    partial class Remove_column_email_at_user_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibrarySystem.Domain.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpire")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_book");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("author");

                    b.Property<int>("AvailableBooks")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("availablebook");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("category");

                    b.Property<string>("DeleteReasoning")
                        .HasColumnType("text")
                        .HasColumnName("reason");

                    b.Property<bool>("DeleteStamp")
                        .HasColumnType("boolean")
                        .HasColumnName("delete_stamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("isbn");

                    b.Property<string>("Language")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("language");

                    b.Property<string>("LibraryLocation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("location");

                    b.Property<int>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("integer");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("publisher");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("purchasedate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("book_pkey");

                    b.HasIndex(new[] { "ISBN" }, "book_isbn_key")
                        .IsUnique();

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Borrowing", b =>
                {
                    b.Property<int>("BorrowingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_borrow");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BorrowingId"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("id_book");

                    b.Property<DateTime>("BorrowedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_borrow");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("HasUnpaidPenalty")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("PenaltyAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("penalty");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_return");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("id_user");

                    b.HasKey("BorrowingId")
                        .HasName("borrow_pkey");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("borrow", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.NextStepRules", b =>
                {
                    b.Property<int>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_rule");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RuleId"));

                    b.Property<string>("ConditionType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("conditiontype");

                    b.Property<string>("ConditionValue")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("conditionvalue");

                    b.Property<int>("CurrentStepId")
                        .HasColumnType("integer")
                        .HasColumnName("id_currentstep");

                    b.Property<int>("NextStepId")
                        .HasColumnType("integer")
                        .HasColumnName("id_nextstep");

                    b.HasKey("RuleId")
                        .HasName("next_step_rule_pkey");

                    b.HasIndex("CurrentStepId");

                    b.HasIndex("NextStepId");

                    b.ToTable("next_step_rule", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Process", b =>
                {
                    b.Property<int>("ProcessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_process");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProcessId"));

                    b.Property<int>("CurrentStepId")
                        .HasColumnType("integer")
                        .HasColumnName("id_current_step");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("request_date");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer")
                        .HasColumnName("id_request");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("request_type");

                    b.Property<string>("RequesterId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("id_requester");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer")
                        .HasColumnName("id_workflow");

                    b.HasKey("ProcessId")
                        .HasName("process_pkey");

                    b.HasIndex("CurrentStepId");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.HasIndex("RequesterId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("process", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_request");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RequestId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("enddate");

                    b.Property<string>("ProcessName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("processname");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("startdate");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer");

                    b.HasKey("RequestId")
                        .HasName("request_pkey");

                    b.HasIndex("WorkflowId");

                    b.ToTable("request", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_user");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("fname");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("lname");

                    b.Property<DateTime>("LibraryCardExpiringDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("cardexp");

                    b.Property<string>("LibraryCardNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("librarycard");

                    b.Property<string>("Notes")
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<string>("Penalty")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("penalty");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("position");

                    b.Property<string>("Privilege")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId")
                        .HasName("user_pkey");

                    b.HasIndex(new[] { "LibraryCardNumber" }, "user_librarycard_key")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Workflow", b =>
                {
                    b.Property<int>("WorkflowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_workflow");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WorkflowId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("WorkflowName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("workflowname");

                    b.HasKey("WorkflowId")
                        .HasName("workflow_pkey");

                    b.ToTable("workflow", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.WorkflowAction", b =>
                {
                    b.Property<int>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_action");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ActionId"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("action");

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("actiondate");

                    b.Property<string>("ActorId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("id_actor");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comments");

                    b.Property<int>("ProcessId")
                        .HasColumnType("integer")
                        .HasColumnName("id_proceess");

                    b.Property<int>("StepId")
                        .HasColumnType("integer")
                        .HasColumnName("id_step");

                    b.HasKey("ActionId")
                        .HasName("workflow_action_pkey");

                    b.HasIndex("ActorId");

                    b.HasIndex("ProcessId");

                    b.HasIndex("StepId");

                    b.ToTable("workflow_action", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.WorkflowSequence", b =>
                {
                    b.Property<int>("WorkflowId")
                        .HasColumnType("integer")
                        .HasColumnName("id_workflow");

                    b.Property<string>("RequiredRole")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("requiredrole");

                    b.Property<int>("StepId")
                        .HasColumnType("integer")
                        .HasColumnName("step_id");

                    b.Property<string>("StepName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("stepname");

                    b.Property<int>("StepOrder")
                        .HasColumnType("integer")
                        .HasColumnName("steporder");

                    b.HasKey("WorkflowId")
                        .HasName("workflow_sequence_pkey");

                    b.ToTable("workflow_sequence", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.AppUser", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Borrowing", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.Book", "Book")
                        .WithMany("Borrows")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("borrow_id_book_fkey");

                    b.HasOne("LibrarySystem.Domain.Models.User", "User")
                        .WithMany("Borrows")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("borrow_id_user_fkey");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.NextStepRules", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.WorkflowSequence", "CurrentStep")
                        .WithMany()
                        .HasForeignKey("CurrentStepId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("next_step_rule_id_currentstep_fkey");

                    b.HasOne("LibrarySystem.Domain.Models.WorkflowSequence", "NextStep")
                        .WithMany()
                        .HasForeignKey("NextStepId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("next_step_rule_id_nextstep_fkey");

                    b.Navigation("CurrentStep");

                    b.Navigation("NextStep");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Process", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.WorkflowSequence", "WorkflowSequence")
                        .WithMany()
                        .HasForeignKey("CurrentStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibrarySystem.Domain.Models.Request", "Request")
                        .WithOne("Process")
                        .HasForeignKey("LibrarySystem.Domain.Models.Process", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Process_Request");

                    b.HasOne("LibrarySystem.Domain.Models.AppUser", "Requester")
                        .WithMany("Processes")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Process_Requester");

                    b.HasOne("LibrarySystem.Domain.Models.Workflow", "Workflow")
                        .WithMany("Processes")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Process_Workflow");

                    b.Navigation("Request");

                    b.Navigation("Requester");

                    b.Navigation("Workflow");

                    b.Navigation("WorkflowSequence");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Request", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.WorkflowAction", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.AppUser", "Actor")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_WorkflowAction_User");

                    b.HasOne("LibrarySystem.Domain.Models.Process", "Process")
                        .WithMany("WorkflowActions")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("workflow_action_id_request_fkey");

                    b.HasOne("LibrarySystem.Domain.Models.WorkflowSequence", "WorkflowSequence")
                        .WithMany()
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Process");

                    b.Navigation("WorkflowSequence");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.WorkflowSequence", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.Workflow", "Workflow")
                        .WithMany("WorkflowSequences")
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("workflow_sequence_id_workflow_fkey");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibrarySystem.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LibrarySystem.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.AppUser", b =>
                {
                    b.Navigation("Processes");

                    b.Navigation("WorkflowActions");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Book", b =>
                {
                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Process", b =>
                {
                    b.Navigation("WorkflowActions");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Request", b =>
                {
                    b.Navigation("Process")
                        .IsRequired();
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.User", b =>
                {
                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("LibrarySystem.Domain.Models.Workflow", b =>
                {
                    b.Navigation("Processes");

                    b.Navigation("WorkflowSequences");
                });
#pragma warning restore 612, 618
        }
    }
}
