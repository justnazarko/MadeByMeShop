﻿// <auto-generated />
using System;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MadeByMe.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MadeByMe.src.Models.BuyerCart", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartItemId"));

                    b.Property<int>("CartId")
                        .HasColumnType("integer");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("PostId");

                    b.ToTable("BuyerCarts");

                    b.HasData(
                        new
                        {
                            CartItemId = 1,
                            CartId = 1,
                            PostId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            CartItemId = 2,
                            CartId = 1,
                            PostId = 3,
                            Quantity = 1
                        },
                        new
                        {
                            CartItemId = 3,
                            CartId = 2,
                            PostId = 2,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartId"));

                    b.Property<int?>("BuyerId")
                        .HasColumnType("integer");

                    b.HasKey("CartId");

                    b.HasIndex("BuyerId");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            CartId = 1,
                            BuyerId = 3
                        },
                        new
                        {
                            CartId = 2,
                            BuyerId = 1
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Handmade Jewelry"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Home Decor"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Art"
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            CommentId = 1,
                            Content = "Дуже гарна сережка! Якісне виконання.",
                            CreatedAt = new DateTime(2025, 4, 7, 19, 45, 14, 107, DateTimeKind.Utc).AddTicks(3116),
                            PostId = 1,
                            UserId = 3
                        },
                        new
                        {
                            CommentId = 2,
                            Content = "Чудова картина, автор - талановитий!",
                            CreatedAt = new DateTime(2025, 4, 6, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3118),
                            PostId = 3,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("post_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric(3,2)");

                    b.Property<int>("SellerId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CreatedAt = new DateTime(2025, 4, 7, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3084),
                            Description = "Ручної роботи з натуральним каменем",
                            PhotoLink = "/images/earring1.jpg",
                            Price = 799.99m,
                            Rating = 0.0m,
                            SellerId = 2,
                            Status = "active",
                            Title = "Срібна сережка"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            CreatedAt = new DateTime(2025, 4, 2, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3085),
                            Description = "Керамічна ваза з українським орнаментом",
                            PhotoLink = "/images/vase.jpg",
                            Price = 1200.50m,
                            Rating = 0.0m,
                            SellerId = 2,
                            Status = "active",
                            Title = "Декоративна ваза"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            CreatedAt = new DateTime(2025, 3, 28, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3092),
                            Description = "Олія на полотні, 40x60 см",
                            PhotoLink = "/images/painting.jpg",
                            Price = 2500.00m,
                            Rating = 0.0m,
                            SellerId = 2,
                            Status = "active",
                            Title = "Картина 'Сонячний день'"
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.SellerPost", b =>
                {
                    b.Property<int>("SellerId")
                        .HasColumnType("integer")
                        .HasColumnName("seller_id");

                    b.Property<int>("PostId")
                        .HasColumnType("integer")
                        .HasColumnName("post_id");

                    b.HasKey("SellerId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("SellerPosts");

                    b.HasData(
                        new
                        {
                            SellerId = 2,
                            PostId = 1
                        },
                        new
                        {
                            SellerId = 2,
                            PostId = 2
                        },
                        new
                        {
                            SellerId = 2,
                            PostId = 3
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("ModelNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UserType")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EmailAddress = "admin@example.com",
                            IsBlocked = false,
                            Password = "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==",
                            ProfilePicture = "/images/admin.jpg",
                            UserType = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            EmailAddress = "artist@example.com",
                            IsBlocked = false,
                            Password = "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==",
                            ProfilePicture = "/images/artist.jpg",
                            UserType = "Seller",
                            Username = "artist123"
                        },
                        new
                        {
                            UserId = 3,
                            EmailAddress = "customer@example.com",
                            IsBlocked = false,
                            Password = "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==",
                            ProfilePicture = "/images/customer.jpg",
                            UserType = "Buyer",
                            Username = "customer1"
                        });
                });

            modelBuilder.Entity("MadeByMe.src.Models.BuyerCart", b =>
                {
                    b.HasOne("MadeByMe.src.Models.Cart", "Cart")
                        .WithMany("BuyerCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MadeByMe.src.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("MadeByMe.src.Models.Cart", b =>
                {
                    b.HasOne("MadeByMe.src.Models.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("MadeByMe.src.Models.Comment", b =>
                {
                    b.HasOne("MadeByMe.src.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MadeByMe.src.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MadeByMe.src.Models.Post", b =>
                {
                    b.HasOne("MadeByMe.src.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MadeByMe.src.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("MadeByMe.src.Models.SellerPost", b =>
                {
                    b.HasOne("MadeByMe.src.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MadeByMe.src.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("MadeByMe.src.Models.Cart", b =>
                {
                    b.Navigation("BuyerCarts");
                });

            modelBuilder.Entity("MadeByMe.src.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
