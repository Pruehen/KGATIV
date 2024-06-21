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
    public State(float state)
    {
        Init(state, 1);
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
        _stateMultiplier += value * 0.01f;
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

    /// <summary>
    /// 유저 함선 생성자
    /// </summary>
    /// <param name="shipData"></param>
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

    /// <summary>
    /// 적 함선 생성자
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="atk"></param>
    /// <param name="def"></param>
    public ShipData(float hp, float atk, float def)
    {
        StateStaticBonusDic.Add(CombatStateType.Hp, new State(hp));
        StateStaticBonusDic.Add(CombatStateType.Atk, new State(atk));
        StateStaticBonusDic.Add(CombatStateType.Def, new State(def));
        StateStaticBonusDic.Add(CombatStateType.CritRate, new State(0));
        StateStaticBonusDic.Add(CombatStateType.CritDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.PhysicsDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.OpticsDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.ParticleDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.PlasmaDmg, new State(0));
    }
    public void AllDataUpdate()
    {
        int level = _shipData._level;

        StateStaticBonusDic[CombatStateType.Hp].Init(_shipTable.GetHp(level));
        StateStaticBonusDic[CombatStateType.Atk].Init(_shipTable.GetAtk(level));
        StateStaticBonusDic[CombatStateType.Def].Init(_shipTable.GetDef(level));
        StateStaticBonusDic[CombatStateType.CritRate].Init(5);
        StateStaticBonusDic[CombatStateType.CritDmg].Init(50);
        StateStaticBonusDic[CombatStateType.PhysicsDmg].Init(0);
        StateStaticBonusDic[CombatStateType.OpticsDmg].Init(0);
        StateStaticBonusDic[CombatStateType.ParticleDmg].Init(0);
        StateStaticBonusDic[CombatStateType.PlasmaDmg].Init(0);

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
    public List<string> GetEquipedCombatItemList()
    {
        return _shipData._combatSlotItemKeyList;
    }
    public string GetEquipedEngine()
    {
        return _shipData._thrusterSlotItemKey;
    }
    public string GetEquipedReactor()
    {
        return _shipData._reactorSlotItemKey;
    }
    public string GetEquipedRadiator()
    {
        return _shipData._radiatorSlotItemKey;
    }
}
