using Personalisation.Web.Interfaces;
using Personalisation.Core.Models;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Personalisation.Web.Models.DocumentTypes
{
    public partial class Master : ISiteContent
    {
        public int CachedPartialDuration { get; set; }
        public string CachePrefix { get; set; }
        public bool StaticHeader { get; set; }

        string ISiteContent.Title => this.Value<string>("title").IfNullOrWhiteSpace(Name);
        string ISiteContent.FriendlyTitle => this.Value<string>("title").IfNullOrWhiteSpace(this.Name).ToLower().ToFriendlyName();
        bool ISiteContent.HideChildrenFromNavigation => false;
        string ISiteContent.IsAncestorOrSelfWithClass(IPublishedContent currentPage, string className) => this.IsAncestorOrSelf(currentPage) ? className : string.Empty;

        IEnumerable<string> ISiteContent.Tags => this.Value<IEnumerable<string>>("tags");
        public IEnumerable<PersonalisationTag> ProfileTags { get; set; }
    }
}