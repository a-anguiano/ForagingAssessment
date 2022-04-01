using NUnit.Framework;
using SustainableForaging.BLL.Tests.TestDoubles;
using SustainableForaging.Core.Models;
using System;

namespace SustainableForaging.BLL.Tests
{
    public class ForageServiceTest
    {
        ForageService service = new ForageService(
           new ForageRepositoryDouble(),
           new ForagerRepositoryDouble(),
           new ItemRepositoryDouble());

        [Test]
        public void ShouldAdd()
        {
            Forage forage = new Forage();
            forage.Date = DateTime.Today;
            forage.Forager = ForagerRepositoryDouble.FORAGER;
            forage.Item = ItemRepositoryDouble.ITEM;
            forage.Kilograms = 0.5M;

            Result<Forage> result = service.Add(forage);
            Assert.IsTrue(result.Success);
            Assert.NotNull(result.Value);
            Assert.AreEqual(36, result.Value.Id.Length);
        }

        [Test]
        public void ShouldNotAddWhenForagerNotFound()
        {
            Forager forager = new Forager();
            forager.Id = "30816379-188d-4552-913f-9a48405e8c08";
            forager.FirstName = "Ermengarde";
            forager.LastName ="Sansom";
            forager.State ="NM";

            Forage forage = new Forage();
            forage.Date = DateTime.Today;
            forage.Forager = forager;
            forage.Item = ItemRepositoryDouble.ITEM;
            forage.Kilograms = 0.5M;

            Result<Forage> result = service.Add(forage);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotAddWhenItemNotFound()
        {
            Item item = new Item(11, "Dandelion", Category.Edible, 0.05M);

            Forage forage = new Forage();
            forage.Date = DateTime.Today;
            forage.Forager = ForagerRepositoryDouble.FORAGER;
            forage.Item = item;
            forage.Kilograms =0.5M;

            Result<Forage> result = service.Add(forage);
            Assert.IsFalse(result.Success);
        }
    }
}
