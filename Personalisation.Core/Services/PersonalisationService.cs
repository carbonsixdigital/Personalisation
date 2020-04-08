using Personalisation.Core.Config;
using Personalisation.Core.Data.Entities;
using Personalisation.Core.Extensions;
using Personalisation.Core.Interfaces;
using Personalisation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Security;

namespace Personalisation.Core.Services
{
    public class PersonalisationService : IPersonalisationService
    {
        private readonly IPersonalisationStore _personalisationStore;
        private readonly ISessionService _sessionService;
        private readonly ILogger _logger;
        private readonly PersonalisationConfig _personalisationConfig;
        private readonly MembershipHelper _membershipHelper;

        public PersonalisationService(

            IPersonalisationStore personalisationStore,
            ISessionService sessionService,
            ILogger logger,
            PersonalisationConfig personalisationConfig,
            MembershipHelper membershipHelper)
        {
            _personalisationStore = personalisationStore ?? throw new ArgumentNullException(nameof(personalisationStore));
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personalisationConfig = personalisationConfig ?? throw new ArgumentNullException(nameof(personalisationConfig));
            _membershipHelper = membershipHelper ?? throw new ArgumentNullException(nameof(membershipHelper));

            var context = GetContext();
            if (context == null) UpsertContext();
        }

        public IEnumerable<PersonalisationTag> GetTags()
        {
            try
            {
                var context = GetContext();
                if (context == null) return null;
                if (context.Tags?.Any() ?? false) return context.Tags;
                if (context.HasStoreTags) return null;

                PersonalisationDto entity = _personalisationStore.GetById(context.MemberId);
                UpsertContext(entity?.Tags);
                context.HasStoreTags = true;

                return entity?.Tags;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationService), ex);
                return null;
            }
        }

        public void AddTags(IEnumerable<string> tags)
        {
            try
            {
                if (!tags?.Any() ?? true) return;
                var currentTags = GetTags()?.ToList();

                UpsertContext(currentTags != null
                    ? currentTags.Merge(tags)
                    : tags.Select(x => new PersonalisationTag(x)));
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationService), ex);
            }
        }

        public void ClearTags()
        {
            try
            {
                var wrapper = GetContext();
                if (wrapper == null) return;

                _sessionService.ClearSessionTags(wrapper);

                _personalisationStore.Delete(wrapper.MemberId);
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationService), ex);
            }
        }

        public IEnumerable<T> Personalise<T>(IEnumerable<T> items, bool backfill = false) where T : IPublishedContent
        {
            try
            {
                var userTags = GetTags();
                if (!userTags?.Any() ?? true)
                    return backfill ? items : null;

                userTags = userTags.OrderByDescending(x => x.Score);

                var taggedItems = items.Where(x => x.HasValue(_personalisationConfig.PropertyAlias));
                if (!taggedItems?.Any() ?? true)
                    return backfill ? items : null;

                var personalisedItems = new List<T>();
                foreach (var userTag in userTags)
                    personalisedItems.AddUnique(taggedItems
                        .Where(x => x
                            .Value<IEnumerable<string>>(_personalisationConfig.PropertyAlias)
                            .Any(y => string.Equals(y, userTag.Tag, StringComparison.OrdinalIgnoreCase))));

                return !backfill ? personalisedItems : personalisedItems.Union(items);
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(PersonalisationService), ex);
                return null;
            }
        }

        private PersonalisationContext GetContext() => _sessionService.GetSessionContext() ?? null;

        private void UpsertContext(IEnumerable<PersonalisationTag> tags = null)
        {
            var member = _membershipHelper.GetCurrentMember();
            if (member == null) return;

            var context = GetContext() ?? new PersonalisationContext() { MemberId = member.Id };

            if (tags?.Any() ?? false)
                context.Tags = tags;

            _sessionService.SetSessionContext(context);
        }


        public IEnumerable<PersonalisationTag> GetFromStore(int memberId) 
            => _personalisationStore.GetById(memberId)?.Tags;

        public void UpsertToStore()
        {
            var context = GetContext();
            if (context != null && (context.Tags?.Any() ?? false))
                _personalisationStore.Upsert(new PersonalisationDto
                {
                    MemberId = context.MemberId,
                    Tags = context.Tags.Select(x => new PersonalisationTag(x.Tag)
                    {
                        Score = x.Score
                    })
                });
        }
    }
}