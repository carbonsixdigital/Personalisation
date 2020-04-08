using Personalisation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personalisation.Core.Extensions
{
    public static class PersonalisationExtensions
    {
        public static IEnumerable<PersonalisationTag> Merge(this IEnumerable<PersonalisationTag> existingTags, IEnumerable<string> newTags)
        {
            var existing = existingTags?.ToList();
            foreach (var tag in newTags)
            {
                var current = existing.Find(x => string.Equals(x.Tag, tag, StringComparison.OrdinalIgnoreCase));
                if (current == null)
                    existing.Add(new PersonalisationTag(tag));
                else
                    current.Score++;
            }
            return existing;
        }
    }
}
