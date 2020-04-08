using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace Personalisation.Web.Interfaces
{
    public interface INavigation : IPublishedContent
    {
        IEnumerable<ISiteContent> MenuItems { get; }
    }
}
