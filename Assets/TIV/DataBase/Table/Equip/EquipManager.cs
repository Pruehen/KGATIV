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
                validKeyList.Add(item._key);
            }
        }
        return validKeyList;
    }

    /// <summary>
    /// �ش� ��Ʈ ȿ���� �������� �������� ������ ������ִ� �޼���
    /// </summary>
    /// <param name="setType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<UserHaveEquipData> RandomEquipDrop(SetType setType, int count)
    {
        List<int> validEquipKeyList = GetValidEquipTable(setType);
        List<UserHaveEquipData> dropItem = new List<UserHaveEquipData>();

        for (int i = 0; i < count; i++)
        {
            int randomItemKey = validEquipKeyList[Random.Range(0, validEquipKeyList.Count)];
            UserHaveEquipData newItem = CreateEquip(randomItemKey, setType);
            dropItem.Add(newItem);
        }

        return dropItem;
    }
    /// <summary>
    /// �ش� Ű �������� ���� ������ ���� �� �������ִ� �޼���
    /// </summary>
    /// <param name="equipTableKey"></param>
    /// <param name="setType"></param>
    /// <returns></returns>
    static UserHaveEquipData CreateEquip(int equipTableKey, SetType setType)
    {
        EquipTable equipTable = JsonDataManager.DataLode_EquipTable(equipTableKey);
        EquipType_PossibleReinforcementOptionsListTable stateTeble = JsonDataManager.DataLode_EquipType_PROTable((int)equipTable._type);
        IncreaseableStateType mainState = stateTeble.GetRandomMainState();

        return new UserHaveEquipData(equipTableKey, setType, mainState);
    }

    public static void EquipLevelUp(UserHaveEquipData equip, int plusLevel)
    {
        string msg = string.Empty;
        equip.LevelUp(plusLevel, out msg);
    }

    public static void EquipOptimize(UserHaveEquipData equip, int plusLevel)
    {
        string msg = string.Empty;
        equip.Optimize(plusLevel, out msg);
    }
}