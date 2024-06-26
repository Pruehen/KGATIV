using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using EnumTypes;
using System;
using System.Threading.Tasks;

public static class JsonDataManager
{
    public static JsonCache jsonCache = new JsonCache();
    public static T DataTableListLoad<T>(string saveDataFileName) where T : class, new()
    {
        string filePath = Application.dataPath + saveDataFileName;

        if (!File.Exists(filePath))
            return new T();

        string fileData = File.ReadAllText(filePath);
        T data;

        try
        {
            data = JsonConvert.DeserializeObject<T>(fileData);
            if (data == null)
            {
                data = new T();
                Debug.Log("새 저장 데이터 생성");
            }

            Debug.Log($"데이터 불러오기 완료 : {typeof(T).Name}");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"데이터 불러오기 실패 : {e.Message}");
            return new T();
        }
    }
    public static void DataSaveCommand<T>(T jsonCacheData, string saveDataFileName)
    {
        Task task = DataSaveAsync(jsonCacheData, saveDataFileName);

        task.ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.LogError($"데이터 저장 중 오류 발생: {t.Exception}");
            }
        });
    }
    static async Task DataSaveAsync<T>(T jsonCacheData, string saveDataFileName)
    {
        string filePath = Application.dataPath + saveDataFileName;

        string data = JsonConvert.SerializeObject(jsonCacheData, Formatting.Indented);

        await File.WriteAllTextAsync(filePath, data);

        Debug.Log($"<color=#FFFF00>데이터 저장 완료</color> : {typeof(T).Name}");
    }

    public static EquipTable DataLode_EquipTable(int key)
    {
        EquipTableList equipTableList = jsonCache.EquipTableListCache;
        return equipTableList.list[key];
    }
    public static EquipType_PossibleReinforcementOptionsListTable DataLode_EquipType_PROTable(EquipType key)
    {
        EquipType_PROListTableList equipTypeList = jsonCache.EquipType_MainableStateListTableListCache;
        return equipTypeList.list[(int)key];
    }
    public static WeaponSkillTable DataLode_WeaponSkillTableList(int key)
    {
        WeaponSkillTableList weaponTableList = jsonCache.WeaponSkillTableListCache;
        return weaponTableList.list[key];
    }
    public static ShipTable DataLode_ShipTable(int key)
    {
        ShipTableList tableList = jsonCache.ShipTableListCache;
        return tableList.list[key];
    }
    public static StateType_StateMultipleTable DataLode_StateType_StateMultipleTable(IncreaseableStateType key)
    {
        StateType_StateMultipleTableList tableList = jsonCache.StateType_StateMultipleTableListCache;
        return tableList.list[(int)key];
    }
    public static EquipSetTable DataLode_SetEffectTable(SetType key)
    {
        EquipSetTableList tableList = jsonCache.EquipSetTableListCache;
        return tableList.list[(int)key];
    }
    public static BuffTable DataLode_BuffTable(string key)
    {
        BuffTableDictionary tableDic = jsonCache.BuffTableDictionaryCache;
        return tableDic._dic[key];
    }
    public static UserHaveEquipData DataLode_UserHaveEquipData(string key)
    {
        UserHaveEquipDataDictionary dataDictionary = jsonCache.UserHaveEquipDataDictionaryCache;
        return dataDictionary._dic[key];
    }
    public static UserHaveShipData DataLode_UserHaveShipData(int key)
    {
        UserHaveShipDataList dataList = jsonCache.UserHaveShipDataListCache;
        return dataList.list[key];
    }
    public static UserData DataLode_UserData()
    {
        return jsonCache.UserDataCache;
    }
    //public static T DataLode<T, TList>()
}

