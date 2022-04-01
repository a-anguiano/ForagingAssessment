using SustainableForaging.BLL.Tests.TestDoubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SustainableForaging.Core.Models;

namespace SustainableForaging.BLL.Tests
{
    public class ForagerServiceTest
    {
        ForagerService service = new ForagerService(new ForagerRepositoryDouble());
        //Only repoDouble needed?

        //First name is required.
        [Test]
        public void ShouldNotSaveNullFirstName()
        {
            Forager forager = new Forager("0", null, "Anguiano", "Texas");   //change to GUID?
            Result<Forager> result = service.Add(forager);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotSaveBlankFirstName()
        {
            Forager forager = new Forager("0", "   \t\n", "Anguiano", "Texas");
            Result<Forager> result = service.Add(forager);
            Assert.IsFalse(result.Success);
        }

        //Last name is required.
        [Test]
        public void ShouldNotSaveNullLastName()
        {
            Forager forager = new Forager("0", "Allison", null, "Texas");   //change to GUID?
            Result<Forager> result = service.Add(forager);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotSaveBlankLastName()
        {
            Forager forager = new Forager("0", "Allison", "   \t\n", "Texas");
            Result<Forager> result = service.Add(forager);
            Assert.IsFalse(result.Success);
        }

        //State is required
        [Test]
        public void ShouldNotSaveNullState()
        {
            Forager forager = new Forager("0", "Allison", "Anguiano", null);   //change to GUID?
            Result<Forager> result = service.Add(forager);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotSaveBlankState()
        {
            Forager forager = new Forager("0", "Allison", "Anguiano", "   \t\n");
            Result<Forager> result = service.Add(forager);
            Assert.IsFalse(result.Success);
        }

        //The combination of first name, last name, and state cannot be duplicated.

        //Forager ID is a GUID
    }
}
