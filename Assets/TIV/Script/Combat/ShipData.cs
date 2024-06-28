using EnumTypes;
using System;
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
    public void AddCurState(float value)
    {
        _curState += value;
    }
    /// <summary>
    /// 스탯의 승수를 변화시킴
    /// </summary>
    /// <param name="value"></param>
    public void AddPercentageState(float value)
    {
        _stateMultiplier += value * 0.01f;
    }
    public float CurrentState()
    {
        return _curState * _stateMultiplier;
    }
    public float CurrentState_AddedBuff(State buffState = null)
    {
        if (buffState == null)
        {
            return _curState * _stateMultiplier;
        }
        else
        {
            return (_curState + buffState._curState) * (_stateMultiplier + buffState._stateMultiplier - 1);
        }
    }
}
/// <summary>
/// 함선 자체 데이터. 개함 버프는 포함하지 않음.
/// </summary>
public class ShipData
{
    Dictionary<CombatStateType, State> StateStaticBonusDic = new Dictionary<CombatStateType, State>();    
    ShipTable _shipTable;
    UserHaveShipData _shipData;
    int _id;

    public Action OnAllStaticDataUpdate { get; set; }
    public Action OnBuffDataUpdate { get; set; }
    public int ValidCount_ASet { get; private set; }
    public int ValidCount_BSet { get; private set; }
    public int ValidCount_GSet { get; private set; }
    public int ValidCount_DSet { get; private set; }

    /// <summary>
    /// 유저 함선 생성자
    /// </summary>
    /// <param name="shipData"></param>
    public ShipData(UserHaveShipData shipData)
    {
        _shipData = shipData;
        _id = shipData._shipTablekey;
        _shipTable = JsonDataManager.DataLode_ShipTable(_id);        

        StateStaticBonusDic.Add(CombatStateType.Hp, new State());
        StateStaticBonusDic.Add(CombatStateType.Atk, new State());
        StateStaticBonusDic.Add(CombatStateType.Def, new State());
        StateStaticBonusDic.Add(CombatStateType.CritRate, new State());
        StateStaticBonusDic.Add(CombatStateType.CritDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.PhysicsDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.OpticsDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.ParticleDmg, new State());
        StateStaticBonusDic.Add(CombatStateType.PlasmaDmg, new State());

        AllStaticDataUpdate();
    }

    /// <summary>
    /// 적 함선 생성자
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="atk"></param>
    /// <param name="def"></param>
    public ShipData(float hp, float atk, float def, float multipleValue)
    {
        StateStaticBonusDic.Add(CombatStateType.Hp, new State(hp * multipleValue));
        StateStaticBonusDic.Add(CombatStateType.Atk, new State(atk * multipleValue));
        StateStaticBonusDic.Add(CombatStateType.Def, new State(def * multipleValue));
        StateStaticBonusDic.Add(CombatStateType.CritRate, new State(0));
        StateStaticBonusDic.Add(CombatStateType.CritDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.PhysicsDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.OpticsDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.ParticleDmg, new State(0));
        StateStaticBonusDic.Add(CombatStateType.PlasmaDmg, new State(0));

        ValidCount_ASet = 0;
        ValidCount_BSet = 0;
        ValidCount_GSet = 0;
        ValidCount_DSet = 0;

        _id = -1;
    }
    public void AllStaticDataUpdate()
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

        List<string> equipedItemKeyList = _shipData.GetAllEquipedItemKey();//모든 장착 아이템

        if (equipedItemKeyList != null && equipedItemKeyList.Count != 0)//아이템을 하나라도 장착하고 있을 경우
        {
            Dictionary<SetType, int> validSetKeyDic = new Dictionary<SetType, int>();//현재 장착중인 아이템의 세트 효과 갯수

            foreach (string equipKey in equipedItemKeyList)//장착 중인 아이템 순회
            {
                UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(equipKey);
                SetType setType = equipData._setType;
                if(validSetKeyDic.ContainsKey(setType))
                {
                    validSetKeyDic[setType]++;
                }
                else
                {
                    validSetKeyDic.Add(setType, 1);
                }

                foreach (var stateSet in equipData.GetAllEquipStateSet())//장착 중인 아이템의 모든 스탯 순회
                {
                    switch (stateSet._stateType)
                    {
                        case IncreaseableStateType.Hp:
                            StateStaticBonusDic[CombatStateType.Hp].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.HpMultiple:
                            StateStaticBonusDic[CombatStateType.Hp].AddPercentageState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.Atk:
                            StateStaticBonusDic[CombatStateType.Atk].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.AtkMultiple:
                            StateStaticBonusDic[CombatStateType.Atk].AddPercentageState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.Def:
                            StateStaticBonusDic[CombatStateType.Def].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.DefMultiple:
                            StateStaticBonusDic[CombatStateType.Def].AddPercentageState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.CritRate:
                            StateStaticBonusDic[CombatStateType.CritRate].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.CritDmg:
                            StateStaticBonusDic[CombatStateType.CritDmg].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.PhysicsDmg:
                            StateStaticBonusDic[CombatStateType.PhysicsDmg].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.OpticsDmg:
                            StateStaticBonusDic[CombatStateType.OpticsDmg].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.ParticleDmg:
                            StateStaticBonusDic[CombatStateType.ParticleDmg].AddCurState(stateSet.GetValue());
                            break;
                        case IncreaseableStateType.PlasmaDmg:
                            StateStaticBonusDic[CombatStateType.PlasmaDmg].AddCurState(stateSet.GetValue());
                            break;
                        default:
                            break;
                    }
                }
            }

            ValidCount_ASet = 0;
            ValidCount_BSet = 0;
            ValidCount_GSet = 0;
            ValidCount_DSet = 0;

            foreach (var item in validSetKeyDic)//적용중인 세트효과 키 순회
            {
                SetValidSetEffect(item.Key, item.Value);
            }
        }

