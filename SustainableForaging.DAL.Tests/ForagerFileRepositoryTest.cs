using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.Collections.Generic;

namespace SustainableForaging.DAL.Tests
{
    public class ForagerFileRepositoryTest
    {
        [Test]
        public void ShouldFindAll()
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
            List<Forager> all = repo.FindAll();
            Assert.AreEqual(1000, all.Count);
        }
    }
}
