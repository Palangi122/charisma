﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DomainModel.Customers.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Addresses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DomainModel.Orders.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("float");

                    b.Property<string>("DiscountNO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PostTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<double>("TotalOrder")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PostTypeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DomainModel.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Qty")
                        .HasColumnType("float");

                    b.Property<bool>("WithMarkup")
                        .HasColumnType("bit");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("DomainModel.Posts.PostType", b =>
                {
                    b.Property<Guid>("PostTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extra")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostTypeId");

                    b.ToTable("postTypes");
                });

            modelBuilder.Entity("DomainModel.Pricing.Discounts.Discount", b =>
                {
                    b.Property<Guid>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiscountNO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountType")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("DomainModel.Products.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Markup")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DomainModel.Products.ProductProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("ProductProductTypes");
                });

            modelBuilder.Entity("DomainModel.Products.ProductType", b =>
                {
                    b.Property<Guid>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("QueryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("DomainModel.Orders.Order", b =>
                {
                    b.HasOne("DomainModel.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainModel.Posts.PostType", "PostType")
                        .WithMany()
                        .HasForeignKey("PostTypeId");
                });

            modelBuilder.Entity("DomainModel.Orders.OrderItem", b =>
                {
                    b.HasOne("DomainModel.Orders.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("DomainModel.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DomainModel.Products.ProductProductType", b =>
                {
                    b.HasOne("DomainModel.Products.Product", "Product")
                        .WithMany("ProductProductTypes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainModel.Products.ProductType", "ProductType")
                        .WithMany("ProductProductTypes")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
