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
        //ack, items and kg

        public ForageRepositoryDouble()
        {
            Forage forage = new Forage();
            forage.Id = "498604db-b6d6-4599-a503-3d8190fda823";
            forage.Date = date;
            forage.Forager = ForagerRepositoryDouble.FORAGER;
            forage.Item = ItemRepositoryDouble.ITEM;
            forage.Kilograms = 1.25M;
            forages.Add(forage);

            Forage forage1 = new Forage();
            forage1.Date = DateTime.Today;
            forage1.Forager = ForagerRepositoryDouble.FORAGER1;
            forage1.Item = ItemRepositoryDouble.ITEM;
            forage1.Kilograms = 0.5M;

            Forage forage2 = new Forage();
            forage2.Id = "498604db-b6d6-4599-a503-3d8190fda824";
            forage2.Date = date;
            forage2.Forager = ForagerRepositoryDouble.FORAGER2;
            forage2.Item = ItemRepositoryDouble.ITEM;
            forage2.Kilograms = 0.2M;
            forages.Add(forage2);

            Forage forage3 = new Forage();
            forage3.Id = "498604db-b6d6-4599-a503-3d8190fda825";
            forage3.Date = date;
            forage3.Forager = ForagerRepositoryDouble.FORAGER3;
            forage3.Item = ItemRepositoryDouble.ITEM;
            forage3.Kilograms = 0.5M;
            forages.Add(forage3);
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
