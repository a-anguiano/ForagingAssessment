using SustainableForaging.Core.Exceptions;
using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace SustainableForaging.DAL
{
    public class ForageFileRepository : IForageRepository
    {
        private const string HEADER = "id,forager_id,item_id,kg";
        private readonly string directory;

        public ForageFileRepository(string directory)
        {
            this.directory = directory;
        }

        public Forage Add(Forage forage)
        {
            List<Forage> all = FindByDate(forage.Date);
            forage.Id = Guid.NewGuid().ToString();
            all.Add(forage);
            Write(all, forage.Date);
            return forage;
        }

        public List<Forage> FindByDate(DateTime date)
        {
            var forages = new List<Forage>();
            var path = GetFilePath(date);
            if(!File.Exists(path))
            {
                return forages;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch(IOException ex)
            {
                throw new RepositoryException("could not read forages", ex);
            }


            for(int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Forage forage = Deserialize(fields, date);
                if(forage != null)
                {
                    forages.Add(forage);
                }
            }
            return forages;
        }

        public bool Update(Forage forage)
        {
            List<Forage> all = FindByDate(forage.Date);
            for(int i = 0; i < all.Count; i++)
            {
                if(all[i].Id == forage.Id)
                {
                    all[i] = forage;
                    Write(all, forage.Date);
                    return true;
                }
            }
            return false;
        }

        private string GetFilePath(DateTime date)
        {
            return Path.Combine(directory, $"{date:yyyy-MM-dd}.csv");
        }

        private string Serialize(Forage item)
        {
            return string.Format("{0},{1},{2},{3}",
                    item.Id,
                    item.Forager.Id,
                    item.Item.Id,
                    item.Kilograms);
        }

        private Forage Deserialize(string[] fields, DateTime date)
        {
            if(fields.Length != 4)
            {
                return null;
            }

            Forage result = new Forage();
            result.Id = fields[0];
            result.Date = date;
            result.Kilograms = decimal.Parse(fields[3]);

            Forager forager = new Forager();
            forager.Id = fields[1];
            result.Forager = forager;

            Item item = new Item();
            item.Id = int.Parse(fields[2]);
            result.Item = item;
            return result;
        }

        private void Write(List<Forage> forages, DateTime date)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(date));
                writer.WriteLine(HEADER);

                if(forages == null)
                {
                    return;
                }

                foreach(var forage in forages)
                {
                    writer.WriteLine(Serialize(forage));
                }
            }
            catch(IOException ex)
            {
                throw new RepositoryException("could not write forages", ex);
            }
        }
    }
}
