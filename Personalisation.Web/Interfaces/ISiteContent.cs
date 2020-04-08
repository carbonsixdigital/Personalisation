using Personalisation.Core.Models;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Personalisation.Web.Interfaces
{
    public interface ISiteContent : IPublishedContent
    {
        string Title { get; }
        string FriendlyTitle { get; }
        bool HideChildrenFromNavigation { get; }
        int CachedPartialDuration { get; set; }
        string CachePrefix { get; set; }
        bool StaticHeader { get; set; }
        string IsAncestorOrSelfWithClass(IPublishedContent currentPage, string className);
        
        IEnumerable<string> Tags { get; } 
        IEnumerable<PersonalisationTag> ProfileTags { get; set; } 
    }
}