using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.Collections.Generic;
using System.IO;
using System;

namespace SustainableForaging.DAL.Tests
{
    public class ForagerFileRepositoryTest
    {
        //string dir = Directory.GetCurrentDirectory();
        //const string SEED_PATH = @"data\foragers.csv";
        //const string TEST_PATH = @"data\foragers.csv";  //"../../../data/foragers.csv"
        //                                                //FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)
        //                                                // File.Copy(String sourceFileName, String destFileName, Boolean overwrite)
        //                                                //why don't we delete?


        //ForagerFileRepository repository = new ForagerFileRepository(TEST_PATH);

        //[SetUp]
        //public void SetUp()
        //{
        //    File.Copy(SEED_PATH, TEST_PATH, true);
        //}

        [Test]
        public void ShouldFindAll()
        {
                ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
                List<Forager> all = repo.FindAll(); //repository
                Assert.AreEqual(1000, all.Count);
        }

        //FindByState

        [Test]
        public void ShouldFindJasmin()
        {
            //cda52d73-fa62-465b-ba64-189a9f356938,Jasmin,Common,CT

            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
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

            //36 is length of GUID generated id
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
            forager = repository.Add(forager);  //may not need to assign variable

            //Assert.AreEqual("AAAA-1111-2222-FFFF", forager.Id);
            Assert.AreEqual(1, repository.FindAll().Count); //
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
