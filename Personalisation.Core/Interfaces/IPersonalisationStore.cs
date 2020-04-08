using Personalisation.Core.Data.Entities;
using System;

namespace Personalisation.Core.Interfaces
{
    public interface IPersonalisationStore
    {
        PersonalisationDto GetById(int id);
        void Upsert(PersonalisationDto poco);
        void Delete(int id);
    }
}
