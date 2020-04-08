using Personalisation.Core.Models;
using System;
using System.Collections.Generic;

namespace Personalisation.Core.Data.Entities
{  
    public class PersonalisationDto
    {
        public int MemberId { get; set; }
        public IEnumerable<PersonalisationTag> Tags { get; set; }
    }
}