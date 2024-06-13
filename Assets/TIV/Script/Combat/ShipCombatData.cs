using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Hp, HpP, Atk, AtkP, Def, DefP, CritRate, CritDmg, PhysicsDmg, OpticsDmg, ParticleDmg, PlasmaDmg
}
public class State//���� ��ġ ���ʽ�. ��� � ���� ���� ��ġ ��ȭ��. ���� �� ������� ����
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
public class ShipCombatData : MonoBehaviour
{
    Dictionary<StateType, State> StateStaticBonusDic = new Dictionary<StateType, State>();

    /// <summary>
    /// StateType�� �ش��ϴ� ���� ������ ��ȯ��
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    public float GetFinalState(StateType stateType)
    {
        return StateStaticBonusDic[stateType].CurrentState();
    }
}
