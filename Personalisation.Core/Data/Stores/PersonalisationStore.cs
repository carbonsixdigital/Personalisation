using Personalisation.Core.Data.Entities;
using Personalisation.Core.Interfaces;
using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using System.Linq;
using Umbraco.Core.Logging;

namespace Personalisation.Core.Data.Stores
{
    public class PersonalisationStore : IPersonalisationStore
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly ILogger _logger;

        public PersonalisationStore(IScopeProvider scopeProvider, ILogger logger)
        {
            _scopeProvider = scopeProvider ?? throw new ArgumentNullException(nameof(scopeProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PersonalisationDto GetById(int id)
        {
            try
            {
                using (var scope = _scopeProvider.CreateScope(autoComplete: true))
                {
                    var entity = scope.Database.SingleOrDefaultById<PersonalisationTagStoreSchema>(id);
                    if (entity == null) return null;

                    return new PersonalisationDto
                    {
                        MemberId = entity.MemberId,
                        Tags = entity.Tags?.Select(x => new Models.PersonalisationTag(x.Tag) { Score = x.Score })
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationStore), ex);
                throw;
            }
        }

        public void Upsert(PersonalisationDto dto)
        {
            try
            {
                using (var scope = _scopeProvider.CreateScope(autoComplete: true))
                {
                    scope.Database.Save(new PersonalisationTagStoreSchema
                    {
                        MemberId = dto.MemberId,
                        Tags = dto.Tags?.Select(x => new PersonalisationTagSchema { Tag = x.Tag, Score = x.Score })?.ToList(),
                        Updated = DateTime.UtcNow
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationStore), ex);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var scope = _scopeProvider.CreateScope(autoComplete: true))
                {
                    scope.Database.Delete<PersonalisationTagStoreSchema>(id);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationStore), ex);
                throw;
            }
        }
    }
}