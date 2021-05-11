using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WeaponAlmanac.Data_Model
{
    class DataModelRepository
    {
        internal DataModelRepository(string ownCollectionPath, string commonCollectionPath)
        {
            m_ownCollectionPath = ownCollectionPath;
            m_commonCollectionPath = commonCollectionPath;
        }

        internal string CommonCollectionPath => Path.GetFullPath(m_commonCollectionPath);
        internal string OwnCollectionPath => Path.GetFullPath(m_ownCollectionPath);

        internal string OwnWeaponCollectionPath => Path.Combine(OwnCollectionPath, c_weaponDirectory);
        internal string WeaponCommonCollectionPath => Path.Combine(CommonCollectionPath, c_weaponDirectory);
        internal string CollectorsCommonCollectionPath => Path.Combine(CommonCollectionPath, c_collectorsDirectory);


        internal void SetWeapon(Weapon weapon)
        {
            SaveWeapon(WeaponCommonCollectionPath, weapon);
        }

        internal void SetOwnWeapon(Weapon weapon)
        {
            SaveWeapon(OwnWeaponCollectionPath, weapon);
        }

        internal void SetCollector(Collector collector)
        {
            SaveCollector(CollectorsCommonCollectionPath, collector);
        }

        internal void RemoveWeapon(string weaponId)
        {
            RemoveDataModelObject(WeaponCommonCollectionPath, weaponId);
        }

        internal void RemoveOwnWeapon(string weaponId)
        {
            RemoveDataModelObject(OwnWeaponCollectionPath, weaponId);
        }

        internal void RemoveCollector(string collectorId)
        {
            RemoveDataModelObject(CollectorsCommonCollectionPath, collectorId);
        }

        internal List<Weapon> GetWeapon()
        {
            return LoadWeaponCollection(WeaponCommonCollectionPath);
        }

        internal List<Collector> GetCollectors()
        {
            return LoadCollectorsCollection(CollectorsCommonCollectionPath);
        }

        internal List<Weapon> GetOwnWeapon()
        {
            return LoadWeaponCollection(OwnWeaponCollectionPath);
        }

        internal void InitDirectories()
        {
            Directory.CreateDirectory(WeaponCommonCollectionPath);
            Directory.CreateDirectory(CollectorsCommonCollectionPath);
            Directory.CreateDirectory(OwnWeaponCollectionPath);
        }

        List<Weapon> LoadWeaponCollection(string path)
        {
            var list = new List<Weapon>();
            var files = Directory.GetFiles(path);

            foreach(string file in files)
            {
                if(Path.GetExtension(file) == c_fileExtension)
                {
                    var json = File.ReadAllText(file);

                    list.Add(JsonSerializer.Deserialize<Weapon>(json));
                }
            }

            return list;
        }

        List<Collector> LoadCollectorsCollection(string path)
        {
            var list = new List<Collector>();
            var files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                if (Path.GetExtension(file) == c_fileExtension)
                {
                    var json = File.ReadAllText(file);

                    list.Add(JsonSerializer.Deserialize<Collector>(json));
                }
            }

            return list;
        }

        void SaveWeapon(string path, Weapon weapon)
        {
            var filePath = GetFilePath(path, weapon.Id);

            var json = JsonSerializer.Serialize<Weapon>(weapon);
            File.WriteAllText(filePath, json);
        }

        void SaveCollector(string path, Collector collector)
        {
            var filePath = GetFilePath(path, collector.Id);

            var json = JsonSerializer.Serialize<Collector>(collector);
            File.WriteAllText(filePath, json);
        }

        void RemoveDataModelObject(string path, string objectId)
        {
            var filePath = GetFilePath(path, objectId);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        string GetFilePath(string path, string objectId)
        {
            return Path.Combine(path, Path.ChangeExtension(objectId, c_fileExtension));
        }

        string m_ownCollectionPath;
        string m_commonCollectionPath;

        const string c_fileExtension = ".json";
        const string c_weaponDirectory = "Weapon";
        const string c_collectorsDirectory = "Collectors";
    }
}
