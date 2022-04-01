using NUnit.Framework;
using SustainableForaging.BLL.Tests.TestDoubles;
using SustainableForaging.Core.Models;

namespace SustainableForaging.BLL.Tests
{
    public class ItemServiceTest
    {
        ItemService service = new ItemService(new ItemRepositoryDouble());

        [Test]
        public void ShouldNotSaveNullName()
        {
            Item item = new Item(0, null, Category.Edible, 5.00M);
            Result<Item> result = service.Add(item);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotSaveBlankName()
        {
            Item item = new Item(0, "   \t\n", Category.Edible, 5.00M);
            Result<Item> result = service.Add(item);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotSaveNegativeDollars()
        {
            Item item = new Item(0, "Test Item", Category.Edible, -5.00M);
            Result<Item> result = service.Add(item);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotSaveTooLargeDollars()
        {
            Item item = new Item(0, "Test Item", Category.Edible, 9999.00M);
            Result<Item> result = service.Add(item);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldSave()
        {
            Item item = new Item(0, "Test Item", Category.Edible, 5.00M);

            Result<Item> result = service.Add(item);

            Assert.NotNull(result.Value);
            Assert.AreEqual(2, result.Value.Id);
        }
    }
}
