using Personalisation.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace Personalisation.Web.Composers
{
    public class BaseControllerComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.SetDefaultRenderMvcController(typeof(BaseController));
        }
    }
}