﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Persistence;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("identity")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.Core.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .IsRequired()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events", "identity");
                });

            modelBuilder.Entity("Server.Core.Models.EventIdentityUser", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdentityUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("EventId", "IdentityUserId");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("EventIdentityUser", "identity");
                });

            modelBuilder.Entity("Server.Core.Models.IdentityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInformation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Education")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interests")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessionalExperience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Searchings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Work")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "identity");
                });

            modelBuilder.Entity("Server.Core.Models.Event", b =>
                {
                    b.HasOne("Server.Core.Models.IdentityUser", "Creator")
                        .WithMany("EventsCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Server.Core.Models.EventIdentityUser", b =>
                {
                    b.HasOne("Server.Core.Models.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Server.Core.Models.IdentityUser", "IdentityUser")
                        .WithMany("EventsAttended")
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("Server.Core.Models.IdentityUser", b =>
                {
                    b.OwnsOne("Server.Core.Models.AuthorizationDetails", "AuthorizationDetails", b1 =>
                        {
                            b1.Property<Guid>("IdentityUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("IdentityUserId");

                            b1.ToTable("Users", "identity");

                            b1.ToJson("AuthorizationDetails");

                            b1.WithOwner()
                                .HasForeignKey("IdentityUserId");

                            b1.OwnsMany("Server.Common.Core.Models.ClaimModel", "Claims", b2 =>
                                {
                                    b2.Property<Guid>("AuthorizationDetailsIdentityUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("AuthorizationDetailsIdentityUserId", "Id");

                                    b2.ToTable("Users", "identity");

                                    b2.WithOwner()
                                        .HasForeignKey("AuthorizationDetailsIdentityUserId");
                                });

                            b1.OwnsMany("Server.Common.Core.Models.GenericPermissionModel", "Permissions", b2 =>
                                {
                                    b2.Property<Guid>("AuthorizationDetailsIdentityUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Discriminator")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("AuthorizationDetailsIdentityUserId", "Id");

                                    b2.ToTable("Users", "identity");

                                    b2.WithOwner()
                                        .HasForeignKey("AuthorizationDetailsIdentityUserId");
                                });

                            b1.OwnsMany("Server.Common.Core.Models.GenericRoleModel", "Roles", b2 =>
                                {
                                    b2.Property<Guid>("AuthorizationDetailsIdentityUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Discriminator")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("AuthorizationDetailsIdentityUserId", "Id");

                                    b2.ToTable("Users", "identity");

                                    b2.WithOwner()
                                        .HasForeignKey("AuthorizationDetailsIdentityUserId");
                                });

                            b1.Navigation("Claims");

                            b1.Navigation("Permissions");

                            b1.Navigation("Roles");
                        });

                    b.OwnsMany("Server.Core.Models.LoginInfo", "Logins", b1 =>
                        {
                            b1.Property<Guid>("IdentityUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("IpAddress")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTimeOffset>("OccurredOn")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("IdentityUserId", "Id");

                            b1.ToTable("Users", "identity");

                            b1.ToJson("Logins");

                            b1.WithOwner()
                                .HasForeignKey("IdentityUserId");

                            b1.OwnsMany("Server.Core.Models.Permission", "Permissions", b2 =>
                                {
                                    b2.Property<Guid>("LoginInfoIdentityUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("LoginInfoId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<Guid>("SiteId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.HasKey("LoginInfoIdentityUserId", "LoginInfoId", "Id");

                                    b2.ToTable("Users", "identity");

                                    b2.ToJson("Permissions");

                                    b2.WithOwner()
                                        .HasForeignKey("LoginInfoIdentityUserId", "LoginInfoId");

                                    b2.OwnsOne("Server.Common.Core.Role", "Role", b3 =>
                                        {
                                            b3.Property<Guid>("PermissionLoginInfoIdentityUserId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<int>("PermissionLoginInfoId")
                                                .HasColumnType("int");

                                            b3.Property<int>("PermissionId")
                                                .HasColumnType("int");

                                            b3.Property<string>("Name")
                                                .IsRequired()
                                                .HasColumnType("nvarchar(max)");

                                            b3.Property<int>("Value")
                                                .HasColumnType("int");

                                            b3.HasKey("PermissionLoginInfoIdentityUserId", "PermissionLoginInfoId", "PermissionId");

                                            b3.ToTable("Users", "identity");

                                            b3.WithOwner()
                                                .HasForeignKey("PermissionLoginInfoIdentityUserId", "PermissionLoginInfoId", "PermissionId");
                                        });

                                    b2.Navigation("Role")
                                        .IsRequired();
                                });

                            b1.Navigation("Permissions");
                        });

                    b.Navigation("AuthorizationDetails")
                        .IsRequired();

                    b.Navigation("Logins");
                });

            modelBuilder.Entity("Server.Core.Models.Event", b =>
                {
                    b.Navigation("Attendees");
                });

            modelBuilder.Entity("Server.Core.Models.IdentityUser", b =>
                {
                    b.Navigation("EventsAttended");

                    b.Navigation("EventsCreated");
                });
#pragma warning restore 612, 618
        }
    }
}
