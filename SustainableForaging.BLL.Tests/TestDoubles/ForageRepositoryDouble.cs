using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL.Tests.TestDoubles
{
    public class ForageRepositoryDouble : IForageRepository
    {
        DateTime date = new DateTime(2020, 6, 26);

        private readonly List<Forage> forages = new List<Forage>();

        public ForageRepositoryDouble()
        {
            Forage forage = new Forage();
            forage.Id = "498604db-b6d6-4599-a503-3d8190fda823";
            forage.Date = date;
            forage.Forager = ForagerRepositoryDouble.FORAGER;
            forage.Item = ItemRepositoryDouble.ITEM;
            forage.Kilograms = 1.25M;
            forages.Add(forage);
        }

        public Forage Add(Forage forage)
        {
            forage.Id = Guid.NewGuid().ToString();
            forages.Add(forage);
            return forage;
        }

        public List<Forage> FindByDate(DateTime date)
        {
            return forages
                .Where(i => i.Date.Date == date.Date)
                .ToList();
        }

        public bool Update(Forage forage)
        {
            return false;
        }
    }
}
