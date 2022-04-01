using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.IO;

namespace SustainableForaging.DAL.Tests
{
    public class ItemFileRepositoryTest
    {
        const string SEED_PATH = @"data\items-seed.txt";
        const string TEST_PATH = @"data\items-test.txt";
        const int NEXT_ID = 27;

        ItemFileRepository repository = new ItemFileRepository(TEST_PATH);

        [SetUp]
        public void SetUp()
        {
            File.Copy(SEED_PATH, TEST_PATH, true);
        }

        [Test]
        public void ShouldFindAll()
        {
            Assert.AreEqual(NEXT_ID - 1, repository.FindAll().Count);
        }

        [Test]
        public void ShouldFindPapaw()
        {
            Item papaw = repository.FindById(6);
            Assert.NotNull(papaw);
            Assert.AreEqual("Papaw", papaw.Name);
            Assert.AreEqual(Category.Edible, papaw.Category);
            Assert.AreEqual(9.99M, papaw.DollarsPerKilogram);
        }

        [Test]
        public void ShouldAdd()
        {
            Item expected = MakeCatalpa();
            expected.Id = NEXT_ID;

            Item item = MakeCatalpa();

            Item actual = repository.Add(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCreateNewFile()
        {
            string path = @"data\items-new.txt";
            FileInfo file = new FileInfo(path);
            file.Delete();

            ItemFileRepository repository = new ItemFileRepository(path);
            Item item = MakeCatalpa();
            item = repository.Add(item);

            Assert.AreEqual(1, item.Id);
            Assert.AreEqual(1, repository.FindAll().Count);
        }

        private Item MakeCatalpa()
        {
            Item item = new Item();
            item.Name = "Catalpa";
            item.Category = Category.Inedible;
            item.DollarsPerKilogram = decimal.Zero;
            return item;
        }
    }
}
