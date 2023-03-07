using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace YogIT.LargeAzFileUpload.Migrations.EntityBuilders
{
    public class LargeAzFileUploadEntityBuilder : AuditableBaseEntityBuilder<LargeAzFileUploadEntityBuilder>
    {
        private const string _entityTableName = "YogITLargeAzFileUpload";
        private readonly PrimaryKey<LargeAzFileUploadEntityBuilder> _primaryKey = new("PK_YogITLargeAzFileUpload", x => x.LargeAzFileUploadId);
        private readonly ForeignKey<LargeAzFileUploadEntityBuilder> _moduleForeignKey = new("FK_YogITLargeAzFileUpload_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public LargeAzFileUploadEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override LargeAzFileUploadEntityBuilder BuildTable(ColumnsBuilder table)
        {
            LargeAzFileUploadId = AddAutoIncrementColumn(table, "LargeAzFileUploadId");
            ModuleId = AddIntegerColumn(table, "ModuleId");
            Name = AddMaxStringColumn(table, "Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> LargeAzFileUploadId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
