using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL
{
    public class ForagerService
    {
        private readonly IForagerRepository repository;

        public ForagerService(IForagerRepository repository)
        {
            this.repository = repository;
        }

        public List<Forager> FindByState(string stateAbbr)
        {
            return repository.FindByState(stateAbbr);
        }

        public List<Forager> FindByLastName(string prefix)
        {
            return repository.FindAll()
                    .Where(i => i.LastName.StartsWith(prefix))
                    .ToList();
        }

        public Result<Forager> Add(Forager forager)
        {
            var result = new Result<Forager>();
            if (forager == null)
            {
                result.AddMessage("Forager must not be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(forager.FirstName))
            {
                result.AddMessage("Forager first name is required.");
            }

            if (string.IsNullOrWhiteSpace(forager.LastName))
            {
                result.AddMessage("Forager last name is required.");
            }

            if (string.IsNullOrWhiteSpace(forager.State))
            {
                result.AddMessage("Forager state is required.");
            }
            else if (repository.FindAll()
                  .Any(i => i.FirstName.Equals(forager.FirstName, StringComparison.OrdinalIgnoreCase)
                  && i.LastName.Equals(forager.LastName, StringComparison.OrdinalIgnoreCase) 
                  && i.State.Equals(forager.State, StringComparison.OrdinalIgnoreCase)))
            {
                result.AddMessage($"Forager '{forager.FirstName} {forager.LastName} in {forager.State}' is a duplicate.");
            }

            if (!result.Success)
            {
                return result;
            }

            result.Value = repository.Add(forager);

            return result;
        }

    }
}
