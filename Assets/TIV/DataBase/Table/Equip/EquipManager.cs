using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

public static class EquipManager
{
    static Dictionary<SetType, List<int>> _setType_validEquipTableKey = new Dictionary<SetType, List<int>>();
    /// <summary>
    /// 세트별 드랍 가능 아이템 종류를 반환하는 메서드. 내용물이 비어있을 시 데이터를 참조.
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
    /// 데이터를 참조해서 각 세트별 드랍 아이템 종류를 확인 후 반환하는 메서드
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
    /// 해당 세트 효과의 아이템을 랜덤으로 일정량 만들어주는 메서드
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
    /// 해당 키 아이템의 메인 스탯을 정한 후 생성해주는 메서드
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