using System.Net;
using Umbraco.Core.Composing;

namespace Personalisation.Web.Composers
{
    public class RegisterServerCertificationValidationCallbackComposer : ComponentComposer<RegisterServerCertificationValidationCallbackComponent>, IUserComposer
    {
    }

    public class RegisterServerCertificationValidationCallbackComponent : IComponent
    {
        public void Initialize()
        {
#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
#endif
        }

        public void Terminate() { }
    }
}

