using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopify.Infa.Db.SqlServer.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class updateseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "4950b1af-41c6-411f-b400-9c6b1eed0323", true, "ADMIN@SHOP.COM", "09120000001", "AQAAAAIAAYagAAAAEA7UGIR4efC/g8+sP4RlqDa0+jeUvDUYc85tIFsvafUlLFCbHq9X3qWwMD1BFXgAJg==", true, "32652055-9209-443a-a89e-d80c630b754a", "09120000001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "9c668d3c-aa67-4959-864b-9d085e0cc148", true, "OPERATOR@SHOP.COM", "09120000002", "AQAAAAIAAYagAAAAEJrIQxpa8LbO3USMm912RyAGtKXY9LNR8t/ukN9TdkNil52brbgGTk7kWD7UAepcpQ==", true, "0637c884-1242-4454-bb4a-eb0a5f582b95", "09120000002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8058d72-3d00-4413-8ccd-2d407793b36d", "AQAAAAIAAYagAAAAEPYIC/m6xnNBIF7TtgGDjKI1iEnXQzyWpBUV2ohIcmWYjdLtbAl2X5Zw20ATEErgrg==", "6c6efbac-8cc4-4e6f-a51b-eba76878142b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "460909ea-a22a-4791-add2-8d934a4c2b63", true, "USER2@GMAIL.COM", "09120000004", "AQAAAAIAAYagAAAAELdsolWU53cGEJnbu+V8tpRedF06NTcAox6LY2raua66gs/pxb4hpZe7S6vaZW9elQ==", true, "1e562ad7-83ce-485a-b0a1-9457d08e358b", "09120000004" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "55afe35e-de4f-4fa0-a812-6904f3cbc3c0", true, "USER3@GMAIL.COM", "09120000005", "AQAAAAIAAYagAAAAEPXDDhabiNQ8m3VPb7RfhUtyh7T6nl4CAj3tSvDr9EmtigSWoL5vyunogdtvZXamIA==", true, "3ccf0352-0c46-4125-b34f-8f354f77f34b", "09120000005" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "0409474a-f88b-40d8-b99a-293b1c94855b", false, null, null, "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", false, null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "08ccda06-d076-4fbc-b5e0-0537c7b39082", false, null, null, "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", false, null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b23aee1-2ec8-479e-b424-2e38a583c024", "AQAAAAIAAYagAAAAEM+r3IaOS3AtujPhWt60MI6fjTKd4uoOf5a+9OV/33T6yFNGPOzSGW+ECsxz/etlfw==", "faf25c8b-87c3-4b41-ade3-0055c2fe6580" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "aaff5523-77f5-493e-ad92-5e76c16af58a", false, null, null, "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", false, null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "UserName" },
                values: new object[] { "43a126c3-3682-41c7-a5d3-819288809676", false, null, null, "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", false, null, null });
        }
    }
}
