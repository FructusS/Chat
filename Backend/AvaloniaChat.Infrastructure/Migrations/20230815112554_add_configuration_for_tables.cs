using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AvaloniaChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_configuration_for_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_user_admin",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups");

            migrationBuilder.RenameColumn(
                name: "UsergroupId",
                table: "UserGroups",
                newName: "UserGroupId");

            migrationBuilder.RenameColumn(
                name: "user_admin",
                table: "Groups",
                newName: "UserAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_user_admin",
                table: "Groups",
                newName: "IX_Groups_UserAdmin");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "UserGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(0) without time zone");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ExpiresIn", "FirstName", "LastName", "Logo", "PasswordHash", "RefreshToken", "Username" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "$2a$11$ItpF9E/CZC2Lb1WSBd46QeXpeUuZqeyckw7baie0dX9gjlza6CI9.", null, "test1" },
                    { 2, null, null, null, null, "$2a$11$nJ1VFBMHFEl9mRd/Uhb7gukx18UxjHpA1gQF3Vmh.mr1w2dUHzYqy", null, "test2" },
                    { 3, null, null, null, null, "$2a$11$bCaBT8I3hZI5uUhnOESD2.DiSIjQuTwBlwFb0WmwuvSXMseIevNIa", null, "test3" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "GroupImage", "GroupTitle", "UserAdmin" },
                values: new object[,]
                {
                    { new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"), null, "Group 2", 2 },
                    { new Guid("f66475d3-7ea1-422c-b658-353a96732d14"), null, "Group 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "UserGroupId", "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"), 1 },
                    { 2, new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"), 2 },
                    { 3, new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"), 3 },
                    { 4, new Guid("f66475d3-7ea1-422c-b658-353a96732d14"), 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserAdmin",
                table: "Groups",
                column: "UserAdmin",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserAdmin",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "UserGroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "UserGroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "UserGroupId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "UserGroupId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: new Guid("f66475d3-7ea1-422c-b658-353a96732d14"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "UserGroupId",
                table: "UserGroups",
                newName: "UsergroupId");

            migrationBuilder.RenameColumn(
                name: "UserAdmin",
                table: "Groups",
                newName: "user_admin");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_UserAdmin",
                table: "Groups",
                newName: "IX_Groups_user_admin");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserGroups",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "UserGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp(0) without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_user_admin",
                table: "Groups",
                column: "user_admin",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
