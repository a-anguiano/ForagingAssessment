using SustainableForaging.Core.Exceptions;
using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SustainableForaging.DAL
{
    public class ForagerFileRepository : IForagerRepository
    {
        private readonly string filePath;

        public ForagerFileRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Forager> FindAll()
        {
            var foragers = new List<Forager>();
            if(!File.Exists(filePath))
            {
                return foragers;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch(IOException ex)
            {
                throw new RepositoryException("could not read foragers", ex);
            }

            for(int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Forager forager = Deserialize(fields);
                if(forager != null)
                {
                    foragers.Add(forager);
                }
            }
            return foragers;
        }

        public Forager FindById(string id)
        {
            return FindAll().FirstOrDefault(i => i.Id == id);
        }

        public List<Forager> FindByState(string stateAbbr)
        {
            return FindAll()
                .Where(i => i.State == stateAbbr)
                .ToList();
        }

        private Forager Deserialize(string[] fields)
        {
            if(fields.Length != 4)
            {
                return null;
            }

            Forager result = new Forager();
            result.Id = fields[0];
            result.FirstName = fields[1];
            result.LastName = fields[2];
            result.State = fields[3];
            return result;
        }
    }
}
