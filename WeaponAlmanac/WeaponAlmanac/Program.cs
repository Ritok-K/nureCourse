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
            Debug.Assert(weaponList.Count == weaponCount + 1);
            Debug.Assert(weaponList[0].Country == weapon.Country);
            Debug.Assert(weaponList[0].Name == weapon.Name);
            Debug.Assert(weaponList[0].Description == weapon.Description);
            Debug.Assert(weaponList[0].Material == weapon.Material);
            Debug.Assert(weaponList[0].IsRare == weapon.IsRare);
            Debug.Assert(weaponList[0].ManufactureDate.Year == weapon.ManufactureDate.Year);
            Debug.Assert(weaponList[0].ManufactureDate.Month == weapon.ManufactureDate.Month);
            Debug.Assert(weaponList[0].ManufactureDate.Day == weapon.ManufactureDate.Day);

            Repository.RemoveWeapon(weapon.Id);
            weaponList = Repository.GetWeapon();
            Debug.Assert(weaponList.Count == weaponCount);
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
