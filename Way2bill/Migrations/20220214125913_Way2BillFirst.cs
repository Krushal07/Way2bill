using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Way2bill.Migrations
{
    public partial class Way2BillFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchMasters",
                columns: table => new
                {
                    Branchid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Baddress = table.Column<string>(type: "varchar(200)", nullable: false),
                    Bphone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Bcity = table.Column<string>(type: "varchar(20)", nullable: false),
                    Barea = table.Column<string>(type: "varchar(20)", nullable: false),
                    Bemail = table.Column<string>(type: "varchar(20)", nullable: false),
                    Bpassword = table.Column<string>(type: "varchar(20)", nullable: false),
                    Bqid = table.Column<int>(type: "int", nullable: false),
                    Banswer = table.Column<string>(type: "varchar(100)", nullable: false),
                    Companyid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchMasters", x => x.Branchid);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMasters",
                columns: table => new
                {
                    Companyid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Caddress = table.Column<string>(type: "varchar(200)", nullable: false),
                    Cphone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Ccity = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cemail = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cpassword = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cqid = table.Column<int>(type: "int", nullable: false),
                    Canswer = table.Column<string>(type: "varchar(100)", nullable: false),
                    Crollid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMasters", x => x.Companyid);
                });

            migrationBuilder.CreateTable(
                name: "ComplainMasters",
                columns: table => new
                {
                    Complainid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Companyid = table.Column<int>(type: "int", nullable: false),
                    Cdetails = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainMasters", x => x.Complainid);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMasters",
                columns: table => new
                {
                    Customerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customername = table.Column<string>(type: "varchar(30)", nullable: false),
                    Customeraddress = table.Column<string>(type: "varchar(200)", nullable: false),
                    Customerphone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Customeremail = table.Column<string>(type: "varchar(20)", nullable: false),
                    Branchid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMasters", x => x.Customerid);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackMasters",
                columns: table => new
                {
                    Fid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Companyid = table.Column<int>(type: "int", nullable: false),
                    Fdetails = table.Column<string>(type: "varchar(200)", nullable: false),
                    Experiencerate = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackMasters", x => x.Fid);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubCategoryMasters",
                columns: table => new
                {
                    Prodscid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prodscname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Prodcatimage = table.Column<string>(type: "varchar(200)", nullable: false),
                    Prodpriceperunit = table.Column<int>(type: "int", nullable: false),
                    Prodcatid = table.Column<int>(type: "int", nullable: false),
                    Prodscdesc = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategoryMasters", x => x.Prodscid);
                });

            migrationBuilder.CreateTable(
                name: "ProudctMainCategoryMasters",
                columns: table => new
                {
                    Prodcatid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prodcatname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Prodcatimage = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProudctMainCategoryMasters", x => x.Prodcatid);
                });

            migrationBuilder.CreateTable(
                name: "QuestionMasters",
                columns: table => new
                {
                    Qid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Questiontext = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMasters", x => x.Qid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchMasters");

            migrationBuilder.DropTable(
                name: "CompanyMasters");

            migrationBuilder.DropTable(
                name: "ComplainMasters");

            migrationBuilder.DropTable(
                name: "CustomerMasters");

            migrationBuilder.DropTable(
                name: "FeedbackMasters");

            migrationBuilder.DropTable(
                name: "ProductSubCategoryMasters");

            migrationBuilder.DropTable(
                name: "ProudctMainCategoryMasters");

            migrationBuilder.DropTable(
                name: "QuestionMasters");
        }
    }
}
