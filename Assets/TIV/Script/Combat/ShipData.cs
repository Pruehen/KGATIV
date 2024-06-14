using EnumTypes;
using System.Collections.Generic;

public class State
{
    float _curState = 0;
    float _stateMultiplier = 1;

    public State(float curState, float stateMultiplier)
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

    /// <summary>
    /// StateType에 해당하는 최종 스텟을 반환함
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public float GetFinalState(CombatStateType stateType)
    {
        return StateStaticBonusDic[stateType].CurrentState();
    }
}
