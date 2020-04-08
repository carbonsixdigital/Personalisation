using Personalisation.Core.Config;
using Personalisation.Core.Models;
using Personalisation.Core.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.Security;

namespace Personalisation.Core.Controllers
{
    public class PersonalisationController : RenderMvcController
    {
        private readonly PersonalisationConfig _personalisationConfig;
        private readonly IPersonalisationService _personalisationService;
        private readonly MembershipHelper _membershipHelper;

        public PersonalisationController(PersonalisationConfig personalisationConfig, IPersonalisationService personalisationService, MembershipHelper membershipHelper)
        {
            _personalisationConfig = personalisationConfig ?? throw new ArgumentNullException(nameof(personalisationConfig));
            _personalisationService = personalisationService ?? throw new ArgumentNullException(nameof(personalisationService));        
            _membershipHelper = membershipHelper ?? throw new ArgumentNullException(nameof(membershipHelper));
        }

        public override ActionResult Index(ContentModel model)
        {
            if (_membershipHelper.IsLoggedIn())
                if (model.Content.HasValue(_personalisationConfig.PropertyAlias))
                    _personalisationService.AddTags(model.Content.Value<IEnumerable<string>>(_personalisationConfig.PropertyAlias));

            return base.Index(model);
        }

        public IEnumerable<PersonalisationTag> GetTags() =>
            _personalisationService.GetTags();

        public IEnumerable<T> Personalise<T>(IEnumerable<T> items, bool backfill = false) where T : IPublishedContent
            => _personalisationService.Personalise<T>(items, backfill);
    }
}