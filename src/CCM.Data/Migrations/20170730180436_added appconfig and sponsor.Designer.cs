using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CCM.Data.Models;
using CCM.Data.Enums;

namespace CCM.Data.Migrations
{
    [DbContext(typeof(CCMContext))]
    [Migration("20170730180436_added appconfig and sponsor")]
    partial class addedappconfigandsponsor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CCM.Data.Models.AppConfig", b =>
                {
                    b.Property<bool>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("CampName");

                    b.Property<int>("NextCamp");

                    b.Property<byte[]>("Pic1");

                    b.Property<string>("Pic1ContentType");

                    b.Property<string>("Pic1FileName");

                    b.Property<byte[]>("Pic2");

                    b.Property<string>("Pic2ContentType");

                    b.Property<string>("Pic2FileName");

                    b.Property<byte[]>("Pic3");

                    b.Property<string>("Pic3ContentType");

                    b.Property<string>("Pic3FileName");

                    b.Property<byte[]>("Pic4");

                    b.Property<string>("Pic4ContentType");

                    b.Property<string>("Pic4FileName");

                    b.Property<byte[]>("Pic5");

                    b.Property<string>("Pic5ContentType");

                    b.Property<string>("Pic5FileName");

                    b.Property<string>("TagLine");

                    b.HasKey("Id");

                    b.ToTable("AppConfigs");
                });

            modelBuilder.Entity("CCM.Data.Models.AuditLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateChanged");

                    b.Property<string>("EntityName");

                    b.Property<string>("NewValue");

                    b.Property<string>("OldValue");

                    b.Property<string>("PrimaryKeyValue");

                    b.Property<string>("PropertyName");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("CCM.Data.Models.Camp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<string>("LocationAddress")
                        .HasMaxLength(256);

                    b.Property<string>("LocationCity")
                        .HasMaxLength(64);

                    b.Property<string>("LocationInfo");

                    b.Property<string>("LocationName")
                        .HasMaxLength(128);

                    b.Property<string>("LocationState")
                        .HasMaxLength(2);

                    b.Property<string>("LocationZip")
                        .HasMaxLength(5);

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Camps");
                });

            modelBuilder.Entity("CCM.Data.Models.CCMUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<byte[]>("Avatar");

                    b.Property<string>("AvatarContentType")
                        .HasMaxLength(256);

                    b.Property<string>("AvatarFileName")
                        .HasMaxLength(512);

                    b.Property<string>("Bio");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(256);

                    b.Property<string>("LastName")
                        .HasMaxLength(256);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<bool>("OkToContact");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool>("PublicProfile");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CCM.Data.Models.Session", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CampId");

                    b.Property<string>("Description");

                    b.Property<int>("Level");

                    b.Property<string>("Title")
                        .HasMaxLength(500);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CampId");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("CCM.Data.Models.Sponsor", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CampId");

                    b.Property<string>("CompanyUrl");

                    b.Property<byte[]>("Logo");

                    b.Property<string>("LogoContentType");

                    b.Property<string>("LogoFileName");

                    b.Property<string>("Name");

                    b.Property<int?>("SponsorTypeId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CampId");

                    b.HasIndex("SponsorTypeId");

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("CCM.Data.Models.SponsorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.ToTable("SponsorTypes");
                });

            modelBuilder.Entity("CCM.Data.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CCM.Data.Models.TagSession", b =>
                {
                    b.Property<string>("TagId");

                    b.Property<string>("SessionId");

                    b.HasKey("TagId", "SessionId");

                    b.HasIndex("SessionId");

                    b.ToTable("TagSessions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CCM.Data.Models.AuditLog", b =>
                {
                    b.HasOne("CCM.Data.Models.CCMUser", "User")
                        .WithMany("AuditLogs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CCM.Data.Models.Session", b =>
                {
                    b.HasOne("CCM.Data.Models.Camp", "Camp")
                        .WithMany("Sessions")
                        .HasForeignKey("CampId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCM.Data.Models.CCMUser", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CCM.Data.Models.Sponsor", b =>
                {
                    b.HasOne("CCM.Data.Models.Camp", "Camp")
                        .WithMany("Sponsors")
                        .HasForeignKey("CampId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCM.Data.Models.SponsorType", "SponsorType")
                        .WithMany()
                        .HasForeignKey("SponsorTypeId");
                });

            modelBuilder.Entity("CCM.Data.Models.TagSession", b =>
                {
                    b.HasOne("CCM.Data.Models.Session", "Session")
                        .WithMany("TagSessions")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCM.Data.Models.Tag", "Tag")
                        .WithMany("TagSessions")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CCM.Data.Models.CCMUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CCM.Data.Models.CCMUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCM.Data.Models.CCMUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
