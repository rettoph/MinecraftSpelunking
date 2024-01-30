﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinecraftSpelunking.Domain.Database;

#nullable disable

namespace MinecraftSpelunking.Domain.Database.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240130030401_AddDescriptionNormalizedColumnToJavaServers")]
    partial class AddDescriptionNormalizedColumnToJavaServers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JavaServerModVersion", b =>
                {
                    b.Property<int>("JavaServersId")
                        .HasColumnType("int");

                    b.Property<int>("ModVersionsId")
                        .HasColumnType("int");

                    b.HasKey("JavaServersId", "ModVersionsId");

                    b.HasIndex("ModVersionsId");

                    b.ToTable("JavaServerModVersion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Account.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ApiAccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasAlternateKey("ApiAccessToken");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Account.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User",
                            NormalizedName = "User",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin",
                            NormalizedName = "Admin",
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dummy",
                            NormalizedName = "Dummy",
                            Type = 3
                        });
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.AddressBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AddressBlocks");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.AddressBlockAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AssignedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("BlockId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("UserId");

                    b.ToTable("AddressBlockAssignments");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.JavaServer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressBlockId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionNormalized")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IconId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastOnlineAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModPackDataId")
                        .HasColumnType("int");

                    b.Property<int?>("ModTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayersMax")
                        .HasColumnType("int");

                    b.Property<int>("PlayersOnline")
                        .HasColumnType("int");

                    b.Property<string>("PlayersSample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Host", "Port");

                    b.HasIndex("AddressBlockId");

                    b.HasIndex("IconId");

                    b.HasIndex("ModPackDataId");

                    b.HasIndex("ModTypeId");

                    b.ToTable("JavaServers");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.Mod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Mods");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.ModPackData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsMetadata")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VersionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "Version");

                    b.ToTable("ModPackData");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.ModType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("ModTypes");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.ModVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ModId")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("ModId", "Version");

                    b.ToTable("ModVersions");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.ReservedAddressBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReservedAddressBlocks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Network = "0.0.0.0/8"
                        },
                        new
                        {
                            Id = 2,
                            Network = "10.0.0.0/8"
                        },
                        new
                        {
                            Id = 3,
                            Network = "100.64.0.0/10"
                        },
                        new
                        {
                            Id = 4,
                            Network = "127.0.0.0/8"
                        },
                        new
                        {
                            Id = 5,
                            Network = "169.254.0.0/16"
                        },
                        new
                        {
                            Id = 6,
                            Network = "172.16.0.0/12"
                        },
                        new
                        {
                            Id = 7,
                            Network = "192.0.0.0/24"
                        },
                        new
                        {
                            Id = 8,
                            Network = "192.0.2.0/24"
                        },
                        new
                        {
                            Id = 9,
                            Network = "192.88.99.0/24"
                        },
                        new
                        {
                            Id = 10,
                            Network = "192.168.0.0/16"
                        },
                        new
                        {
                            Id = 11,
                            Network = "198.18.0.0/15"
                        },
                        new
                        {
                            Id = 12,
                            Network = "198.51.100.0/24"
                        },
                        new
                        {
                            Id = 13,
                            Network = "203.0.113.0/24"
                        },
                        new
                        {
                            Id = 14,
                            Network = "224.0.0.0/4"
                        },
                        new
                        {
                            Id = 15,
                            Network = "240.0.0.0/4"
                        },
                        new
                        {
                            Id = 16,
                            Network = "255.255.255.255/32"
                        });
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.ServerIcon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Hash")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServerIcons");
                });

            modelBuilder.Entity("JavaServerModVersion", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.JavaServer", null)
                        .WithMany()
                        .HasForeignKey("JavaServersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.ModVersion", null)
                        .WithMany()
                        .HasForeignKey("ModVersionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.AddressBlockAssignment", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.AddressBlock", "Block")
                        .WithMany("Assignments")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinecraftSpelunking.Common.Account.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.JavaServer", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.AddressBlock", "AddressBlock")
                        .WithMany()
                        .HasForeignKey("AddressBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.ServerIcon", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.ModPackData", "ModPackData")
                        .WithMany()
                        .HasForeignKey("ModPackDataId");

                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.ModType", "ModType")
                        .WithMany()
                        .HasForeignKey("ModTypeId");

                    b.Navigation("AddressBlock");

                    b.Navigation("Icon");

                    b.Navigation("ModPackData");

                    b.Navigation("ModType");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.ModVersion", b =>
                {
                    b.HasOne("MinecraftSpelunking.Common.Minecraft.Entities.Mod", "Mod")
                        .WithMany()
                        .HasForeignKey("ModId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mod");
                });

            modelBuilder.Entity("MinecraftSpelunking.Common.Minecraft.Entities.AddressBlock", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
