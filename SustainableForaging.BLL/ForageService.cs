using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL
{
    public class ForageService
    {
        private readonly IForageRepository forageRepository;
        private readonly IForagerRepository foragerRepository;
        private readonly IItemRepository itemRepository;

        public ForageService(IForageRepository forageRepository, IForagerRepository foragerRepository, IItemRepository itemRepository)
        {
            this.forageRepository = forageRepository;
            this.foragerRepository = foragerRepository;
            this.itemRepository = itemRepository;
        }

        public List<Forage> FindByDate(DateTime date)
        {
            Dictionary<string, Forager> foragerMap = foragerRepository.FindAll()
                    .ToDictionary(i => i.Id);
            Dictionary<int, Item> itemMap = itemRepository.FindAll()
                    .ToDictionary(i => i.Id);

            List<Forage> result = forageRepository.FindByDate(date);
            foreach(var forage in result)
            {
                forage.Forager = foragerMap[forage.Forager.Id];
                forage.Item = itemMap[forage.Item.Id];
            }

            return result;
        }

        public Result<Forage> Add(Forage forage)
        {
            Result<Forage> result = Validate(forage);
            if(!result.Success)
            {
                return result;
            }

            result.Value = forageRepository.Add(forage);

            return result;
        }

        public int Generate(DateTime start, DateTime end, int count)
        {
            if(start > end || count <= 0)
            {
                return 0;
            }

            count = Math.Min(count, 500);

            var dates = new List<DateTime>();
            while(start <= end)
            {
                dates.Add(start);
                start = start.AddDays(1);
            }

            List<Item> items = itemRepository.FindAll();
            List<Forager> foragers = foragerRepository.FindAll();
            Random random = new Random();

            for(int i = 0; i < count; i++)
            {
                Forage forage = new Forage();
                forage.Date = dates[random.Next(dates.Count)];
                forage.Forager = foragers[random.Next(foragers.Count)];
                forage.Item = items[random.Next(items.Count)];
                forage.Kilograms = (decimal)(random.NextDouble() * 5.0 + 0.1);
                forageRepository.Add(forage);
            }

            return count;
        }

        private Result<Forage> Validate(Forage forage)
        {
            Result<Forage> result = ValidateNulls(forage);
            if(!result.Success)
            {
                return result;
            }

            ValidateFields(forage, result);
            if(!result.Success)
            {
                return result;
            }

            ValidateChildrenExist(forage, result);

            return result;
        }

        private Result<Forage> ValidateNulls(Forage forage)
        {
            var result = new Result<Forage>();

            if(forage == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if(forage.Forager == null)
            {
                result.AddMessage("Forager is required.");
            }

            if(forage.Item == null)
            {
                result.AddMessage("Item is required.");
            }

            return result;
        }

        private void ValidateFields(Forage forage, Result<Forage> result)
        {
            // No future dates.
            if(forage.Date > DateTime.Now)
            {
                result.AddMessage("Forage date cannot be in the future.");
            }

            if(forage.Kilograms <= 0M || forage.Kilograms > 250.0M)
            {
                result.AddMessage("Kilograms must be a positive number less than 250.0");
            }
        }

        private void ValidateChildrenExist(Forage forage, Result<Forage> result)
        {
            if(forage.Forager.Id == null
                    || foragerRepository.FindById(forage.Forager.Id) == null)
            {
                result.AddMessage("Forager does not exist.");
            }

            if(itemRepository.FindById(forage.Item.Id) == null)
            {
                result.AddMessage("Item does not exist.");
            }
        }
    }
}
