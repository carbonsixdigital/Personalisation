using Personalisation.Web.Interfaces;
using System.Collections.Generic;
using Umbraco.Web;

namespace Personalisation.Web.Models.DocumentTypes
{
	public partial class Master : INavigation
	{
	   IEnumerable<ISiteContent> INavigation.MenuItems => this.AncestorOrSelf<Homepage>()?.MenuItems;
	}
}