using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

public static class EquipManager
{
    static Dictionary<SetType, List<int>> _setType_validEquipTableKey = new Dictionary<SetType, List<int>>();
    /// <summary>
    /// ��Ʈ�� ��� ���� ������ ������ ��ȯ�ϴ� �޼���. ���빰�� ������� �� �����͸� ����.
    /// </summary>
    /// <param name="setType"></param>
    /// <returns></returns>
    static List<int> GetValidEquipTable(SetType setType)
    {
        if(_setType_validEquipTableKey.ContainsKey(setType) == false || _setType_validEquipTableKey[setType].Count == 0)
        {
            _setType_validEquipTableKey.Add(setType, LodeValidEquipTable(setType));
        }

        return _setType_validEquipTableKey[setType];
    }
    /// <summary>
    /// �����͸� �����ؼ� �� ��Ʈ�� ��� ������ ������ Ȯ�� �� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="setType"></param>
    /// <returns></returns>
    static List<int> LodeValidEquipTable(SetType setType)
    {
        EquipTableList totalTable = JsonDataManager.jsonCache.EquipTableListCache;
        List<int> validKeyList = new List<int>();

        foreach (EquipTable item in totalTable.list)
        {
            if(item._validSetList.Contains(setType))
            {                
                if(item._type == EquipType.Weapon)
                {
                    ListKeyAdd(validKeyList, item._key, 1);
                }
                else
                {
                    ListKeyAdd(validKeyList, item._key, 6);
                }
            }
        }
        return validKeyList;
    }
    static void ListKeyAdd(List<int> targetList, int value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            targetList.Add(value);
        }
    }

    /// <summary>
    /// �ش� ��Ʈ ȿ���� �������� �������� ������ ������ִ� �޼���
    /// </summary>
    /// <param name="setType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static void RandomEquipDrop(SetType setType, int count)
    {
        List<int> validEquipKeyList = GetValidEquipTable(setType);
        UserHaveEquipDataDictionary data = JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache;

        for (int i = 0; i < count; i++)
        {
            int randomItemKey = validEquipKeyList[Random.Range(0, validEquipKeyList.Count)];
            UserHaveEquipData newItem = CreateEquip(randomItemKey, setType, i);
            data._dic.Add(newItem._itemUniqueKey, newItem);
        }

        JsonDataManager.DataSave<UserHaveEquipDataDictionary>(JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache, UserHaveEquipDataDictionary.FilePath());
    }
    /// <summary>
    /// �ش� Ű �������� ���� ������ ���� �� �������ִ� �޼���
    /// </summary>
    /// <param name="equipTableKey"></param>
    /// <param name="setType"></param>
    /// <returns></returns>
    static UserHaveEquipData CreateEquip(int equipTableKey, SetType setType, int createNum)
    {
        EquipTable equipTable = JsonDataManager.DataLode_EquipTable(equipTableKey);
        EquipType_PossibleReinforcementOptionsListTable stateTeble = JsonDataManager.DataLode_EquipType_PROTable(equipTable._type);
        IncreaseableStateType mainState = stateTeble.GetRandomMainState();

        return new UserHaveEquipData(createNum, equipTableKey, setType, mainState);
    }

    public static List<string> PrintEquipData(UserHaveEquipData equip)
    {
        UserHaveEquipDataDictionary data = JsonDataManager.jsonCache.UserHaveEquipDataDictionaryCache;
        List<string> stringData = new List<string>();
        foreach (var item in data._dic)
        {
            EquipTable tabledata = JsonDataManager.DataLode_EquipTable(item.Value._equipTableKey);
            stringData.Add($"{EnumTextData.EquipTypeText(tabledata._type)}");
            stringData.Add($"{tabledata._star}��");
            stringData.Add(tabledata._name);
            stringData.Add(tabledata._info);

            stringData.Add($"{item.Value._level} ����");
            float addStateValue = item.Value._mainState._level * JsonDataManager.DataLode_StateType_StateMultipleTable(item.Value._mainState._stateType)._multipleValue;
            stringData.Add($"{EnumTextData.EquipTypeText(item.Value._mainState._stateType, addStateValue)}");
        }
        return stringData;
    }
}