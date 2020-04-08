using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personalisation.Core.Constants
{
    public static class AppSettings
    {
        public static class Personalisation
        {
            public static string PropertyAlias = "personalisation:PropertyAlias";     
            public static string SessionPersonalisationId = "personalisation:Session:PersonalisationId";

            public static class Defaults
            {
                public static string PropertyValueDefault = "tags";
                public static string SessionPersonalisationId = "personalisationId";
            }
        }
    }
}