public class JsonCache
{
    EquipTableList _equipTableListCache;
    public EquipTableList EquipTableListCache
    {
        get
        {
            if (_equipTableListCache == null)
            {
                _equipTableListCache = JsonDataManager.DataTableListLoad<EquipTableList>(EquipTableList.FilePath());                
            }
            return _equipTableListCache;
        }
    }
    EquipType_PROListTableList _equipType_MainableStateListTableListCache;
    public EquipType_PROListTableList EquipType_MainableStateListTableListCache
    {
        get
        {
            if (_equipType_MainableStateListTableListCache == null)
            {
                _equipType_MainableStateListTableListCache = JsonDataManager.DataTableListLoad<EquipType_PROListTableList>(EquipType_PROListTableList.FilePath());
            }
            return _equipType_MainableStateListTableListCache;
        }
    }
    WeaponSkillTableList _weaponSkillTableListCache;
    public WeaponSkillTableList WeaponSkillTableListCache
    {
        get
        {
            if (_weaponSkillTableListCache == null)
            {
                _weaponSkillTableListCache = JsonDataManager.DataTableListLoad<WeaponSkillTableList>(WeaponSkillTableList.FilePath());
            }
            return _weaponSkillTableListCache;
        }
    }
    ShipTableList _shipTableListCache;
    public ShipTableList ShipTableListCache
    {
        get
        {
            if (_shipTableListCache == null)
            {
                _shipTableListCache = JsonDataManager.DataTableListLoad<ShipTableList>(ShipTableList.FilePath());
            }
            return _shipTableListCache;
        }
    }
    GachaTable _gachaTableCache;
    public GachaTable GachaTableCache
    {
        get
        {
            if (_gachaTableCache == null)
            {
                _gachaTableCache = JsonDataManager.DataTableListLoad<GachaTable>(GachaTable.FilePath());
            }
            return _gachaTableCache;
        }
    }

    StateType_StateMultipleTableList _stateType_StateMultipleTableListCache;
    public StateType_StateMultipleTableList StateType_StateMultipleTableListCache
    {
        get
        {
            if (_stateType_StateMultipleTableListCache == null)
            {
                _stateType_StateMultipleTableListCache = JsonDataManager.DataTableListLoad<StateType_StateMultipleTableList>(StateType_StateMultipleTableList.FilePath());
            }
            return _stateType_StateMultipleTableListCache;
        }
    }
    EquipSetTableList _equipSetTableList;
    public EquipSetTableList EquipSetTableListCache
    {
        get
        {
            if (_equipSetTableList == null)
            {
                _equipSetTableList = JsonDataManager.DataTableListLoad<EquipSetTableList>(EquipSetTableList.FilePath());
            }
            return _equipSetTableList;
        }
    }
    BuffTableDictionary _buffTableDictionaryCache;
    public BuffTableDictionary BuffTableDictionaryCache
    {
        get
        {
            if (_buffTableDictionaryCache == null)
            {
                _buffTableDictionaryCache = JsonDataManager.DataTableListLoad<BuffTableDictionary>(BuffTableDictionary.FilePath());
            }
            return _buffTableDictionaryCache;
        }
    }

    UserData _userData;
    public UserData UserDataCache
    {
        get
        {
            if (_userData == null)
            {
                _userData = JsonDataManager.DataTableListLoad<UserData>(UserData.FilePath());
            }
            return _userData;
        }
    }
    UserHaveEquipDataDictionary _userHaveEquipDataDictionaryCache;
    public UserHaveEquipDataDictionary UserHaveEquipDataDictionaryCache
    {
        get
        {
            if (_userHaveEquipDataDictionaryCache == null)
            {
                _userHaveEquipDataDictionaryCache = JsonDataManager.DataTableListLoad<UserHaveEquipDataDictionary>(UserHaveEquipDataDictionary.FilePath());
                _userHaveEquipDataDictionaryCache.AllDicItemUpdate_EquipedShipKey();
            }
            return _userHaveEquipDataDictionaryCache;
        }
    }
    UserHaveShipDataList _userHaveShipDataListCache;
    public UserHaveShipDataList UserHaveShipDataListCache
    {
        get
        {
            if (_userHaveShipDataListCache == null)
            {
                _userHaveShipDataListCache = JsonDataManager.DataTableListLoad<UserHaveShipDataList>(UserHaveShipDataList.FilePath());
            }
            return _userHaveShipDataListCache;
        }
    }

    public void Lode()
    {
        _equipTableListCache = EquipTableListCache;
        _equipType_MainableStateListTableListCache = EquipType_MainableStateListTableListCache;
        _weaponSkillTableListCache = WeaponSkillTableListCache;
        _shipTableListCache = ShipTableListCache;
        _gachaTableCache = GachaTableCache;
        _stateType_StateMultipleTableListCache = StateType_StateMultipleTableListCache;
        _equipSetTableList = EquipSetTableListCache;
        _buffTableDictionaryCache = BuffTableDictionaryCache;
        _userData = UserDataCache;
        _userHaveEquipDataDictionaryCache = UserHaveEquipDataDictionaryCache;
        _userHaveShipDataListCache = UserHaveShipDataListCache;
    }
}