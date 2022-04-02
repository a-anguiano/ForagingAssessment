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
        const string SEED_PATH = @"data\foragers.csv";
        const string TEST_PATH = @"data\foragers.csv";  //"../../../data/foragers.csv"
                                                        //FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)
                                                        // File.Copy(String sourceFileName, String destFileName, Boolean overwrite)

        //const int NEXT_COUNT = 1001;
        //why don't we delete?


        //ForagerFileRepository repository = new ForagerFileRepository(TEST_PATH);

        //[SetUp]
        //public void SetUp()
        //{
        //    File.Copy(SEED_PATH, TEST_PATH, true);
        //}

        //try - catch
        [Test]
        public void ShouldFindAll()
        {
                ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
                List<Forager> all = repo.FindAll(); //repository
                Assert.AreEqual(1000, all.Count);
        }

        //FindByState

        //[Test]
        //public void ShouldFindJasmin()
        //{
        //    //cda52d73-fa62-465b-ba64-189a9f356938,Jasmin,Common,CT

        //    Forager jasmin = repository.FindById("cda52d73-fa62-465b-ba64-189a9f356938");
        //    Assert.NotNull(jasmin);
        //    Assert.AreEqual("Jasmin", jasmin.FirstName);
        //    Assert.AreEqual("Common", jasmin.LastName);
        //    Assert.AreEqual("CT", jasmin.State);
        //}

        //[Test]
        //public void ShouldAdd()
        //{
        //    Forager forager = MakeForagerRick();

        //    Forager actual = repository.Add(forager);

        //    Assert.AreEqual(1001, actual.Id.Length);
        //}

        //[Test]
        //public void ShouldCreateNewFile()
        //{
        //    string path = @"data\foragers.csv";
        //    FileInfo file = new FileInfo(path);
        //    file.Delete();

        //    ForagerFileRepository repository = new ForagerFileRepository(path);
        //    Forager forager = MakeForagerRick();
        //    forager = repository.Add(forager);

        //    Assert.AreEqual(1, forager.Id);
        //    Assert.AreEqual(1, repository.FindAll().Count);
        //}

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
