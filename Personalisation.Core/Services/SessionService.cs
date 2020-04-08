using Personalisation.Core.Config;
using Personalisation.Core.Interfaces;
using Personalisation.Core.Models;
using System;
using System.Web;
using Umbraco.Core.Logging;

namespace Personalisation.Core.Services
{
    public class SessionService : ISessionService
    {
        private readonly PersonalisationConfig _personalisationConfig;
        private readonly ILogger _logger;

        public SessionService(PersonalisationConfig personalisationConfig, ILogger logger)
        {
            _personalisationConfig = personalisationConfig ?? throw new ArgumentNullException(nameof(personalisationConfig));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void SetSessionContext(PersonalisationContext context)
        {
            try
            {
                HttpContext.Current?.Session?.Add(_personalisationConfig.SessionName, context);
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(SessionService), ex);
            }
        }

        public PersonalisationContext GetSessionContext()
        {
            try
            {
                return HttpContext.Current?.Session != null && HttpContext.Current?.Session[_personalisationConfig.SessionName] != null
                    ? (PersonalisationContext)HttpContext.Current?.Session[_personalisationConfig.SessionName] 
                    : null;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(SessionService), ex);
                return null;
            }
        }

        public void ClearSessionTags(PersonalisationContext context)
        {
            try
            {
                if (context != null)
                {
                    context.Tags = null;
                    HttpContext.Current?.Session?.Add(_personalisationConfig.SessionName, context);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(SessionService), ex);
            }
        }
    }
}
