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
    /// ������ ���� ��ġ�� ��ȭ��Ŵ
    /// </summary>
    /// <param name="value"></param>
    public void ChangeCurState(float value)
    {
        _curState += value;
    }
    /// <summary>
    /// ������ �¼��� ��ȭ��Ŵ
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
    /// StateType�� �ش��ϴ� ���� ������ ��ȯ��
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public float GetFinalState(CombatStateType stateType)
    {
        return StateStaticBonusDic[stateType].CurrentState();
    }
}
