using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotDonkeyApp_UG.Migrations
{
    public partial class addedNotDonkeyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotDonkeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerPhoneNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    AnimalType = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(maxLength: 15, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    AdditionalDescription = table.Column<string>(maxLength: 2137, nullable: false),
                    IsDonkey = table.Column<bool>(nullable: false),
                    AnimalsYouLike = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotDonkeys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotDonkeys");
        }
    }
}
