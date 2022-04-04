using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL.Tests.TestDoubles
{
    public class ForagerRepositoryDouble : IForagerRepository
    {
        public readonly static Forager FORAGER = MakeForager();
        public readonly static Forager FORAGER1 = MakeForager1();
        public readonly static Forager FORAGER2 = MakeForager2();
        public readonly static Forager FORAGER3 = MakeForager3();

        private readonly List<Forager> foragers = new List<Forager>();

        public ForagerRepositoryDouble()
        {
            foragers.Add(FORAGER);
        }
        public List<Forager> FindAll()
        {
            return foragers;
        }

        public Forager Add(Forager forager)
        {
            List<Forager> all = FindAll();
            forager.Id = all.Max(i => i.Id) + 1;
            all.Add(forager);
            return forager;
        }

        public Forager FindById(string id)
        {
            return foragers.FirstOrDefault(i => i.Id == id);
        }

        public List<Forager> FindByState(string stateAbbr)
        {
            return foragers
                .Where(i => i.State.Equals(stateAbbr, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private static Forager MakeForager()
        {
            Forager forager = new Forager();
            forager.Id = "0e4707f4-407e-4ec9-9665-baca0aabe88c";
            forager.FirstName = "Jilly";
            forager.LastName = "Sisse";
            forager.State = "GA";
            return forager;
        }

        private static Forager MakeForager1()
        {
            Forager forager = new Forager();
            forager.Id = "0e4707f4-407e-4ec9-9665-baca0aabe88f";
            forager.FirstName = "Lazy";
            forager.LastName = "Dog";
            forager.State = "TX";
            return forager;
        }
        private static Forager MakeForager2()
        {
            Forager forager = new Forager();
            forager.Id = "0e4707f4-407e-4ec9-9665-baca0aabe88d";
            forager.FirstName = "Jane";
            forager.LastName = "Doe";
            forager.State = "GA";
            return forager;
        }

        private static Forager MakeForager3()
        {
            Forager forager = new Forager();
            forager.Id = "0e4707f4-407e-4ec9-9665-baca0aabe88e";
            forager.FirstName = "John";
            forager.LastName = "Doe";
            forager.State = "TX";
            return forager;
        }
    }
}
