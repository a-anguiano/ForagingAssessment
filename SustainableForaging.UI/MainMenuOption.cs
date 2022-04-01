using System;

namespace SustainableForaging.UI
{
    public enum MainMenuOption
    {
        Exit,
        ViewForagesByDate,
        ViewItems,
        AddForage,
        AddForager,
        AddItem,
        ReportKgPerItem,
        ReportCategoryValue,
        Generate
    }

    public static class MainMenuOptionExtensions
    {
        public static string ToLabel(this MainMenuOption option) => option switch
        {
            MainMenuOption.Exit => "Exit",
            MainMenuOption.ViewForagesByDate => "View Forages By Date",
            MainMenuOption.ViewItems => "View Items",
            MainMenuOption.AddForage => "Add a Forage",
            MainMenuOption.AddForager => "Add a Forager",
            MainMenuOption.AddItem => "Add an Item",
            MainMenuOption.ReportKgPerItem => "Report: Kilograms of Item",
            MainMenuOption.ReportCategoryValue => "Report: Item Category Value",
            MainMenuOption.Generate => "Generate Random Forages",
            _ => throw new NotImplementedException()
        };

        public static bool IsHidden(this MainMenuOption option) => option switch
        {
            MainMenuOption.Generate => true,
            _ => false
        };
    }


}
