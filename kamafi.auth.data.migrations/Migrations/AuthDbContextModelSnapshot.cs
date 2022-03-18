﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using kamafi.auth.data;

#nullable disable

namespace kamafi.auth.data.migrations.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("kamafi.auth.data.models.Role", b =>
                {
                    b.Property<string>("RoleName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("role_name");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<Guid>("PublicKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("public_key")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("current_timestamp");

                    b.HasKey("RoleName")
                        .HasName("pk_role");

                    b.HasIndex("PublicKey")
                        .HasDatabaseName("ix_role_public_key");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("kamafi.auth.data.models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("password");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("password_salt");

                    b.Property<Guid>("PublicKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("public_key")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("role_name");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("current_timestamp");

                    b.HasKey("UserId")
                        .HasName("pk_user");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_user_email");

                    b.HasIndex("PublicKey")
                        .HasDatabaseName("ix_user_public_key");

                    b.HasIndex("RoleName")
                        .HasDatabaseName("ix_user_role_name");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("kamafi.auth.data.models.UserApiKey", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("api_key");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<bool?>("IsEnabled")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("is_enabled")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("current_timestamp");

                    b.HasKey("UserId")
                        .HasName("pk_user_api_key");

                    b.ToTable("user_api_key", (string)null);
                });

            modelBuilder.Entity("kamafi.auth.data.models.User", b =>
                {
                    b.HasOne("kamafi.auth.data.models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_role_temp_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("kamafi.auth.data.models.UserApiKey", b =>
                {
                    b.HasOne("kamafi.auth.data.models.User", null)
                        .WithMany("ApiKeys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_user_api_key_user_user_id");
                });

            modelBuilder.Entity("kamafi.auth.data.models.User", b =>
                {
                    b.Navigation("ApiKeys");
                });
#pragma warning restore 612, 618
        }
    }
}
