using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponAlmanac.Data_Model;

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

            TestRepository();
        }

        static void TestRepository()
        {
#if DEBUG
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
            Debug.Assert(weaponList.Count == 1);
            Debug.Assert(weaponList[0].Country == "UA");
            Debug.Assert(weaponList[0].Name == "Sword");
            Debug.Assert(weaponList[0].Description == "Supper");
            Debug.Assert(weaponList[0].Material == "Steel");
            Debug.Assert(weaponList[0].IsRare);
            Debug.Assert(weaponList[0].ManufactureDate.Year == DateTime.Now.Year);
            Debug.Assert(weaponList[0].ManufactureDate.Month == DateTime.Now.Month);
            Debug.Assert(weaponList[0].ManufactureDate.Day == DateTime.Now.Day);

            Repository.RemoveWeapon(weapon.Id);
            weaponList = Repository.GetWeapon();
            Debug.Assert(weaponList.Count == 0);
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
