using Personalisation.Web.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web;

namespace Personalisation.Web.Models.DocumentTypes
{
    public partial class Homepage : IHomepage
    {
        public IEnumerable<ISiteContent> MenuItems => PrimaryNavigation?.OfType<ISiteContent>()?.Where(x => x.IsVisible());
  
        IEnumerable<Promotion> IHomepage.Promotions => this.Promotions.OfType<Promotion>().Where(x => x.IsVisible());    
        public IEnumerable<Promotion> PersonalisedPromotions { get; set; }
        public IEnumerable<Promotion> PersonalisedPromotionsWithoutBackfill { get; set; }
        bool IHomepage.HasPromotions => Promotions?.Any() ?? false;
        bool IHomepage.HasPersonalisedPromotions => PersonalisedPromotions?.Any() ?? false;
        bool IHomepage.HasPersonalisedPromotionsWithoutBackfill => PersonalisedPromotionsWithoutBackfill?.Any() ?? false;

        public IEnumerable<ISiteContent> Pages { get; set; }
        bool IHomepage.HasPages => Pages?.Any() ?? false;
    }
}