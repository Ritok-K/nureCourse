using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using WeaponAlmanac.Data_Model.Serializers;
using System.Diagnostics;

namespace WeaponAlmanac.Data_Model
{
    class DataModelRepository
    {
        internal DataModelRepository(string ownCollectionPath, string commonCollectionPath)
        {
            m_ownCollectionPath = ownCollectionPath;
            m_commonCollectionPath = commonCollectionPath;
        }


        #region Properties_CollectionPath
        internal string CommonCollectionPath => Path.GetFullPath(m_commonCollectionPath);
        internal string OwnCollectionPath => Path.GetFullPath(m_ownCollectionPath);

        internal string OwnWeaponCollectionPath => Path.Combine(OwnCollectionPath, c_weaponDirectory);
        internal string WeaponCommonCollectionPath => Path.Combine(CommonCollectionPath, c_weaponDirectory);
        internal string CollectorsCommonCollectionPath => Path.Combine(CommonCollectionPath, c_collectorsDirectory);
        #endregion

        internal void InitDirectories()
        {
            Directory.CreateDirectory(WeaponCommonCollectionPath);
            Directory.CreateDirectory(CollectorsCommonCollectionPath);
            Directory.CreateDirectory(OwnWeaponCollectionPath);
        }


        #region Set Object
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
        #endregion

        #region Remove Object
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
        #endregion

        #region Get Collection
        internal List<Weapon> GetWeapon(IDataModelWeaponFilter filter = null)
        {
            return LoadWeaponCollection(WeaponCommonCollectionPath, filter);
        }

        internal List<Collector> GetCollectors(IDataModelCollectorFilter filter = null)
        {
            return LoadCollectorsCollection(CollectorsCommonCollectionPath, filter);
        }

        internal List<Weapon> GetOwnWeapon(IDataModelWeaponFilter filter = null)
        {
            return LoadWeaponCollection(OwnWeaponCollectionPath, filter);
        }
        #endregion

        #region Utility object methods

        List<Weapon> LoadWeaponCollection(string path, IDataModelWeaponFilter filter)
        {
            var list = new List<Weapon>();
            var files = Directory.GetFiles(path);

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Converters = { new BitmapJsonConverter() }
            };

            foreach (string file in files)
            {
                if(Path.GetExtension(file) == c_fileExtension)
                {
                    var id = Path.GetFileNameWithoutExtension(file);
                    if (filter?.PassId(id) ?? true)
                    {
                        var json = File.ReadAllText(file);
                        var obj = JsonSerializer.Deserialize<Weapon>(json, jsonOptions);
                        Debug.Assert(obj.Id == id);

                        if (filter?.PassDataModelObject(obj) ?? true)
                        {
                            list.Add(obj);
                        }
                    }
                }
            }

            return list;
        }

        List<Collector> LoadCollectorsCollection(string path, IDataModelCollectorFilter filter)
        {
            var list = new List<Collector>();
            var files = Directory.GetFiles(path);

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            foreach (string file in files)
            {
                if (Path.GetExtension(file) == c_fileExtension)
                {
                    var id = Path.GetFileNameWithoutExtension(file);
                    if (filter?.PassId(id) ?? true)
                    {
                        var json = File.ReadAllText(file);
                        var obj = JsonSerializer.Deserialize<Collector>(json, jsonOptions);
                        Debug.Assert(obj.Id == id);

                        if (filter?.PassDataModelObject(obj) ?? true)
                        {
                            list.Add(obj);
                        }
                    }
                }
            }

            return list;
        }

        void SaveWeapon(string path, Weapon weapon)
        {
            var filePath = GetFilePath(path, weapon.Id);

            var jsonOptions = new JsonSerializerOptions() 
            {
                WriteIndented = true, 
                Converters = { new BitmapJsonConverter() }
            };

            var json = JsonSerializer.Serialize<Weapon>(weapon, jsonOptions);
            File.WriteAllText(filePath, json);
        }

        void SaveCollector(string path, Collector collector)
        {
            var filePath = GetFilePath(path, collector.Id);

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<Collector>(collector, jsonOptions);
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
        #endregion

        string m_ownCollectionPath;
        string m_commonCollectionPath;

        const string c_fileExtension = ".json";
        const string c_weaponDirectory = "Weapon";
        const string c_collectorsDirectory = "Collectors";
    }
}
