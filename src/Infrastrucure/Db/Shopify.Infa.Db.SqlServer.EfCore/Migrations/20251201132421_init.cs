using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shopify.Infa.Db.SqlServer.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Plaque = table.Column<int>(type: "int", nullable: false),
                    UnitNumber = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GuestId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "ParentId", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Electronics", null, null });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Color", null },
                    { 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Size", null },
                    { 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Weight", null },
                    { 4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Material", null },
                    { 5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Model", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CreatedAt", "Email", "FirstName", "ImgUrl", "IsDeleted", "LastName", "PasswordHash", "Phone", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 0m, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@shop.com", "Admin", "default.jpg", false, "User", "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", "09120000001", 3, null },
                    { 2, 0m, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "operator@shop.com", "Operator", "default.jpg", false, "User", "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", "09120000002", 2, null },
                    { 3, 0m, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@gmail.com", "Ali", "default.jpg", false, "Ahmadi", "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", "09120000003", 1, null },
                    { 4, 0m, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@gmail.com", "Reza", "default.jpg", false, "Moradi", "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", "09120000004", 1, null },
                    { 5, 0m, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3@gmail.com", "Sara", "default.jpg", false, "Karimi", "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", "09120000005", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "CreatedAt", "IsDefault", "IsDeleted", "Name", "Plaque", "PostalCode", "Province", "Street", "UnitNumber", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Tehran", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Admin Home", 0, "1111111111", "Tehran", "Valiasr Street, No. 120", 0, null, 1 },
                    { 2, "Karaj", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Operator Office", 0, "2222222222", "Tehran", "Azadi Blvd, No. 45", 0, null, 2 },
                    { 3, "Isfahan", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Home", 0, "3333333333", "Isfahan", "Chaharbagh Abbasi, No. 87", 0, null, 3 },
                    { 4, "Shiraz", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Home", 0, "4444444444", "Shiraz", "Zand Street, No. 51", 0, null, 4 },
                    { 5, "Tabriz", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Home", 0, "5555555555", "Tabriz", "Shariati Street, No. 20", 0, null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "ParentId", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Laptop", 1, null },
                    { 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Mobile", 1, null },
                    { 4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Headphones", 1, null },
                    { 5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Smart Watch", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "IsFinalized", "Status", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1, 28000000m, null, 3 },
                    { 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 2, 15500000m, null, 4 },
                    { 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1, 9000000m, null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ImageUrl", "IsActive", "IsDeleted", "IsSpecial", "Price", "ShortDescription", "StockQuantity", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "iphone14pro.jpg", true, false, true, 120000000m, "Apple flagship smartphone with A16 chip", 12, "iPhone 14 Pro", null },
                    { 2, 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "s23.jpg", true, false, false, 95000000m, "Latest Samsung flagship with Snapdragon processor", 20, "Samsung Galaxy S23", null },
                    { 3, 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "macbookm2.jpg", true, false, true, 150000000m, "Apple Macbook Pro with M2 processor", 8, "Macbook Pro M2", null },
                    { 4, 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "dellxps13.jpg", true, false, false, 110000000m, "Lightweight premium ultrabook", 14, "Dell XPS 13", null },
                    { 5, 4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "airpodspro2.jpg", true, false, true, 8500000m, "Apple wireless noise-cancelling earbuds", 50, "AirPods Pro 2", null },
                    { 6, 4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "xm5.jpg", true, false, false, 12000000m, "Sony premium noise-cancelling headphones", 25, "Sony WH-1000XM5", null },
                    { 7, 5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "watch6.jpg", true, false, false, 9000000m, "Smart watch with health and fitness tracking", 30, "Samsung Galaxy Watch 6", null },
                    { 8, 5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "applewatch9.jpg", true, false, true, 18000000m, "Latest Apple Watch with powerful S9 chip", 18, "Apple Watch Series 9", null },
                    { 9, 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "rogphone7.jpg", true, false, false, 75000000m, "Gaming smartphone with powerful specs", 10, "Asus ROG Phone 7", null },
                    { 10, 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "redminote12.jpg", true, false, false, 12000000m, "Budget friendly smartphone with great features", 40, "Xiaomi Redmi Note 12", null }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "OrderId", "ProductId", "Quantity", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, 1, 20000000m, null },
                    { 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 4, 1, 8000000m, null },
                    { 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 2, 1, 15000000m, null },
                    { 4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 10, 1, 500000m, null },
                    { 5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 3, 1, 12000000m, null },
                    { 6, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 7, 1, 3000000m, null }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributeValues",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ProductAttributeId", "ProductId", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, null, "Red" },
                    { 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, null, "Blue" },
                    { 3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 1, null, "M" },
                    { 4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 1, null, "L" },
                    { 5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, 2, null, "XPS 13" },
                    { 6, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 2, null, "1.2kg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeId",
                table: "ProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
