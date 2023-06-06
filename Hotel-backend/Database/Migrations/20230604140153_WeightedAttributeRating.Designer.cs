﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20230604140153_WeightedAttributeRating")]
    partial class WeightedAttributeRating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AppUserRefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_AppUserRefreshToken_UserId");

                    b.ToTable("AppUserRefreshToken", (string)null);
                });

            modelBuilder.Entity("AppUserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Attribute", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AttributeName")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("ID");

                    b.ToTable("Attribute", (string)null);
                });

            modelBuilder.Entity("AttributeDecision", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccumulatedCapital")
                        .HasColumnType("int");

                    b.Property<string>("Attribute")
                        .HasColumnType("longtext");

                    b.Property<bool>("Confirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("LaborBudget")
                        .HasColumnType("int");

                    b.Property<int>("MonthID")
                        .HasColumnType("int");

                    b.Property<int>("NewCapital")
                        .HasColumnType("int");

                    b.Property<int>("OperationBudget")
                        .HasColumnType("int");

                    b.Property<int>("QuarterForecast")
                        .HasColumnType("int");

                    b.Property<int>("QuarterNo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MonthID");

                    b.ToTable("AttributeDecision", (string)null);
                });

            modelBuilder.Entity("ClassGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Serial")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("ClassId")
                        .HasDatabaseName("IX_ClassGroup_ClassID");

                    b.ToTable("ClassGroup", (string)null);
                });

            modelBuilder.Entity("ClassSession", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrentQuater")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelsCount")
                        .HasColumnType("int");

                    b.Property<string>("Memo")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("RoomInEachHotel")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("ClassId");

                    b.ToTable("Class", (string)null);
                });

            modelBuilder.Entity("CustomerRawRating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Attribute")
                        .HasColumnType("longtext");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("MonthID")
                        .HasColumnType("int");

                    b.Property<int>("QuarterNo")
                        .HasColumnType("int");

                    b.Property<int>("RawRating")
                        .HasColumnType("int");

                    b.Property<string>("Segment")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("MonthID");

                    b.ToTable("CustomerRawRating", (string)null);
                });

            modelBuilder.Entity("Database.Domain.StudentClassMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentClassMapping");
                });

            modelBuilder.Entity("Database.Domain.StudentSignupTemp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ClassCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Institute")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsSignupComplete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentFailureReason")
                        .HasColumnType("longtext");

                    b.Property<int>("PaymentStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("RawTransactionResponse")
                        .HasColumnType("longtext");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("quantityleft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StudentSignupTemp", (string)null);
                });

            modelBuilder.Entity("DistributionChannels", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Channel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("DistributionChannels", (string)null);
                });

            modelBuilder.Entity("MarketingDecision", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActualDemand")
                        .HasColumnType("int");

                    b.Property<bool>("Confirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("LaborSpending")
                        .HasColumnType("int");

                    b.Property<string>("MarketingTechniques")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MonthID")
                        .HasColumnType("int");

                    b.Property<int>("QuarterNo")
                        .HasColumnType("int");

                    b.Property<string>("Segment")
                        .HasColumnType("longtext");

                    b.Property<int>("Spending")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MonthID");

                    b.ToTable("MarketingDecision", (string)null);
                });

            modelBuilder.Entity("MarketingTechniques", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Techniques")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("MarketingTechniques", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MigrationScript", b =>
                {
                    b.Property<string>("ScriptId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("ExecutedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.HasKey("ScriptId");

                    b.ToTable("__MigrationScript", (string)null);
                });

            modelBuilder.Entity("Month", b =>
                {
                    b.Property<int>("MonthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("ConfigId")
                        .HasColumnType("int");

                    b.Property<bool>("IsComplete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalMarket")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MonthId");

                    b.HasIndex("ClassId")
                        .HasDatabaseName("IX_ClassGroup_ClassID");

                    b.ToTable("ClassMonth", (string)null);
                });

            modelBuilder.Entity("PriceDecision", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActualDemand")
                        .HasColumnType("int");

                    b.Property<bool>("Confirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("DistributionChannel")
                        .HasColumnType("longtext");

                    b.Property<string>("GroupID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MonthID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("QuarterNo")
                        .HasColumnType("int");

                    b.Property<string>("Segment")
                        .HasColumnType("longtext");

                    b.Property<bool>("Weekday")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("ID");

                    b.HasIndex("MonthID");

                    b.ToTable("PriceDecision", (string)null);
                });

            modelBuilder.Entity("RoomAllocation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActualDemand")
                        .HasColumnType("int");

                    b.Property<bool>("Confirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("MonthID")
                        .HasColumnType("int");

                    b.Property<int>("QuarterForecast")
                        .HasColumnType("int");

                    b.Property<int>("QuarterNo")
                        .HasColumnType("int");

                    b.Property<int>("Revenue")
                        .HasColumnType("int");

                    b.Property<int>("RoomsAllocated")
                        .HasColumnType("int");

                    b.Property<int>("RoomsSold")
                        .HasColumnType("int");

                    b.Property<string>("Segment")
                        .HasColumnType("longtext");

                    b.Property<bool>("Weekday")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("ID");

                    b.HasIndex("MonthID");

                    b.ToTable("RoomAllocation", (string)null);
                });

            modelBuilder.Entity("Segment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MaxRating")
                        .HasColumnType("int");

                    b.Property<string>("SegmentName")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("ID");

                    b.ToTable("Segment", (string)null);
                });

            modelBuilder.Entity("WeightedAttributeRating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActualDemand")
                        .HasColumnType("int");

                    b.Property<int>("CustomerRating")
                        .HasColumnType("int");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("MonthID")
                        .HasColumnType("int");

                    b.Property<int>("QuarterNo")
                        .HasColumnType("int");

                    b.Property<string>("Segment")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("MonthID");

                    b.ToTable("WeightedAttributeRating", (string)null);
                });

            modelBuilder.Entity("Instructor", b =>
                {
                    b.HasBaseType("AppUser");

                    b.Property<string>("Institute")
                        .HasColumnType("longtext");

                    b.ToTable("Instructor", (string)null);
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.HasBaseType("AppUser");

                    b.Property<string>("Institue")
                        .HasColumnType("longtext");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("AppUserRefreshToken", b =>
                {
                    b.HasOne("AppUser", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AttributeDecision", b =>
                {
                    b.HasOne("Month", "Month")
                        .WithMany("AttributeDecision")
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });

            modelBuilder.Entity("ClassGroup", b =>
                {
                    b.HasOne("ClassSession", "Class")
                        .WithMany("Groups")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("CustomerRawRating", b =>
                {
                    b.HasOne("Month", "Month")
                        .WithMany("CustomerRawRating")
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });

            modelBuilder.Entity("Database.Domain.StudentClassMapping", b =>
                {
                    b.HasOne("ClassSession", "Class")
                        .WithMany("StudentClassMappings")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student", "Student")
                        .WithMany("StudentClassMappings")
                        .HasForeignKey("StudentId");

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MarketingDecision", b =>
                {
                    b.HasOne("Month", "Month")
                        .WithMany("MarketingDecision")
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("AppUserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("AppUserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Month", b =>
                {
                    b.HasOne("ClassSession", "Class")
                        .WithMany("Months")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("PriceDecision", b =>
                {
                    b.HasOne("Month", "Month")
                        .WithMany("PriceDecision")
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });

            modelBuilder.Entity("RoomAllocation", b =>
                {
                    b.HasOne("Month", "Month")
                        .WithMany("RoomAllocation")
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });

            modelBuilder.Entity("WeightedAttributeRating", b =>
                {
                    b.HasOne("Month", "Month")
                        .WithMany("WeightedAttributeRating")
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });

            modelBuilder.Entity("Instructor", b =>
                {
                    b.HasOne("AppUser", null)
                        .WithOne()
                        .HasForeignKey("Instructor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.HasOne("AppUser", null)
                        .WithOne()
                        .HasForeignKey("Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppUser", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("ClassSession", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Months");

                    b.Navigation("StudentClassMappings");
                });

            modelBuilder.Entity("Month", b =>
                {
                    b.Navigation("AttributeDecision");

                    b.Navigation("CustomerRawRating");

                    b.Navigation("MarketingDecision");

                    b.Navigation("PriceDecision");

                    b.Navigation("RoomAllocation");

                    b.Navigation("WeightedAttributeRating");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Navigation("StudentClassMappings");
                });
#pragma warning restore 612, 618
        }
    }
}