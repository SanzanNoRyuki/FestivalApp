using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FestivalApp.DAL.Migrations
{
    public partial class Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Country", "Genre", "LongDescription", "Name", "PhotoPath", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"), "Great Britain", "Rock", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "Freddy Mercury", "pic/freddy.jpg", "Mercury defied the conventions of a rock frontman, with his highly theatrical style influencing theartistic direction of Queen." },
                    { new Guid("5469990b-d1f9-49a6-ae50-800270d77b88"), "Great Britain", "Rock", "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB", "Queen", "pic/Queen.png", "Short description..." }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a"), new Guid("d6a8bbe4-ecbb-43c9-b9ea-e6ce7a734f4d"), "e@mail.com", "John Doe", "+42 987 654 321" },
                    { new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110"), new Guid("66c446a1-2447-4f58-aecd-e385c02217b9"), "milos@zeman.cz", "Milos Zeman", "+42 000 000 000" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "City", "Country", "PostalCode", "State", "UserId" },
                values: new object[,]
                {
                    { new Guid("d6a8bbe4-ecbb-43c9-b9ea-e6ce7a734f4d"), "Bozetechova", "2/1", "Brno", "Czechia", "612 00", "Jihomoravsky", new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a") },
                    { new Guid("66c446a1-2447-4f58-aecd-e385c02217b9"), "Hradčany", null, "Prague", "Czechia", "119 08", "Prazsky", new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110") }
                });

            migrationBuilder.InsertData(
                table: "Festivals",
                columns: new[] { "Id", "Capacity", "End", "Start", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"), 10000L, new DateTime(2021, 6, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), "Rock For People", new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a") },
                    { new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"), 10000L, new DateTime(2021, 7, 7, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), "Pohoda", new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a") }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Description", "FestivalId", "Name", "PhotoPath" },
                values: new object[,]
                {
                    { new Guid("40d3eb9b-8a13-46b4-815e-a0dfb1bef9be"), "Stage east of gate", new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"), "Stage East", "stages/east.png" },
                    { new Guid("fa804f29-e366-4018-b38b-71af32d8142f"), "Stage west of gate", new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"), "Stage West", "stages/west.png" },
                    { new Guid("75fcfe46-c596-4d93-aa11-89574b2c7574"), "Only stage", new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"), "Main Stage", "stages/main.png" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "FestivalId", "Length", "PriceAmount", "PriceCurrency", "StartDate", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a"), new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"), 1, 1500m, 2, new DateTime(2021, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110") },
                    { new Guid("ff603e7c-9ee3-4e54-8d3f-6ef2796e9113"), new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"), 1, 3000m, 2, new DateTime(2021, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a") },
                    { new Guid("ef603e7c-9ee3-4e54-8d3f-6ef2796e9111"), new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"), 0, 30m, 1, new DateTime(2021, 7, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110") }
                });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "ArtistId", "EndPlaying", "LengthOfPlaying", "StageId", "StartPlaying" },
                values: new object[] { new Guid("3c0cb3fa-2be5-408a-af45-f258a5d3df00"), new Guid("5469990b-d1f9-49a6-ae50-800270d77b88"), new DateTime(2021, 6, 10, 22, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0), new Guid("40d3eb9b-8a13-46b4-815e-a0dfb1bef9be"), new DateTime(2021, 6, 10, 20, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "ArtistId", "EndPlaying", "LengthOfPlaying", "StageId", "StartPlaying" },
                values: new object[] { new Guid("8837e270-2243-4f59-98ba-b2189dcae3ce"), new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"), new DateTime(2021, 6, 10, 18, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 40, 0, 0), new Guid("fa804f29-e366-4018-b38b-71af32d8142f"), new DateTime(2021, 6, 10, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "ArtistId", "EndPlaying", "LengthOfPlaying", "StageId", "StartPlaying" },
                values: new object[] { new Guid("7714e9e2-8bba-49d3-b72f-184d94afa736"), new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"), new DateTime(2021, 7, 4, 19, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 30, 0, 0), new Guid("75fcfe46-c596-4d93-aa11-89574b2c7574"), new DateTime(2021, 7, 4, 18, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("66c446a1-2447-4f58-aecd-e385c02217b9"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("d6a8bbe4-ecbb-43c9-b9ea-e6ce7a734f4d"));

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: new Guid("3c0cb3fa-2be5-408a-af45-f258a5d3df00"));

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: new Guid("7714e9e2-8bba-49d3-b72f-184d94afa736"));

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: new Guid("8837e270-2243-4f59-98ba-b2189dcae3ce"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("ef603e7c-9ee3-4e54-8d3f-6ef2796e9111"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("ff603e7c-9ee3-4e54-8d3f-6ef2796e9113"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("5469990b-d1f9-49a6-ae50-800270d77b88"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("40d3eb9b-8a13-46b4-815e-a0dfb1bef9be"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("75fcfe46-c596-4d93-aa11-89574b2c7574"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("fa804f29-e366-4018-b38b-71af32d8142f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110"));

            migrationBuilder.DeleteData(
                table: "Festivals",
                keyColumn: "Id",
                keyValue: new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"));

            migrationBuilder.DeleteData(
                table: "Festivals",
                keyColumn: "Id",
                keyValue: new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a"));
        }
    }
}
