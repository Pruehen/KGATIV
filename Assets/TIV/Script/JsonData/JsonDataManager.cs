using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using EnumTypes;
using System;

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
    public static void DataSave<T>(T jsonCacheData, string saveDataFileName)
    {
        string filePath = Application.dataPath + saveDataFileName;

        string data = JsonConvert.SerializeObject(jsonCacheData, Formatting.Indented);

        File.WriteAllText(filePath, data);

        Debug.Log($"데이터 저장 완료 : {typeof(T).Name}");
    }

    public static EquipTable DataLode_EquipTable(int key)
    {
        EquipTableList equipTableList = jsonCache.EquipTableListCache;
        return equipTableList.list[key];
    }
    public static EquipType_PossibleReinforcementOptionsListTable DataLode_EquipType_PROTable(int key)
    {
        EquipType_PROListTableList equipTypeList = jsonCache.EquipType_MainableStateListTableListCache;
        return equipTypeList.list[key];
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
    public static UserHaveEquipData DataLode_UserHaveEquipData(string key)
    {
        UserHaveEquipDataDictionary dataDictionary = jsonCache.UserHaveEquipDataDictionaryCache;
        return dataDictionary._dic[key];
    }
    public static UserHaveItemData DataLode_UserHaveItemData(ItemType itemType)
    {
        UserHaveItemDataList dataList = jsonCache.UserHaveItemDataListCache;
        return dataList.list[(int)itemType];
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

    UserHaveItemDataList _userHaveItemDataListCache;
    public UserHaveItemDataList UserHaveItemDataListCache
    {
        get
        {
            if (_userHaveItemDataListCache == null)
            {
                _userHaveItemDataListCache = JsonDataManager.DataTableListLoad<UserHaveItemDataList>(UserHaveItemDataList.FilePath());
            }
            return _userHaveItemDataListCache;
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


}