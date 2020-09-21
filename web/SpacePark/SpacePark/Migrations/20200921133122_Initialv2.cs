using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark.Migrations
{
    public partial class Initialv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLot_SpaceShips_SpaceShipID",
                table: "ParkingLot");

            migrationBuilder.DropForeignKey(
                name: "FK_People_SpaceShips_SpaceShipID",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaceShips",
                table: "SpaceShips");

            migrationBuilder.DropIndex(
                name: "IX_People_SpaceShipID",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingLot",
                table: "ParkingLot");

            migrationBuilder.RenameTable(
                name: "SpaceShips",
                newName: "Spaceships");

            migrationBuilder.RenameTable(
                name: "ParkingLot",
                newName: "Parkinglot");

            migrationBuilder.RenameColumn(
                name: "SpaceShipID",
                table: "Spaceships",
                newName: "SpaceshipID");

            migrationBuilder.RenameColumn(
                name: "SpaceShipID",
                table: "People",
                newName: "SpaceshipID");

            migrationBuilder.RenameColumn(
                name: "SpaceShipID",
                table: "Parkinglot",
                newName: "SpaceshipID");

            migrationBuilder.RenameColumn(
                name: "ParkingLotID",
                table: "Parkinglot",
                newName: "ParkinglotID");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingLot_SpaceShipID",
                table: "Parkinglot",
                newName: "IX_Parkinglot_SpaceshipID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spaceships",
                table: "Spaceships",
                column: "SpaceshipID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parkinglot",
                table: "Parkinglot",
                column: "ParkinglotID");

            migrationBuilder.CreateIndex(
                name: "IX_People_SpaceshipID",
                table: "People",
                column: "SpaceshipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parkinglot_Spaceships_SpaceshipID",
                table: "Parkinglot",
                column: "SpaceshipID",
                principalTable: "Spaceships",
                principalColumn: "SpaceshipID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Spaceships_SpaceshipID",
                table: "People",
                column: "SpaceshipID",
                principalTable: "Spaceships",
                principalColumn: "SpaceshipID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parkinglot_Spaceships_SpaceshipID",
                table: "Parkinglot");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Spaceships_SpaceshipID",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spaceships",
                table: "Spaceships");

            migrationBuilder.DropIndex(
                name: "IX_People_SpaceshipID",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parkinglot",
                table: "Parkinglot");

            migrationBuilder.RenameTable(
                name: "Spaceships",
                newName: "SpaceShips");

            migrationBuilder.RenameTable(
                name: "Parkinglot",
                newName: "ParkingLot");

            migrationBuilder.RenameColumn(
                name: "SpaceshipID",
                table: "SpaceShips",
                newName: "SpaceShipID");

            migrationBuilder.RenameColumn(
                name: "SpaceshipID",
                table: "People",
                newName: "SpaceShipID");

            migrationBuilder.RenameColumn(
                name: "SpaceshipID",
                table: "ParkingLot",
                newName: "SpaceShipID");

            migrationBuilder.RenameColumn(
                name: "ParkinglotID",
                table: "ParkingLot",
                newName: "ParkingLotID");

            migrationBuilder.RenameIndex(
                name: "IX_Parkinglot_SpaceshipID",
                table: "ParkingLot",
                newName: "IX_ParkingLot_SpaceShipID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaceShips",
                table: "SpaceShips",
                column: "SpaceShipID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingLot",
                table: "ParkingLot",
                column: "ParkingLotID");

            migrationBuilder.CreateIndex(
                name: "IX_People_SpaceShipID",
                table: "People",
                column: "SpaceShipID",
                unique: true,
                filter: "[SpaceShipID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLot_SpaceShips_SpaceShipID",
                table: "ParkingLot",
                column: "SpaceShipID",
                principalTable: "SpaceShips",
                principalColumn: "SpaceShipID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_SpaceShips_SpaceShipID",
                table: "People",
                column: "SpaceShipID",
                principalTable: "SpaceShips",
                principalColumn: "SpaceShipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
