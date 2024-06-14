using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using EnumTypes;
using System;

public static class JsonDataManager
{
    public static JsonCache jsonCache = new JsonCache();
    public static T DataTableListLoad<T>(string saveDataFileName)
    {
        if (!File.Exists(Application.dataPath + saveDataFileName))
            return default(T);
        
        string fileData = File.ReadAllText(Application.dataPath + saveDataFileName);
        T data;

        try
        {
            data = JsonConvert.DeserializeObject<T>(fileData);            
            if (data == null)
            {
                data = default(T);                
                Debug.Log("새 저장 데이터 생성");
            }

            Debug.Log($"데이터 불러오기 완료 : {typeof(T).Name}");
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 불러오기 실패 : {e.Message}");
            data = default(T);
            return data;
        }
    }
    static void DataTableListSave<T>(string saveDataFileName)
    {
        string folderPath = Application.dataPath + saveDataFileName;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        T saveData = jsonCache.GetCacheData<T>();
        string data = JsonConvert.SerializeObject(saveData, Formatting.Indented);

        File.WriteAllText(folderPath + saveDataFileName, data);

        Debug.Log($"데이터 저장 완료 : {typeof(T).Name}");
    }

    public static EquipTable DataLode_EquipTable(EquipType key)
    {
        EquipTableList equipTableList = (jsonCache.equipTableListCache == null) ? 
            DataTableListLoad<EquipTableList>(EquipTableList.FilePath()) : 
            jsonCache.equipTableListCache;

        jsonCache.equipTableListCache = equipTableList;
        return equipTableList.list[(int)key];
    }
    public static EquipType_PossibleReinforcementOptionsListTable DataLode_EquipType_PROTable(EquipType key)
    {
        EquipType_PROListTableList equipTypeList = (jsonCache.equipType_MainableStateListTableListCache == null) ?
            DataTableListLoad<EquipType_PROListTableList>(EquipType_PROListTableList.FilePath()) :
            jsonCache.equipType_MainableStateListTableListCache;

        jsonCache.equipType_MainableStateListTableListCache = equipTypeList;
        return equipTypeList.list[(int)key];
    }
}

public class JsonCache
{
    public EquipTableList equipTableListCache;
    public EquipType_PROListTableList equipType_MainableStateListTableListCache;
    public ShipTableList shipTableListCache;
    public StateType_StateMultipleTableList stateType_StateMultipleTableListCache;

    public T GetCacheData<T>()
    {
        if (typeof(T) == typeof(EquipTableList))
        {
            return (T)(object)equipTableListCache;
        }
        else if (typeof(T) == typeof(EquipType_PROListTableList))
        {
            return (T)(object)equipType_MainableStateListTableListCache;
        }
        else
        {
            throw new InvalidOperationException("Unsupported type.");
        }
    }
}