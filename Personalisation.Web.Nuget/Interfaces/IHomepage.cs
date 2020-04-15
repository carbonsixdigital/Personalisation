using Personalisation.Web.Models.DocumentTypes;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Personalisation.Web.Interfaces
{
    public interface IHomepage : ISiteContent
    {
        IEnumerable<Promotion> Promotions { get; }
       
        IEnumerable<Promotion> PersonalisedPromotions { get; set; }

        IEnumerable<Promotion> PersonalisedPromotionsWithoutBackfill { get; set; }

        IEnumerable<ISiteContent> Pages { get; set; } 

        bool HasPromotions { get; } 
        bool HasPersonalisedPromotions { get; }
        bool HasPersonalisedPromotionsWithoutBackfill { get; }
        bool HasPages { get; }
    }
}
