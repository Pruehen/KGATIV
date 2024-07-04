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
                //if(item._type == EquipType.Weapon)
                //{
                //    ListKeyAdd(validKeyList, item._key, 1);
                //}
                //else
                //{
                //    ListKeyAdd(validKeyList, item._key, 6);
                //}
                ListKeyAdd(validKeyList, item._key, 1);
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
    public static List<string> RandomEquipDrop(SetType setType, int count)
    {
        List<string> addedEquipKeyList = new List<string>();
        List<int> validEquipKeyList = GetValidEquipTable(setType);
        UserHaveEquipDataPack data = JsonDataManager.jsonCache.UserHaveEquipDataPackCache;

        for (int i = 0; i < count; i++)
        {
            int randomItemKey = validEquipKeyList[Random.Range(0, validEquipKeyList.Count)];
            UserHaveEquipData newItem = CreateEquip(randomItemKey, setType, i);
            data.Add(newItem._itemUniqueKey, newItem);
            addedEquipKeyList.Add(newItem._itemUniqueKey);
        }

        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveEquipDataPackCache, UserHaveEquipDataPack.FilePath());
        return addedEquipKeyList;
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

        if(equipTable._weaponSkillKey == -1)//���� ��ų�� ���� ���, ��Ʈ ȿ�� �ο�
        {
            return new UserHaveEquipData(createNum, equipTableKey, setType, mainState);
        }
        else//���� ��ų�� �ִ� ���, ��Ʈ ȿ�� ����
        {
            return new UserHaveEquipData(createNum, equipTableKey, (SetType)(-1), mainState);
        }        
    }

    public static List<string> PrintEquipData(UserHaveEquipData equip)
    {
        UserHaveEquipDataPack data = JsonDataManager.jsonCache.UserHaveEquipDataPackCache;
        List<string> stringData = new List<string>();
        foreach (var item in data.cacheList)
        {
            EquipTable tabledata = JsonDataManager.DataLode_EquipTable(item._equipTableKey);
            stringData.Add($"{EnumTextData.EquipTypeText(tabledata._type)}");
            stringData.Add($"{tabledata._star}��");
            stringData.Add(tabledata._name);
            stringData.Add(tabledata._info);

            stringData.Add($"{item._level} ����");
            float addStateValue = item._mainState._level * JsonDataManager.DataLode_StateType_StateMultipleTable(item._mainState._stateType)._multipleValue;
            stringData.Add($"{EnumTextData.EquipTypeText(item._mainState._stateType, addStateValue)}");
        }
        return stringData;
    }
}