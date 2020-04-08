using Personalisation.Web.Interfaces;
using Personalisation.Web.Models.DocumentTypes;
using Personalisation.Core.Services;
using System;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Personalisation.Core.Config;
using Umbraco.Web.Security;

namespace Personalisation.Web.Controllers
{
    public class HomepageController : BaseController
    {
        public HomepageController(PersonalisationConfig personalisationConfig, IPersonalisationService personalisationService, MembershipHelper membershipHelper)
            : base(personalisationConfig, personalisationService, membershipHelper)
        { }

        public ActionResult Index(ContentModel<Homepage> model)
        {
#if DEBUG
            // for demo purposes
            if (!Members.IsLoggedIn())
                Members.Login("test@carbonsix.digital", ">;#jRwxjg(");
#endif

            var viewModel = model.Content as IHomepage;

            viewModel.PersonalisedPromotions = base.Personalise(viewModel.Promotions, true);
            viewModel.PersonalisedPromotionsWithoutBackfill = base.Personalise(viewModel.Promotions);
            viewModel.Pages = base.Personalise(((INavigation)viewModel)?.MenuItems, true);

            return base.Index(model);
        }
    }
}