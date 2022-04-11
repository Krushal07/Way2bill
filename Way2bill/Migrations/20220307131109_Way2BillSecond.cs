using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Way2bill.Migrations
{
    public partial class Way2BillSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prodcatname",
                table: "ProudctMainCategoryMasters",
                newName: "Catname");

            migrationBuilder.RenameColumn(
                name: "Prodcatimage",
                table: "ProudctMainCategoryMasters",
                newName: "Catimage");

            migrationBuilder.RenameColumn(
                name: "Prodcatid",
                table: "ProudctMainCategoryMasters",
                newName: "Catid");

            migrationBuilder.RenameColumn(
                name: "Prodscname",
                table: "ProductSubCategoryMasters",
                newName: "Scname");

            migrationBuilder.RenameColumn(
                name: "Prodscdesc",
                table: "ProductSubCategoryMasters",
                newName: "Scimage");

            migrationBuilder.RenameColumn(
                name: "Prodpriceperunit",
                table: "ProductSubCategoryMasters",
                newName: "Scpriceperunit");

            migrationBuilder.RenameColumn(
                name: "Prodcatimage",
                table: "ProductSubCategoryMasters",
                newName: "Scdesc");

            migrationBuilder.RenameColumn(
                name: "Prodcatid",
                table: "ProductSubCategoryMasters",
                newName: "Catid");

            migrationBuilder.RenameColumn(
                name: "Prodscid",
                table: "ProductSubCategoryMasters",
                newName: "Scid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Catname",
                table: "ProudctMainCategoryMasters",
                newName: "Prodcatname");

            migrationBuilder.RenameColumn(
                name: "Catimage",
                table: "ProudctMainCategoryMasters",
                newName: "Prodcatimage");

            migrationBuilder.RenameColumn(
                name: "Catid",
                table: "ProudctMainCategoryMasters",
                newName: "Prodcatid");

            migrationBuilder.RenameColumn(
                name: "Scpriceperunit",
                table: "ProductSubCategoryMasters",
                newName: "Prodpriceperunit");

            migrationBuilder.RenameColumn(
                name: "Scname",
                table: "ProductSubCategoryMasters",
                newName: "Prodscname");

            migrationBuilder.RenameColumn(
                name: "Scimage",
                table: "ProductSubCategoryMasters",
                newName: "Prodscdesc");

            migrationBuilder.RenameColumn(
                name: "Scdesc",
                table: "ProductSubCategoryMasters",
                newName: "Prodcatimage");

            migrationBuilder.RenameColumn(
                name: "Catid",
                table: "ProductSubCategoryMasters",
                newName: "Prodcatid");

            migrationBuilder.RenameColumn(
                name: "Scid",
                table: "ProductSubCategoryMasters",
                newName: "Prodscid");
        }
    }
}
