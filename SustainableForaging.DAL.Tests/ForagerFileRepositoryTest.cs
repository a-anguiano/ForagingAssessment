using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.Collections.Generic;
using System.IO;
using System;

namespace SustainableForaging.DAL.Tests
{
    public class ForagerFileRepositoryTest
    {
        [Test]
        public void ShouldFindAll()
        {
                ForagerFileRepository repo = new ForagerFileRepository("../../../data/foragers.csv");
                List<Forager> all = repo.FindAll(); 
                Assert.AreEqual(1000, all.Count);
        }

        [Test]
        public void ShouldFindJasmin()
        {
            //cda52d73-fa62-465b-ba64-189a9f356938,Jasmin,Common,CT

            ForagerFileRepository repo = new ForagerFileRepository("../../../data/foragers.csv");
            Forager jasmin = repo.FindById("cda52d73-fa62-465b-ba64-189a9f356938");
            Assert.NotNull(jasmin);
            Assert.AreEqual("Jasmin", jasmin.FirstName);
            Assert.AreEqual("Common", jasmin.LastName);
            Assert.AreEqual("CT", jasmin.State);
        }

        [Test]
        public void ShouldAdd()
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
            Forager forager = MakeForagerRick();

            Forager actual = repo.Add(forager);
            Assert.AreEqual(36, actual.Id.Length); 
            
        }

        [Test]
        public void ShouldCreateNewFile()
        {
            string path = @"data\foragers.csv";
            FileInfo file = new FileInfo(path);
            file.Delete();

            ForagerFileRepository repository = new ForagerFileRepository(path);
            Forager forager = MakeForagerRick();
            repository.Add(forager);  

            Assert.AreEqual(1, repository.FindAll().Count); 
        }

        private Forager MakeForagerRick()
        {
            Forager forager = new Forager();
            forager.Id = "AAAA-1111-2222-FFFF";
            forager.FirstName = "Rick";
            forager.LastName = "Grimes";
            forager.State = "KY";
            return forager;
        }
    }
}