        OnAllStaticDataUpdate?.Invoke();
    }
    void SetValidSetEffect(SetType setType, int count)
    {
        switch (setType)
        {
            case SetType.Alpha:
                Set_ASetEffect(setType, count);
                break;
            case SetType.Beta:
                Set_BSetEffect(setType, count);
                break;
            case SetType.Gamma:
                Set_GSetEffect(setType, count);
                break;
            case SetType.Delta:
                Set_DSetEffect(setType, count);
                break;
            default:
                break;
        }
    }
    void Set_ASetEffect(SetType setType, int count)
    {
        EquipSetTable table = JsonDataManager.DataLode_SetEffectTable(setType);
        if (count >= 1)
        {
            StateStaticBonusDic[CombatStateType.Atk].AddPercentageState(JsonDataManager.DataLode_BuffTable(table._set1Key)._buffValueList[0]);
        }
        if (count >= 2)
        {
            StateStaticBonusDic[CombatStateType.PhysicsDmg].AddCurState(JsonDataManager.DataLode_BuffTable(table._set2Key)._buffValueList[0]);
        }
        ValidCount_ASet = count;
    }
    void Set_BSetEffect(SetType setType, int count)
    {
        EquipSetTable table = JsonDataManager.DataLode_SetEffectTable(setType);
        if (count >= 1)
        {
            StateStaticBonusDic[CombatStateType.Def].AddPercentageState(JsonDataManager.DataLode_BuffTable(table._set1Key)._buffValueList[0]);
        }
        if (count >= 2)
        {
            StateStaticBonusDic[CombatStateType.OpticsDmg].AddCurState(JsonDataManager.DataLode_BuffTable(table._set2Key)._buffValueList[0]);
        }
        ValidCount_BSet = count;
    }
    void Set_GSetEffect(SetType setType, int count)
    {
        EquipSetTable table = JsonDataManager.DataLode_SetEffectTable(setType);
        if (count >= 1)
        {
            StateStaticBonusDic[CombatStateType.CritDmg].AddCurState(JsonDataManager.DataLode_BuffTable(table._set1Key)._buffValueList[0]);
        }
        if (count >= 2)
        {
            StateStaticBonusDic[CombatStateType.ParticleDmg].AddCurState(JsonDataManager.DataLode_BuffTable(table._set2Key)._buffValueList[0]);
        }
        ValidCount_GSet = count;
    }
    void Set_DSetEffect(SetType setType, int count)
    {
        EquipSetTable table = JsonDataManager.DataLode_SetEffectTable(setType);
        if (count >= 1)
        {
            StateStaticBonusDic[CombatStateType.CritRate].AddCurState(JsonDataManager.DataLode_BuffTable(table._set1Key)._buffValueList[0]);
        }
        if (count >= 2)
        {
            StateStaticBonusDic[CombatStateType.PlasmaDmg].AddCurState(JsonDataManager.DataLode_BuffTable(table._set2Key)._buffValueList[0]);
        }
        ValidCount_DSet = count;
    }


    /// <summary>
    /// StateType에 해당하는 최종 스텟을 버프를 포함해서 반환함. buffState가 null일 경우, 버프가 없는 기본 스탯을 반환함.
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public float GetFinalState(CombatStateType stateType, State buffState = null)
    {
        return StateStaticBonusDic[stateType].CurrentState_AddedBuff(buffState);
    }
    public float GetFinalState(CombatStateType stateType, ShipBuffManager buffManager)
    {
        return StateStaticBonusDic[stateType].CurrentState_AddedBuff(buffManager.GetFinalState(stateType));
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
    public int GetLevel()
    {
        return _shipData._level;
    }
    public int GetMaxSlot()
    {
        return _shipTable._maxCombatSlot;
    }
    public int GetUseSlot()
    {
        return _shipData.GetUseSlotCount();
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
