using SustainableForaging.BLL;
using SustainableForaging.DAL;
using System;
using System.IO;
using Ninject;
using Ninject.Modules;

namespace SustainableForaging.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.Kernel.Get<Controller>();
            controller.Run();
        }

    }
}
