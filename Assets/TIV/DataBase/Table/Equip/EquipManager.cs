using EnumTypes;

public static class EquipManager
{
    public static UserHaveEquipData CreateEquip(EquipType equipType)
    {
        EquipTable equipTable = JsonDataManager.DataLode_EquipTable((int)equipType);
        EquipType_PossibleReinforcementOptionsListTable stateTeble = JsonDataManager.DataLode_EquipType_PROTable((int)equipTable._type);

        return new UserHaveEquipData(equipType, 1, stateTeble.GetRandomMainState());
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