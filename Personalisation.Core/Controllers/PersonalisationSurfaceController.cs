using Personalisation.Core.Services;
using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Personalisation.Core.Controllers
{
    [PluginController("personalisation")]
    public class PersonalisationSurfaceController : SurfaceController
    {
        private readonly IPersonalisationService _personalisationService;

        public PersonalisationSurfaceController(IPersonalisationService personalisationService)         
        {
            _personalisationService = personalisationService ?? throw new ArgumentNullException(nameof(personalisationService));
        }
             
        public ActionResult RemoveProfileTags()
        {
            _personalisationService.ClearTags();
            return Redirect("~/"); 
        }

        public ActionResult AbandonSession()
        {
            Session.Abandon();
            //  Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            //  Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);
            return Redirect("~/");
        }

        public ActionResult ClearSession()
        {
            _personalisationService.UpsertToStore();

            TempData.Clear();
            Session.Clear();
            return Redirect("~/");
        }
    }
}