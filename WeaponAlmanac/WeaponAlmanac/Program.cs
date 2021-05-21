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

            //PopulateRepository();
            //TestRepository();
        }

        static void PopulateRepository()
        {
            Repository.SetWeapon(new Weapon() 
            {
                Country = "England",
                IsRare = true,
                Name = "Excalibur",
                Description = "Excalibur is the legendary sword of King Arthur, sometimes also attributed with magical powers or associated with the rightful sovereignty of Britain. It was associated with the Arthurian legend very early on.",
                Material = "Steel",
            });

            Repository.SetWeapon(new Weapon()
            {
                Country = "Greece",
                IsRare = true,
                Name = "Aegis",
                Description = "The aegis, as stated in the Iliad, is a device carried by Athena and Zeus, variously interpreted as an animal skin or a shield and sometimes featuring the head of a Gorgon.",
                Material = "Skin",
            });

            Repository.SetCollector(new Collector()
            {
                Country = "Ukraine",
                Name = "Kyselgov Evgen",
                EMail = "kyselgov@gmail.com",
                Phone = "",
            });

            Repository.SetCollector(new Collector()
            {
                Country = "USA",
                Name = "Jhon Smit",
                EMail = "smit@gmail.com",
                Phone = "+40000000",
            });
        }

        static void TestRepository()
        {
#if DEBUG
            var weaponCount = Repository.GetWeapon().Count;

            var weapon = new Weapon() 
            {
                Country = "UA",
                IsRare = true,
                Name = "Sword",
                Description = "Supper",
                Material = "Steel",
            };

            Repository.SetWeapon(weapon);

            var weaponList = Repository.GetWeapon();
            var index = weaponList.FindIndex(w => w.Id == weapon.Id);

            Debug.Assert(weaponList.Count == weaponCount + 1);
            Debug.Assert(index >= 0);
            Debug.Assert(weaponList[index].Country == weapon.Country);
            Debug.Assert(weaponList[index].Name == weapon.Name);
            Debug.Assert(weaponList[index].Description == weapon.Description);
            Debug.Assert(weaponList[index].Material == weapon.Material);
            Debug.Assert(weaponList[index].IsRare == weapon.IsRare);
            Debug.Assert(weaponList[index].ManufactureDate == weapon.ManufactureDate);

            Repository.RemoveWeapon(weapon.Id);
            weaponList = Repository.GetWeapon();
            Debug.Assert(weaponList.Count == weaponCount);
            Debug.Assert(weaponList.FindIndex(w => w.Id == weapon.Id) < 0);
#endif
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
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
