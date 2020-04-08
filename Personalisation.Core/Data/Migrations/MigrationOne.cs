using Umbraco.Core.Migrations;

namespace Personalisation.Core.Data.Entities
{
    public partial class MigrationOne : MigrationBase
    {
        public MigrationOne(IMigrationContext context) : base(context) { }

        public override void Migrate()
        {
            Logger.Debug(typeof(MigrationOne), "Running migration {MigrationStep}");
         
            if (!TableExists("PersonalisationTagStore"))
                Create.Table<PersonalisationTagStoreSchema>().Do();
            else
                Logger.Debug(typeof(MigrationOne), "The database table {DbTable} already exists, skipping");
        }
    }   
}