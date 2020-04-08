using Personalisation.Core.Models;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Personalisation.Core.Services
{
    public interface IPersonalisationService
    {
        IEnumerable<PersonalisationTag> GetTags();
        void AddTags(IEnumerable<string> tags);
        void ClearTags();      
        IEnumerable<T> Personalise<T>(IEnumerable<T> items, bool backfill = false) where T : IPublishedContent;
        IEnumerable<PersonalisationTag> GetFromStore(int memberId);
        void UpsertToStore();
    }
}