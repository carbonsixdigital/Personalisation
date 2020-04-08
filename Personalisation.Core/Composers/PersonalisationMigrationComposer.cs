using Personalisation.Core.Constants;
using Personalisation.Core.Data.Entities;
using System;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;

namespace Personalisation.Core.Composers
{
    public class PersonalisationMigrationComposer : ComponentComposer<PersonalisationMigrationComponent> { }

    public class PersonalisationMigrationComponent : IComponent
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IMigrationBuilder _migrationBuilder;
        private readonly IKeyValueService _keyValueService;
        private readonly ILogger _logger;

        public PersonalisationMigrationComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            _scopeProvider = scopeProvider ?? throw new ArgumentNullException(nameof(scopeProvider));
            _migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            _keyValueService = keyValueService ?? throw new ArgumentNullException(nameof(keyValueService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize()
        {
            try
            {
                var migrationPlan = new MigrationPlan(Migrations.MigrationPlan);

                migrationPlan
                    .From(string.Empty)
                    .To<MigrationOne>(nameof(MigrationOne));

                var upgrader = new Upgrader(migrationPlan);
                upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
            }
            catch { }
        }

        public void Terminate() { }
    }
}