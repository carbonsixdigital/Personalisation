using Personalisation.Core.Models;

namespace Personalisation.Core.Interfaces
{
    public interface ISessionService
    {
        void SetSessionContext(PersonalisationContext wrapper);
        PersonalisationContext GetSessionContext();
        void ClearSessionTags(PersonalisationContext wrapper);
    }
}
