using SustainableForaging.BLL;
using SustainableForaging.Core.Exceptions;
using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;

namespace SustainableForaging.UI
{
    public class Controller
    {
        private readonly ForagerService foragerService;
        private readonly ForageService forageService;
        private readonly ItemService itemService;
        private readonly View view;

        public Controller(ForagerService foragerService, ForageService forageService, ItemService itemService, View view)
        {
            this.foragerService = foragerService;
            this.forageService = forageService;
            this.itemService = itemService;
            this.view = view;
        }

        public void Run()
        {
            view.DisplayHeader("Welcome to Sustainable Foraging");
            try
            {
                RunAppLoop();
            }
            catch(RepositoryException ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch(option)
                {
                    case MainMenuOption.ViewForagesByDate:
                        ViewByDate();
                        break;
                    case MainMenuOption.ViewItems:
                        ViewItems();
                        break;
                    case MainMenuOption.AddForage:
                        AddForage();
                        break;
                    case MainMenuOption.AddForager:
                        AddForager();
                        break;
                    case MainMenuOption.AddItem:
                        AddItem();
                        break;
                    case MainMenuOption.ReportKgPerItem:
                        view.DisplayStatus(false, "NOT IMPLEMENTED");
                        view.EnterToContinue();
                        break;
                    case MainMenuOption.ReportCategoryValue:
                        view.DisplayStatus(false, "NOT IMPLEMENTED");
                        view.EnterToContinue();
                        break;
                    case MainMenuOption.Generate:
                        Generate();
                        break;
                }
            } while(option != MainMenuOption.Exit);
        }

        // top level menu
        private void ViewByDate()
        {
            DateTime date = view.GetForageDate();
            List<Forage> forages = forageService.FindByDate(date);
            view.DisplayForages(forages);
            view.EnterToContinue();
        }

        private void ViewItems()
        {
            view.DisplayHeader(MainMenuOption.ViewItems.ToLabel());
            Category category = view.GetItemCategory();
            List<Item> items = itemService.FindByCategory(category);
            view.DisplayHeader("Items");
            view.DisplayItems(items);
            view.EnterToContinue();
        }

        private void AddForage()
        {
            view.DisplayHeader(MainMenuOption.AddForage.ToLabel());
            Forager forager = GetForager();
            if(forager == null)
            {
                return;
            }
            Item item = GetItem();
            if(item == null)
            {
                return;
            }
            Forage forage = view.MakeForage(forager, item);
            Result<Forage> result = forageService.Add(forage);
            if(!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Forage {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }

        //ADD A FORAGER
        private void AddForager()
        {
            Forager forager = view.MakeForager();
            Result<Forager> result = foragerService.Add(forager);
            if(!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Item {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }

        private void AddItem()
        {
            Item item = view.MakeItem();
            Result<Item> result = itemService.Add(item);
            if(!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Item {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }

        //REPORT1


        //REPORT2

        private void Generate()
        {
            ForageGenerationRequest request = view.GetForageGenerationRequest();
            if(request != null)
            {
                int count = forageService.Generate(request.Start, request.End, request.Count);
                view.DisplayStatus(true, $"{count} forages generated.");
            }
        }

        // support methods
        private Forager GetForager()
        {
            string lastNamePrefix = view.GetForagerNamePrefix();
            List<Forager> foragers = foragerService.FindByLastName(lastNamePrefix);
            return view.ChooseForager(foragers);
        }

        private Item GetItem()
        {
            Category category = view.GetItemCategory();
            List<Item> items = itemService.FindByCategory(category);
            return view.ChooseItem(items);
        }
    }
}
