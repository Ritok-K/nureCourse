using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponAlmanac.Data_Model;
using WeaponAlmanac.UI;

namespace WeaponAlmanac
{
    static class Program
    {
        static internal DataModelRepository Repository { get; set; }

        static void InitRepository()
        {
            Repository = new DataModelRepository(@".\Data\OwnCollection",
                                                 @".\Data\CommonCollection");
            Repository.InitDirectories();
        }

        //static void PopulateRepository()
        //{
        //    Repository.SetWeapon(new Weapon() 
        //    {
        //        Country = "England",
        //        IsRare = true,
        //        Name = "Excalibur",
        //        Description = "Excalibur is the legendary sword of King Arthur, sometimes also attributed with magical powers or associated with the rightful sovereignty of Britain. It was associated with the Arthurian legend very early on.",
        //        Material = "Steel",
        //    });

        //    Repository.SetWeapon(new Weapon()
        //    {
        //        Country = "Greece",
        //        IsRare = true,
        //        Name = "Aegis",
        //        Description = "The aegis, as stated in the Iliad, is a device carried by Athena and Zeus, variously interpreted as an animal skin or a shield and sometimes featuring the head of a Gorgon.",
        //        Material = "Skin",
        //    });

        //    Repository.SetCollector(new Collector()
        //    {
        //        Country = "Ukraine",
        //        Name = "Kyselgov Evgen",
        //        EMail = "kyselgov@gmail.com",
        //        Phone = "",
        //    });

        //    Repository.SetCollector(new Collector()
        //    {
        //        Country = "USA",
        //        Name = "Jhon Smit",
        //        EMail = "smit@gmail.com",
        //        Phone = "+40000000",
        //    });
        //}

        [STAThread]
        static void Main()
        {
            InitRepository();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
