using UnityEngine;

public struct Calcf
{
    public static float ShipUpgradePrice(ShipTable table, int level)
    {
        return level * (table._maxCombatSlot * table._maxCombatSlot * table._star * table._star + 37) * 0.1f;
    }

    public static float EquipUPgradePrice(int equipLevel)
    {
        return (equipLevel * 1200 * (((equipLevel / 4) * (equipLevel / 4)) + 1));
    }

    public static float DmgMultiple_Def(float baseDmg, float def)
    {
        return baseDmg / (baseDmg + def);
    }
}
