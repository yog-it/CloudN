using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using YogIT.LargeAzFileUpload.Migrations.EntityBuilders;
using YogIT.LargeAzFileUpload.Repository;

namespace YogIT.LargeAzFileUpload.Migrations
{
    [DbContext(typeof(LargeAzFileUploadContext))]
    [Migration("YogIT.LargeAzFileUpload.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new LargeAzFileUploadEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new LargeAzFileUploadEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}
