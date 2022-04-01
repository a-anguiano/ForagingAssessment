using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.UI
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for(int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                if(!option.IsHidden())
                {
                    io.PrintLine($"{i}. {option.ToLabel()}");
                }
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max - 1}]: ";
            return options[io.ReadInt(message, min, max)];
        }

        public DateTime GetForageDate()
        {
            DisplayHeader(MainMenuOption.ViewForagesByDate.ToLabel());
            return io.ReadDate("Select a date [MM/dd/yyyy]: ");
        }

        public string GetForagerNamePrefix()
        {
            return io.ReadRequiredString("Forager last name starts with: ");
        }

        public Forager ChooseForager(List<Forager> foragers)
        {
            if(foragers == null || foragers.Count == 0)
            {
                io.PrintLine("No foragers found");
                return null;
            }

            int index = 1;
            foreach(Forager forager in foragers.Take(25))
            {
                io.PrintLine($"{index++}: {forager.FirstName} {forager.LastName}");
            }
            index--;

            if(foragers.Count > 25)
            {
                io.PrintLine("More than 25 foragers found. Showing first 25. Please refine your search.");
            }
            io.PrintLine("0: Exit");
            string message = $"Select a forager by their index [0-{index}]: ";

            index = io.ReadInt(message, 0, index);
            if(index <= 0)
            {
                return null;
            }
            return foragers[index - 1];
        }

        public Category GetItemCategory()
        {
            DisplayHeader("Item Categories");
            int index = 1;
            Category[] categories = Enum.GetValues<Category>();
            for(; index <= categories.Length; index++)
            {
                io.PrintLine($"{index}: {categories[index - 1]}");
            }
            index--;
            string message = $"Select a Category [1-{index}]: ";
            return categories[io.ReadInt(message, 1, index) - 1];
        }

        public Item ChooseItem(List<Item> items)
        {
            DisplayItems(items);

            if(items == null || items.Count == 0)
            {
                return null;
            }

            int itemId = io.ReadInt("Select an item id: ");
            Item item = items.FirstOrDefault(i => i.Id == itemId);

            if(item == null)
            {
                DisplayStatus(false, $"No item with id {itemId} found.");
            }

            return item;
        }

        public Forage MakeForage(Forager forager, Item item)
        {
            Forage forage = new Forage();
            forage.Forager = forager;
            forage.Item = item;
            forage.Date = io.ReadDate("Forage date [MM/dd/yyyy]: ");
            string message = $"Kilograms of {item.Name}: ";
            forage.Kilograms = io.ReadDecimal(message, 0.001M, 250.0M);
            return forage;
        }

        public Item MakeItem()
        {
            DisplayHeader(MainMenuOption.AddItem.ToLabel());
            Item item = new Item();
            item.Category = GetItemCategory();
            item.Name = io.ReadRequiredString("Item Name: ");
            item.DollarsPerKilogram = io.ReadDecimal("$/Kg: ", 0M, 7500.00M);
            return item;
        }

        public ForageGenerationRequest GetForageGenerationRequest()
        {
            DisplayHeader(MainMenuOption.Generate.ToLabel());
            DateTime start = io.ReadDate("Select a start date [MM/dd/yyyy]: ");
            if(start > DateTime.Now)
            {
                DisplayStatus(false, "Start date must be in the past.");
                return null;
            }

            DateTime end = io.ReadDate("Select an end date [MM/dd/yyyy]: ");
            if(end > DateTime.Now || end < start)
            {
                DisplayStatus(false, "End date must be in the past and after the start date.");
                return null;
            }

            ForageGenerationRequest request = new ForageGenerationRequest();
            request.Start = start;
            request.End = end;
            request.Count = io.ReadInt("Generate how many random forages [1-500]?: ", 1, 500);
            return request;
        }

        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }

        // display only
        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }

        public void DisplayStatus(bool success, string message)
        {
            DisplayStatus(success, new List<string>() { message });
        }

        public void DisplayStatus(bool success, List<string> messages)
        {
            DisplayHeader(success ? "Success" : "Error");
            foreach(string message in messages)
            {
                io.PrintLine(message);
            }
        }

        public void DisplayForages(List<Forage> forages)
        {
            if(forages == null || forages.Count == 0)
            {
                io.PrintLine("No forages found.");
                return;
            }

            foreach(Forage forage in forages)
            {
                io.PrintLine(
                    string.Format("{0} {1} - {2}:{3} - Value: ${4:0.00}",
                        forage.Forager.FirstName,
                        forage.Forager.LastName,
                        forage.Item.Name,
                        forage.Item.Category,
                        forage.Value)
                );
            }
        }

        public void DisplayItems(List<Item> items)
        {
            if(items == null || items.Count == 0)
            {
                io.PrintLine("No items found");
            }

            foreach(Item item in items)
            {
                io.PrintLine($"{item.Id}: {item.Name}, {item.Category}, {item.DollarsPerKilogram:0.00} $/kg");
            }
        }
    }
}
