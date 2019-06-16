using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cms.NetCore.Admin.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    PassWord = table.Column<string>(nullable: false),
                    LogInCount = table.Column<int>(nullable: false),
                    LastLoginIp = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RealName = table.Column<string>(maxLength: 20, nullable: false),
                    HeadImgUrl = table.Column<string>(nullable: true),
                    Mobilephone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true),
                    UserLoginId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserManager_UserManager_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserManager_UserManager_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserManager_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buttion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Icon = table.Column<string>(maxLength: 100, nullable: false),
                    Sort = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buttion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buttion_UserManager_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buttion_UserManager_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Sort = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_UserManager_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_Department_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_UserManager_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    ParentID = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Icon = table.Column<string>(maxLength: 100, nullable: false),
                    BaseUrl = table.Column<string>(maxLength: 200, nullable: false),
                    Sort = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_UserManager_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menu_UserManager_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationalLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Controller = table.Column<string>(maxLength: 30, nullable: false),
                    Action = table.Column<string>(maxLength: 20, nullable: false),
                    UserManagerId = table.Column<Guid>(nullable: false),
                    OperationalTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    OperationalIp = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationalLog_UserManager_UserManagerId",
                        column: x => x.UserManagerId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RoleName = table.Column<string>(maxLength: 20, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_UserManager_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Role_UserManager_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserManagerId = table.Column<Guid>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDepartment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDepartment_UserManager_UserManagerId",
                        column: x => x.UserManagerId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuButton",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false),
                    ButtonId = table.Column<Guid>(nullable: false),
                    ButtionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuButton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuButton_Buttion_ButtionId",
                        column: x => x.ButtionId,
                        principalTable: "Buttion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuButton_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserManagerId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_UserManager_UserManagerId",
                        column: x => x.UserManagerId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenuButton",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    MenuButtonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenuButton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenuButton_MenuButton_MenuButtonId",
                        column: x => x.MenuButtonId,
                        principalTable: "MenuButton",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenuButton_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buttion_CreateUserId",
                table: "Buttion",
                column: "CreateUserId",
                unique: true,
                filter: "[CreateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buttion_UpdateUserId",
                table: "Buttion",
                column: "UpdateUserId",
                unique: true,
                filter: "[UpdateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreateUserId",
                table: "Department",
                column: "CreateUserId",
                unique: true,
                filter: "[CreateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentId",
                table: "Department",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_UpdateUserId",
                table: "Department",
                column: "UpdateUserId",
                unique: true,
                filter: "[UpdateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_CreateUserId",
                table: "Menu",
                column: "CreateUserId",
                unique: true,
                filter: "[CreateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentID",
                table: "Menu",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_UpdateUserId",
                table: "Menu",
                column: "UpdateUserId",
                unique: true,
                filter: "[UpdateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MenuButton_ButtionId",
                table: "MenuButton",
                column: "ButtionId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuButton_MenuId",
                table: "MenuButton",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationalLog_UserManagerId",
                table: "OperationalLog",
                column: "UserManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreateUserId",
                table: "Role",
                column: "CreateUserId",
                unique: true,
                filter: "[CreateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdateUserId",
                table: "Role",
                column: "UpdateUserId",
                unique: true,
                filter: "[UpdateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuButton_MenuButtonId",
                table: "RoleMenuButton",
                column: "MenuButtonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuButton_RoleId",
                table: "RoleMenuButton",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_DepartmentId",
                table: "UserDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_UserManagerId",
                table: "UserDepartment",
                column: "UserManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserManager_CreateUserId",
                table: "UserManager",
                column: "CreateUserId",
                unique: true,
                filter: "[CreateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserManager_UpdateUserId",
                table: "UserManager",
                column: "UpdateUserId",
                unique: true,
                filter: "[UpdateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserManager_UserLoginId",
                table: "UserManager",
                column: "UserLoginId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserManagerId",
                table: "UserRole",
                column: "UserManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationalLog");

            migrationBuilder.DropTable(
                name: "RoleMenuButton");

            migrationBuilder.DropTable(
                name: "UserDepartment");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "MenuButton");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Buttion");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "UserManager");

            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
