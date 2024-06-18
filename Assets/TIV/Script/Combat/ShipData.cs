using EnumTypes;
using System;
using System.Collections.Generic;

public class State
{
    float _curState = 0;
    float _stateMultiplier = 1;

    public State(float curState = 0, float stateMultiplier = 1)
    {
        _curState = curState;
        _stateMultiplier = stateMultiplier;
    }

    /// <summary>
    /// 스탯의 고정 수치를 변화시킴
    /// </summary>
    /// <param name="value"></param>
    public void ChangeCurState(float value)
    {
        _curState += value;
    }
    /// <summary>
    /// 스탯의 승수를 변화시킴
    /// </summary>
    /// <param name="value"></param>
    public void ChangePercentageState(float value)
    {
        _stateMultiplier += value;
    }
    public float CurrentState()
    {
        return _curState * _stateMultiplier;
    }
}
public class ShipData
{
    Dictionary<CombatStateType, State> StateStaticBonusDic = new Dictionary<CombatStateType, State>();
    ShipTable _shipTable;
    UserHaveShipData _shipData;

    public ShipData(UserHaveShipData shipData)
    {
        _shipData = shipData;
        _shipTable = JsonDataManager.DataLode_ShipTable(shipData._shipTablekey);
        int level = shipData._level;

        StateStaticBonusDic.Add(CombatStateType.Hp, new State(_shipTable.GetHp(level)));
        StateStaticBonusDic.Add(CombatStateType.Atk, new State(_shipTable.GetAtk(level)));
        StateStaticBonusDic.Add(CombatStateType.Def, new State(_shipTable.GetDef(level)));
        StateStaticBonusDic.Add(CombatStateType.CritRate, new State(5));
        StateStaticBonusDic.Add(CombatStateType.CritDmg, new State(50));
        StateStaticBonusDic.Add(CombatStateType.PhysicsDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.OpticsDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.ParticleDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.PlasmaDmg, new State());
    }
    /// <summary>
    /// StateType에 해당하는 최종 스텟을 반환함
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public float GetFinalState(CombatStateType stateType)
    {
        return StateStaticBonusDic[stateType].CurrentState();
    }
    public string GetFinalStateText(CombatStateType stateType)
    {
        float value = GetFinalState(stateType);
        string text = string.Empty;

        switch (stateType)
        {
            case CombatStateType.Hp:
                text = $"{value:F0}";
                break;
            case CombatStateType.Atk:
                text = $"{value:F0}";
                break;
            case CombatStateType.Def:
                text = $"{value:F0}";
                break;
            case CombatStateType.CritRate:
                text = $"{value:F1}%";
                break;
            case CombatStateType.CritDmg:
                text = $"{value:F1}%";
                break;
            case CombatStateType.PhysicsDmg:
                text = $"{value:F1}%";
                break;
            case CombatStateType.OpticsDmg:
                text = $"{value:F1}%";
                break;
            case CombatStateType.ParticleDmg:
                text = $"{value:F1}%";
                break;
            case CombatStateType.PlasmaDmg:
                text = $"{value:F1}%";
                break;
            default:
                text = "알 수 없음";
                break;
        }
        return text;
    }
    public string[] GetAllFinalStateText()
    {
        string[] stringDataArray = new string[12];
        stringDataArray[0] = _shipTable.GetName();
        stringDataArray[1] = _shipTable.GetClass();
        stringDataArray[2] = _shipTable.GetStar();
        for (int i = 3; i < stringDataArray.Length; i++)
        {
            stringDataArray[i] = GetFinalStateText((CombatStateType)i-3);
        }
        return stringDataArray;
    }
}
