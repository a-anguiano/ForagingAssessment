using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace SustainableForaging.DAL.Tests
{
    public class ForagerFileRepositoryTest
    {
        const string SEED_PATH = @"data\foragers.csv";
        const string TEST_PATH = @"data\foragers.csv";
        const int NEXT_ID = 1001;
        //1001?

        ForagerFileRepository repository = new ForagerFileRepository(TEST_PATH);

        [SetUp]
        public void SetUp()
        {
            File.Copy(SEED_PATH, TEST_PATH, true);
        }

        [Test]
        public void ShouldFindAll()
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
            List<Forager> all = repo.FindAll();
            Assert.AreEqual(1000, all.Count);
        }

        [Test]
        public void ShouldFindJasmin()
        {
            //cda52d73-fa62-465b-ba64-189a9f356938,Jasmin,Common,CT

            Forager jasmin = repository.FindById("cda52d73-fa62-465b-ba64-189a9f356938");
            Assert.NotNull(jasmin);
            Assert.AreEqual("Jasmin", jasmin.FirstName);
            Assert.AreEqual("Common", jasmin.LastName);
            Assert.AreEqual("CT", jasmin.State);
        }
    }
}
