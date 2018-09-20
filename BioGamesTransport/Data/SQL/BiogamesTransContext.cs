using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BioGamesTransport.Data.SQL
{
    public partial class BiogamesTransContext : DbContext
    {
        public BiogamesTransContext()
        {
        }

        public BiogamesTransContext(DbContextOptions<BiogamesTransContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Expenditures> Expenditures { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<InvoiceAddresses> InvoiceAddresses { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStatuses> OrderStatuses { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ShipAddresses> ShipAddresses { get; set; }
        public virtual DbSet<ShipModes> ShipModes { get; set; }
        public virtual DbSet<ShipStatuses> ShipStatuses { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SBBV4JC\\BIOGAMESMANAGER;Initial Catalog=BiogamesTrans;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.BankAccount)
                    .HasColumnName("bank_account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Newsletter).HasColumnName("newsletter");

                entity.Property(e => e.OutCustomerId).HasColumnName("out_customer_id");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_customers_customers_Shop");
            });

            modelBuilder.Entity<Expenditures>(entity =>
            {
                entity.ToTable("expenditures");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Expenditures)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expenditures_expenditures_orders");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.ToTable("images");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContentType)
                    .HasColumnName("content_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<InvoiceAddresses>(entity =>
            {
                entity.ToTable("invoice_addresses");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Default).HasColumnName("default");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.OutId).HasColumnName("out_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxNumber)
                    .HasColumnName("tax_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasColumnName("zipcode")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.InvoiceAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_invoice_addresses_invoice_addresses_Customer");
            });

            modelBuilder.Entity<Manufacturers>(entity =>
            {
                entity.ToTable("manufacturers");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.ToTable("order_details");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deposit).HasColumnName("deposit");

                entity.Property(e => e.ExpensePrice).HasColumnName("expense_price");

                entity.Property(e => e.ImagesId).HasColumnName("images_id");

                entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductOutId).HasColumnName("product_out_id");

                entity.Property(e => e.ProductRef)
                    .IsRequired()
                    .HasColumnName("product_ref")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ShipDeliveredDate)
                    .HasColumnName("ship_delivered_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShipExpectedDate)
                    .HasColumnName("ship_expected_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShipModeId).HasColumnName("ship_mode_id");

                entity.Property(e => e.ShipPrice).HasColumnName("ship_price");

                entity.Property(e => e.ShipStatusId).HasColumnName("ship_status_id");

                entity.Property(e => e.ShipUndertakenDate)
                    .HasColumnName("ship_undertaken_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Images)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ImagesId)
                    .HasConstraintName("FK_order_details_order_details_Images");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_order_details_order_details_Manufacturer");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_details_order_details_order");

                entity.HasOne(d => d.ShipMode)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ShipModeId)
                    .HasConstraintName("FK_order_details_order_details_ShipMode");

                entity.HasOne(d => d.ShipStatus)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ShipStatusId)
                    .HasConstraintName("FK_order_details_order_details_ShipStatues");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Deposit).HasColumnName("deposit");

                entity.Property(e => e.ExpensePrice).HasColumnName("expense_price");

                entity.Property(e => e.InvoiceAddressId).HasColumnName("invoice_address_id");

                entity.Property(e => e.LastCheck)
                    .HasColumnName("last_check")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderDatetime)
                    .HasColumnName("order_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderOutId).HasColumnName("order_out_id");

                entity.Property(e => e.OrderOutRef)
                    .HasColumnName("order_out_ref")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");

                entity.Property(e => e.Payment)
                    .HasColumnName("payment")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipAddressId).HasColumnName("ship_address_id");

                entity.Property(e => e.ShipDeliveredDate)
                    .HasColumnName("ship_delivered_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShipExpectedDate)
                    .HasColumnName("ship_expected_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShipModeId).HasColumnName("ship_mode_id");

                entity.Property(e => e.ShipPrice).HasColumnName("ship_price");

                entity.Property(e => e.ShipStatusId).HasColumnName("ship_status_id");

                entity.Property(e => e.ShipUndertakenDate)
                    .HasColumnName("ship_undertaken_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Shipment)
                    .HasColumnName("shipment")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_orders_Customers");

                entity.HasOne(d => d.InvoiceAddress)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.InvoiceAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_orders_InvoiceAddresses");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_orders_statuses");

                entity.HasOne(d => d.ShipAddress)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_orders_ShipAddresses");

                entity.HasOne(d => d.ShipMode)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipModeId)
                    .HasConstraintName("FK_orders_orders_ShipModes");

                entity.HasOne(d => d.ShipStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipStatusId)
                    .HasConstraintName("FK_orders_orders_ShipStatuses");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_orders_Shops");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_orders_orders_AspNetUsers");
            });

            modelBuilder.Entity<OrderStatuses>(entity =>
            {
                entity.ToTable("order_statuses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlertTime).HasColumnName("alert_time");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Priority).HasColumnName("priority");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Depth).HasColumnName("depth");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.DescriptionShort)
                    .HasColumnName("description_short")
                    .HasColumnType("text");

                entity.Property(e => e.Ean13)
                    .HasColumnName("ean13")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.IdOut).HasColumnName("id_out");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.Isbn)
                    .HasColumnName("isbn")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");

                entity.Property(e => e.MinimalQuantity).HasColumnName("minimal_quantity");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingCost).HasColumnName("shipping_cost");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPriceRatio).HasColumnName("unit_price_ratio");

                entity.Property(e => e.Upc)
                    .HasColumnName("upc")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.Property(e => e.WolesalePrice).HasColumnName("wolesale_price");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_products_products_Images");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_products_products_Manufacturer");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_products_products_Shop");
            });

            modelBuilder.Entity<ShipAddresses>(entity =>
            {
                entity.ToTable("ship_addresses");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Default).HasColumnName("default");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.OutId).HasColumnName("out_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasColumnName("zipcode")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShipAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ship_addresses_ship_addresses_Customer");
            });

            modelBuilder.Entity<ShipModes>(entity =>
            {
                entity.ToTable("ship_modes");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShipStatuses>(entity =>
            {
                entity.ToTable("ship_statuses");

                entity.Property(e => e.AlertTime).HasColumnName("alert_time");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Priority).HasColumnName("priority");
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.ToTable("shops");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseUrl)
                    .IsRequired()
                    .HasColumnName("base_url")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastSync)
                    .HasColumnName("last_sync")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastSyncOrderId).HasColumnName("last_sync_order_id");

                entity.Property(e => e.LastSyncProdId).HasColumnName("last_sync_prod_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.ToTable("system_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Logs)
                    .IsRequired()
                    .HasColumnName("logs")
                    .HasColumnType("text");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });
        }
    }
}
