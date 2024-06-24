using EnumTypes;
using System;
using UnityEngine;

public class ShipCombatData : MonoBehaviour
{
    ShipData _shipData;

    [Header("플레이어 함선일 경우, 해당 함선의 키")]
    [SerializeField] int shipTableKey = -1;
    public int GetShipTableKey() { return shipTableKey; }

    [Header("적 함선일 경우, 체력, 공격력, 방어력")]
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

    public void Hit(float originDmg, WeaponProjectileType type, bool isCrit)
    {
        float calcedDmg = originDmg * 1000 / (1000 + _shipData.GetFinalState(CombatStateType.Def));
        _curHp -= calcedDmg;
        kjh.GameLogicManager.Instance.OnDameged(calcedDmg, type, this.transform.position, isCrit);

        if(_curHp <= 0)
        {
            _onDead?.Invoke();
        }
    }
    public float GetDmg(WeaponSkillTable table, out bool isCrit)
    {
        float atk = _shipData.GetFinalState(CombatStateType.Atk);
        float dmgBonus;
        switch (table._weaponProjectileType)
        {
            case WeaponProjectileType.Physics:
                dmgBonus = _shipData.GetFinalState(CombatStateType.PhysicsDmg);
                break;
            case WeaponProjectileType.Optics:
                dmgBonus = _shipData.GetFinalState(CombatStateType.OpticsDmg);
                break;
            case WeaponProjectileType.Particle:
                dmgBonus = _shipData.GetFinalState(CombatStateType.ParticleDmg);
                break;
            case WeaponProjectileType.Plasma:
                dmgBonus = _shipData.GetFinalState(CombatStateType.PlasmaDmg);
                break;
            default:
                dmgBonus = 0;
                break;
        }
        dmgBonus = 1 + (dmgBonus * 0.01f);

        float critRate = _shipData.GetFinalState(CombatStateType.CritRate);
        float critDmg = _shipData.GetFinalState(CombatStateType.CritDmg);
        float random = UnityEngine.Random.Range(0, 100);
        float critDmgBonus;
        if(critRate >= random)
        {
            critDmgBonus = 1 + (critDmg * 0.01f);
            isCrit = true;
        }
        else
        {
            critDmgBonus = 1;
            isCrit = false;
        }

        float finalDmg = atk * dmgBonus * critDmgBonus * UnityEngine.Random.Range(0.9f, 1.1f);
        return finalDmg * table._dmg * 0.01f;
    }
    public float GetHpRatio()
    {
        return _curHp / _shipData.GetFinalState(CombatStateType.Hp);
    }

    Action _onDead;
}
