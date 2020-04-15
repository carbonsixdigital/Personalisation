using Personalisation.Core.Config;
using Personalisation.Core.Controllers;
using Personalisation.Core.Services;
using Personalisation.Web.Interfaces;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Security;

namespace Personalisation.Web.Controllers
{
    public class BaseController : PersonalisationController
    {
        public BaseController(PersonalisationConfig personalisationConfig, IPersonalisationService personalisationService, MembershipHelper membershipHelper)
            : base(personalisationConfig, personalisationService, membershipHelper)
        { }

        public override ActionResult Index(ContentModel model)
        {
            // get profile tags for the partial to display them
            var viewModel = model.Content as ISiteContent;
            viewModel.ProfileTags = base.GetTags();
            return base.Index(model);
        }
    }
}