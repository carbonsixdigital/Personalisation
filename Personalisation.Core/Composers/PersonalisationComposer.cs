using Personalisation.Core.Config;
using Personalisation.Core.Constants;
using Personalisation.Core.Data.Stores;
using Personalisation.Core.Interfaces;
using Personalisation.Core.Services;
using System.Configuration;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Personalisation.Core.Composers
{
    public class PersonalisationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register((factory) => new PersonalisationConfig
            {
                PropertyAlias = ConfigurationManager.AppSettings[AppSettings.Personalisation.PropertyAlias] ??
                    AppSettings.Personalisation.Defaults.PropertyValueDefault,

                SessionName = ConfigurationManager.AppSettings[AppSettings.Personalisation.SessionPersonalisationId] ?? 
                    AppSettings.Personalisation.Defaults.SessionPersonalisationId    
            });

            composition.Register<IPersonalisationStore, PersonalisationStore>();            
            composition.Register<ISessionService, SessionService>();
            composition.Register<IPersonalisationService, PersonalisationService>();
        }
    }
}