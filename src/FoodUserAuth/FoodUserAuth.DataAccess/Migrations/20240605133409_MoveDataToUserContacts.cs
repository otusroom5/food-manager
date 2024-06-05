using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodUserAuth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MoveDataToUserContacts : Migration
    {
        private const string SqlMove = "insert into \"UserContacts\" (\"Id\", \"UserId\", \"ContactType\", \"Contact\") select gen_random_uuid(), \"Id\", 0, \"Email\" from \"Users\"";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlMove);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
