using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;

namespace SustainableForaging.Core.Repositories
{
    public interface IForageRepository
    {
        List<Forage> FindByDate(DateTime date);

        Forage Add(Forage forage);

        bool Update(Forage forage);
    }
}
