using Personalisation.Core.Config;
using Personalisation.Core.Data.Entities;
using Personalisation.Core.Interfaces;
using Personalisation.Core.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Logging;

namespace Personalisation.Core
{
    public class Global : Umbraco.Web.UmbracoApplication
    {
        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                var personalisationConfig = DependencyResolver.Current.GetService<PersonalisationConfig>();
                if (personalisationConfig == null) return;

                if (Session[personalisationConfig.SessionName] == null) return;

                var store = DependencyResolver.Current.GetService<IPersonalisationStore>();
                if (store == null) return;

                var context = Session[personalisationConfig.SessionName] as PersonalisationContext;

                if (context != null && (context.Tags?.Any() ?? false))
                    store.Upsert(new PersonalisationDto
                    {
                        MemberId = context.MemberId,
                        Tags = context.Tags.Select(x => new PersonalisationTag(x.Tag)
                        {
                            Score = x.Score
                        })
                    });
            }
            catch (Exception ex)
            {
                var logger = DependencyResolver.Current.GetService<ILogger>();
                if (logger != null)
                    logger.Error(typeof(Global), ex);
            }
        }
    }
}