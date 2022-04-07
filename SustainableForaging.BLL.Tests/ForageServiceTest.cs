using NUnit.Framework;
using SustainableForaging.BLL.Tests.TestDoubles;
using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;

namespace SustainableForaging.BLL.Tests
{
    public class ForageServiceTest
    {
        ForageService service = new ForageService(
           new ForageRepositoryDouble(),
           new ForagerRepositoryDouble(),
           new ItemRepositoryDouble());

        //total kg = 1.2M

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

        [Test]
        public void ShouldNotAddWhenComboIsDuplicate()
        {
            Forage forage = new Forage();
            forage.Date = DateTime.Today;
            forage.Forager = ForagerRepositoryDouble.FORAGER;
            forage.Item = ItemRepositoryDouble.ITEM;
            forage.Kilograms = 0.5M;
            service.Add(forage);

            Forage forageDuplicate = new Forage();
            forageDuplicate.Date = DateTime.Today;
            forageDuplicate.Forager = ForagerRepositoryDouble.FORAGER;
            forageDuplicate.Item = ItemRepositoryDouble.ITEM;
            forageDuplicate.Kilograms = 0.5M;
            Result<Forage> result = service.Add(forageDuplicate);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void GetTotalValueOfEachCategoryInOneDay_ReturnsToalValueByCategory()
        {
            //9.99M = dollars per killogram
            //1.2kg of item

            DateTime date = DateTime.Today;
            var expected = new Dictionary<Category, decimal> { { Category.Edible, 11.988M} };

            Dictionary<Category, decimal> actual = service.GetTotalValueOfEachCategoryInOneDay(date);

            Assert.AreEqual(expected, actual);
        }

        //1.2M
        //[Test]
        //public void GetKgOfEachItemInADay()
        //{
        //    DateTime date = DateTime.Today;

        //    Category[] expectedItemCategory = new Category[3] {Category.Edible, Category.Edible, Category.Edible};
        //    string[] expectedItemName = new string[3] { "Chanterelle", "Chanterelle", "Chanterelle" };
        //    decimal[] expectedKg = new decimal[3] {0.5M, 0.2M, 0.5M};

        //    Category[] actualCategory = new Category[3];
        //    string[] actualItemName = new string[3];
        //    decimal[] actualKg = new decimal[3];

        //    var byCategory = service.GetItemKgStatReport(date);


        //    for (int i = 0; i < 3; i++)
        //    {
        //        actualCategory = byCategory;

        //        foreach (var item in itemGroup)
        //        {
        //            actualItemName = item.Item.Name;
        //            actualKg = item.Kilograms;
        //        }
        //    }

        //    Assert.AreEqual(expectedItemCategory, itemCategory);
        //    Assert.AreEqual(expectedItemName, itemName);

        //}
    }
}
