using EnumTypes;
using System.Collections.Generic;
public class State
{
    float _curState = 0;
    float _stateMultiplier = 1;

    public State()
    {
        Init();
    }
    public void Init(float curState = 0, float stateMultiplier = 1)
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

        StateStaticBonusDic.Add(CombatStateType.Hp, new State());
        StateStaticBonusDic.Add(CombatStateType.Atk, new State());
        StateStaticBonusDic.Add(CombatStateType.Def, new State());
        StateStaticBonusDic.Add(CombatStateType.CritRate, new State());
        StateStaticBonusDic.Add(CombatStateType.CritDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.PhysicsDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.OpticsDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.ParticleDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.PlasmaDmg, new State());

        AllDataUpdate();
    }
    void AllDataUpdate()
    {
        int level = _shipData._level;

        StateStaticBonusDic[CombatStateType.Hp].Init(_shipTable.GetHp(level));
        StateStaticBonusDic[CombatStateType.Atk].Init(_shipTable.GetAtk(level));
        StateStaticBonusDic[CombatStateType.Def].Init(_shipTable.GetDef(level));
        StateStaticBonusDic[CombatStateType.CritRate].Init(5);
        StateStaticBonusDic[CombatStateType.CritDmg].Init(50);
        StateStaticBonusDic[CombatStateType.PhysicsDmg].Init();
        StateStaticBonusDic[CombatStateType.OpticsDmg].Init();
        StateStaticBonusDic[CombatStateType.ParticleDmg].Init();
        StateStaticBonusDic[CombatStateType.PlasmaDmg].Init();

        List<string> equipedItemKeyList = _shipData.GetAllEquipedItemKey();
        if (equipedItemKeyList != null && equipedItemKeyList.Count != 0)
        {
            foreach (string equipKey in equipedItemKeyList)//장착 중인 아이템 순회
            {
                UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(equipKey);
                foreach (var stateSet in equipData.GetAllEquipStateSet())//장착 중인 아이템의 모든 스탯 순회
                {
                    switch (stateSet._stateType)
                    {
                        case IncreaseableStateType.Hp:
                            StateStaticBonusDic[CombatStateType.Hp].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.HpMultiple:
                            StateStaticBonusDic[CombatStateType.Hp].ChangePercentageState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.Atk:
                            StateStaticBonusDic[CombatStateType.Atk].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.AtkMultiple:
                            StateStaticBonusDic[CombatStateType.Atk].ChangePercentageState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.Def:
                            StateStaticBonusDic[CombatStateType.Def].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.DefMultiple:
                            StateStaticBonusDic[CombatStateType.Def].ChangePercentageState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.CritRate:
                            StateStaticBonusDic[CombatStateType.CritRate].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.CritDmg:
                            StateStaticBonusDic[CombatStateType.CritDmg].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.PhysicsDmg:
                            StateStaticBonusDic[CombatStateType.PhysicsDmg].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.OpticsDmg:
                            StateStaticBonusDic[CombatStateType.OpticsDmg].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.ParticleDmg:
                            StateStaticBonusDic[CombatStateType.ParticleDmg].ChangeCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.PlasmaDmg:
                            StateStaticBonusDic[CombatStateType.PlasmaDmg].ChangeCurState(stateSet.GetValue());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
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
    //public string GetFinalStateText(CombatStateType stateType)
    //{
    //    float value = GetFinalState(stateType);
    //    string text = string.Empty;

    //    switch (stateType)
    //    {
    //        case CombatStateType.Hp:
    //            text = $"{value:F0}";
    //            break;
    //        case CombatStateType.Atk:
    //            text = $"{value:F0}";
    //            break;
    //        case CombatStateType.Def:
    //            text = $"{value:F0}";
    //            break;
    //        case CombatStateType.CritRate:
    //            text = $"{value:F1}%";
    //            break;
    //        case CombatStateType.CritDmg:
    //            text = $"{value:F1}%";
    //            break;
    //        case CombatStateType.PhysicsDmg:
    //            text = $"{value:F1}%";
    //            break;
    //        case CombatStateType.OpticsDmg:
    //            text = $"{value:F1}%";
    //            break;
    //        case CombatStateType.ParticleDmg:
    //            text = $"{value:F1}%";
    //            break;
    //        case CombatStateType.PlasmaDmg:
    //            text = $"{value:F1}%";
    //            break;
    //        default:
    //            text = "알 수 없음";
    //            break;
    //    }
    //    return text;
    //}
    public string GetName()
    {
        return _shipTable._name;
    }
    public int GetShipClass()
    {
        return _shipTable._shipClass;
    }
    public int GetShipStar()
    {
        return _shipTable._star;
    }
    public int GetMaxSlot()
    {
        return _shipTable._maxCombatSlot;
    }
}
