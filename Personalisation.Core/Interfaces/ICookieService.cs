using System;

namespace Personalisation.Core.Interfaces
{
    public interface ICookieService
    {
        void SetCookieId(Guid id);
        Guid? GetCookieId( );
    }
}
