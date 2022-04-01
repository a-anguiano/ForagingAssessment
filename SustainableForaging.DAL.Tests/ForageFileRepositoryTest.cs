using NUnit.Framework;
using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SustainableForaging.DAL.Tests
{
    public class ForageFileRepositoryTest
    {
        const string SEED_FILE_PATH = @"data\forage-seed-2020-06-26.csv";
        const string TEST_FILE_PATH = @"data\forage_data_test\2020-06-26.csv";
        const string TEST_DIR_PATH = @"data\forage_data_test";
        const int FORAGE_COUNT = 54;

        DateTime date = new DateTime(2020, 6, 26);

        ForageFileRepository repository = new ForageFileRepository(TEST_DIR_PATH);

        [SetUp]
        public void SetUp()
        {
            File.Copy(SEED_FILE_PATH, TEST_FILE_PATH, true);
        }

        [Test]
        public void ShouldFindByDate()
        {
            List<Forage> forages = repository.FindByDate(date);
            Assert.AreEqual(FORAGE_COUNT, forages.Count);
        }

        [Test]
        public void ShouldAdd()
        {
            Forage forage = new Forage();
            forage.Date = date;
            forage.Kilograms = 0.75M;

            Item item = new Item();
            item.Id = 12;
            forage.Item = item;

            Forager forager = new Forager();
            forager.Id = "AAAA-1111-2222-FFFF";
            forage.Forager = forager;

            forage = repository.Add(forage);

            Assert.AreEqual(36, forage.Id.Length);
        }
    }
}
