using SustainableForaging.Core.Models;
using System.Collections.Generic;

namespace SustainableForaging.Core.Repositories
{
    public interface IForagerRepository
    {
        Forager FindById(string id);

        List<Forager> FindAll();

        List<Forager> FindByState(string stateAbbr);

        Forager Add(Forager forager);
    }
}
