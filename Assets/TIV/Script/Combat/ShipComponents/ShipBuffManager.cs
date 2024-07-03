using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuffManager : MonoBehaviour
{
    ShipCombatData CombatData;
    ShipData _shipData;//참조용. 사용 가능한 버프인지 파악하는 용도로만 사용할 것.

    BuffTable _a4setTable;
    bool _isActive_A4Set;

    BuffTable _b4setTable;
    public bool _isActive_B4Set { get; private set; }

    BuffTable _g4setTable;
    bool _isActive_G4SetDebuff;
    float _g4setDebuffTime;

    BuffTable _d4setTable;    
    Queue<bool> _d4BuffCheckerQueue = new Queue<bool>();

    Dictionary<CombatStateType, State> StateBuffBonusDic = new Dictionary<CombatStateType, State>();

    /// <summary>
    /// 현재 개함 적용중인 버프 스탯 클래스 객체를 반환.
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public State GetFinalState(CombatStateType stateType)
    {
        if (StateBuffBonusDic.ContainsKey(stateType))
        {
            return StateBuffBonusDic[stateType];
        }
        else
        {
            return null;
        }
    }

    public void Init(ShipData shipData)
    {
        _shipData = shipData;
        CombatData = GetComponent<ShipCombatData>();

        _a4setTable = JsonDataManager.DataLode_BuffTable(JsonDataManager.DataLode_SetEffectTable(SetType.Alpha)._set4Key);
        _b4setTable = JsonDataManager.DataLode_BuffTable(JsonDataManager.DataLode_SetEffectTable(SetType.Beta)._set4Key);
        _g4setTable = JsonDataManager.DataLode_BuffTable(JsonDataManager.DataLode_SetEffectTable(SetType.Gamma)._set4Key);
        _d4setTable = JsonDataManager.DataLode_BuffTable(JsonDataManager.DataLode_SetEffectTable(SetType.Delta)._set4Key);
    }

    private void Update()
    {
        BuffCheck_G4Set_OnUpdate();
        BuffCheck_A4Set_OnUpdate(CombatData.GetHpRatio());
    }
    /// <summary>
    /// 고정 버프 수치를 변화시키는 메서드. 예) 공격력 1000 증가, 물리 피해 보너스 30% 증가 (value는 1000, 30)
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="addValue"></param>
    void AddBuffState_CurState(CombatStateType stateType, float addValue)
    {
        if (StateBuffBonusDic.ContainsKey(stateType) == false)
        {
            StateBuffBonusDic.Add(stateType, new State(0));
        }
        StateBuffBonusDic[stateType].AddCurState(addValue);
        kjh.GameLogicManager.Instance.UpdateSelectShipData();
    }
    /// <summary>
    /// 퍼센테이지 버프 수치를 변화시키는 메서드. 예) 공격력 30% 증가 (value는 30)
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="addValue"></param>
    void AddBuffState_MultiState(CombatStateType stateType, float addValue)
    {
        if (StateBuffBonusDic.ContainsKey(stateType) == false)
        {
            StateBuffBonusDic.Add(stateType, new State(0));
        }
        StateBuffBonusDic[stateType].AddPercentageState(addValue);
        kjh.GameLogicManager.Instance.UpdateSelectShipData();
    }

    public void BuffCheck_A4Set_OnUpdate(float hpRatio)
    {
        if (_shipData.ValidCount_ASet < 4)
        {
            SetActive_A4Set(false);
        }
        else
        {
            if (hpRatio >= _a4setTable._buffValueList[0] * 0.01f)
            {
                SetActive_A4Set(true);
            }
            else
            {
                SetActive_A4Set(false);
            }
        }
    }
    void SetActive_A4Set(bool value)
    {
        if (value == _isActive_A4Set)
            return;

        _isActive_A4Set = value;
        if(value == true)//값이 true로 변경되었을 경우
        {
            AddBuffState_CurState(CombatStateType.PhysicsDmg, _a4setTable._buffValueList[1]);
        }
        else//값이 false로 변경되었을 경우
        {
            AddBuffState_CurState(CombatStateType.PhysicsDmg, -_a4setTable._buffValueList[1]);
        }
    }
    public float BuffCheck_B4Set_OnSetCollDownValue(float originCollTime)
    {
        if(_shipData.ValidCount_BSet >= 4)
        {
            return originCollTime * (1 - (_b4setTable._buffValueList[0] * 0.01f));
        }
        else
        {
            return originCollTime;
        }
    }

    public bool BuffCheck_G4Set_OnFire(out string debuffKey)
    {
        if(_shipData.ValidCount_GSet >= 4)
        {            
            debuffKey = _g4setTable._buffKey;            
            return true;
        }
        else
        {
            debuffKey = null;
            return false;
        }        
    }

    public void BuffCheck_G4Set_OnHit_TryAddDebuff(WeaponProjectileType type, List<string> hasDebuffKey)
    {
        if (hasDebuffKey != null && hasDebuffKey.Contains(_g4setTable._buffKey) && type == WeaponProjectileType.Particle)
        {
            if (_isActive_G4SetDebuff == false)
            {
                _isActive_G4SetDebuff = true;
                _g4setDebuffTime = _g4setTable._buffValueList[0];
            }
        }
    }
    /// <summary>
    /// 감마4 세트 디버프가 적용되어있지 않을 경우 1, 적용되어 있을 경우 감소된 방어력 비율을 반환함
    /// </summary>
    /// <returns></returns>
    public float BuffCheck_G4Set_OnHit_ValidDefRatio()
    {        
        return _isActive_G4SetDebuff ? 1 - (_g4setTable._buffValueList[1] * 0.01f) : 1;
    }

    void BuffCheck_G4Set_OnUpdate()
    {
        if(_isActive_G4SetDebuff == true)
        {
            _g4setDebuffTime -= Time.deltaTime;
            if(_g4setDebuffTime <= 0)
            {
                _isActive_G4SetDebuff = false;
            }
        }
    }

    /// <summary>
    /// 발사 시 버프 부여가 가능할 경우 버프를 부여하며 플라즈마 피해를 올림.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public void BuffCheck_D4Set_OnFire()
    {
        if (_shipData.ValidCount_DSet < 4)
            return;

        if(true)//세트 효과를 가진 객체가 무기를 발사했을 때
        {
            _d4BuffCheckerQueue.Enqueue(true);//버프 스택 인큐
            if (_d4BuffCheckerQueue.Count <= _d4setTable._buffValueList[2])//6중첩 이하일 경우
            {
                AddBuffState_CurState(CombatStateType.PlasmaDmg, _d4setTable._buffValueList[1]);//버프 스택 증가
            }
            StartCoroutine(BuffCheckCoroutine_D4Set(_d4setTable._buffValueList[0]));//버프 지속시간 후 디큐 코루틴
        }
        //int buffStack = Mathf.Min(_d4BuffCheckerQueue.Count, (int)(_d4setTable._buffValueList[2]));
        //return _d4setTable._buffValueList[1] * buffStack;
    }

    IEnumerator BuffCheckCoroutine_D4Set(float time)
    {
        yield return new WaitForSeconds(time);        
        _d4BuffCheckerQueue.Dequeue();
        if (_d4BuffCheckerQueue.Count < _d4setTable._buffValueList[2])//5중첩 이하일 경우
        {
            AddBuffState_CurState(CombatStateType.PlasmaDmg, -_d4setTable._buffValueList[1]);//버프 스택 감소
        }
    }
}
