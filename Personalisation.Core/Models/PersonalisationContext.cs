using System;
using System.Collections.Generic;

namespace Personalisation.Core.Models
{
    public class PersonalisationContext
    {
        public int MemberId { get; set; } 
        public IEnumerable<PersonalisationTag> Tags { get; set; }           
        public bool HasStoreTags { get; set; } 
    } 
}