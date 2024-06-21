using EnumTypes;
using System;
using UnityEngine;

public class ShipCombatData : MonoBehaviour
{
    ShipData _shipData;

    [Header("�÷��̾� �Լ��� ���, �ش� �Լ��� Ű")]
    [SerializeField] int shipTableKey = -1;
    public int GetShipTableKey() { return shipTableKey; }

    [Header("�� �Լ��� ���, ü��, ���ݷ�, ����")]
    [SerializeField] float hp;
    [SerializeField] float atk;
    [SerializeField] float def;

    float _curHp;
    public void Init()
    {
        if (shipTableKey >= 0)
        {
            _shipData = kjh.GameLogicManager.GetShipData(shipTableKey);
        }
        else
        {
            _shipData = new ShipData(hp, atk, def);            
        }
        _curHp = _shipData.GetFinalState(CombatStateType.Hp);
    }
    public void Register_OnDead(Action callBack)
    {
        _onDead += callBack;
    }

    public void Hit(float originDmg)
    {        
        float calcedDmg = originDmg * (1 / Mathf.Exp(-Mathf.Log(0.5f) / 1500 * _shipData.GetFinalState(CombatStateType.Def)));
        _curHp -= calcedDmg;

        if(_curHp <= 0)
        {
            _onDead?.Invoke();
        }
    }

    Action _onDead;
}
