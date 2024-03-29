using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace YogIT.Module.CloudN.Migrations.EntityBuilders
{
    public class CloudNEntityBuilder : AuditableBaseEntityBuilder<CloudNEntityBuilder>
    {
        private const string _entityTableName = "YogITCloudN";
        private readonly PrimaryKey<CloudNEntityBuilder> _primaryKey = new("PK_YogITCloudN", x => x.CloudNId);
        private readonly ForeignKey<CloudNEntityBuilder> _moduleForeignKey = new("FK_YogITCloudN_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public CloudNEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override CloudNEntityBuilder BuildTable(ColumnsBuilder table)
        {
            CloudNId = AddAutoIncrementColumn(table,"CloudNId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> CloudNId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
