using EnumTypes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipCombatData : MonoBehaviour
{
    public ShipData ShipData { get; private set; }//참조용. 직접 변경 금지
    public ShipBuffManager BuffManager { get; private set; }//직접 가지고 있음

    [Header("플레이어 함선일 경우, 해당 함선의 키")]
    [SerializeField] int shipTableKey = -1;
    public int GetShipTableKey() { return shipTableKey; }

    [Header("적 함선일 경우, 체력, 공격력, 방어력")]
    [SerializeField] float hp;
    [SerializeField] float atk;
    [SerializeField] float def;

    public float CurHp { get; private set; }
    public void Init()
    {
        if (shipTableKey >= 0)
        {
            this.ShipData = kjh.GameLogicManager.GetShipData(shipTableKey);
        }
        else
        {
            this.ShipData = new ShipData(hp, atk, def, JsonDataManager.DataLode_UserData().GetValue_StageState());            
        }
        CurHp = ShipData.GetFinalState(CombatStateType.Hp);

        this.BuffManager = GetComponent<ShipBuffManager>();
        this.BuffManager.Init(ShipData);
    }
    public void Register_OnDead(Action callBack)
    {
        _onDead += callBack;
    }
    public void Register_OnAllStaticDataUpdate(Action callBack)
    {
        ShipData.OnAllStaticDataUpdate += callBack;
    }
    public void UnRegister_OnAllStaticDataUpdate(Action callBack)
    {
        ShipData.OnAllStaticDataUpdate -= callBack;
    }

    public void Hit(float originDmg, WeaponProjectileType type, bool isCrit, List<string> hasDebuffKey)
    {
        BuffManager.BuffCheck_G4Set_OnHit_TryAddDebuff(type, hasDebuffKey);
        float validDef = ShipData.GetFinalState(CombatStateType.Def, BuffManager.GetFinalState(CombatStateType.Def)) * BuffManager.BuffCheck_G4Set_OnHit_ValidDefRatio();

        float calcedDmg = originDmg * ((700 / (1000 + validDef) + 0.3f));
        CurHp -= calcedDmg;
        kjh.GameLogicManager.Instance.OnDameged(calcedDmg, type, this.transform.position, isCrit);
        kjh.GameLogicManager.Instance.UpdateSelectShipData();

        if(CurHp <= 0)
        {
            _onDead?.Invoke();
        }
    }
    public float GetDmg(WeaponSkillTable table, out bool isCrit)
    {
        float atk = ShipData.GetFinalState(CombatStateType.Atk, BuffManager.GetFinalState(CombatStateType.Atk));
        float dmgBonus;
        switch (table._weaponProjectileType)
        {
            case WeaponProjectileType.Physics:
                dmgBonus = ShipData.GetFinalState(CombatStateType.PhysicsDmg, BuffManager.GetFinalState(CombatStateType.PhysicsDmg));
                break;
            case WeaponProjectileType.Optics:
                dmgBonus = ShipData.GetFinalState(CombatStateType.OpticsDmg, BuffManager.GetFinalState(CombatStateType.OpticsDmg));
                break;
            case WeaponProjectileType.Particle:
                dmgBonus = ShipData.GetFinalState(CombatStateType.ParticleDmg, BuffManager.GetFinalState(CombatStateType.ParticleDmg));
                break;
            case WeaponProjectileType.Plasma:
                dmgBonus = ShipData.GetFinalState(CombatStateType.PlasmaDmg, BuffManager.GetFinalState(CombatStateType.PlasmaDmg));
                break;
            default:
                dmgBonus = 0;
                break;
        }
        dmgBonus = 1 + (dmgBonus * 0.01f);

        float critRate = ShipData.GetFinalState(CombatStateType.CritRate, BuffManager.GetFinalState(CombatStateType.CritRate));
        float critDmg = ShipData.GetFinalState(CombatStateType.CritDmg, BuffManager.GetFinalState(CombatStateType.CritDmg));
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

        float finalDmg = atk * dmgBonus * critDmgBonus;
        return finalDmg * table._dmg * 0.01f;
    }
    public float GetHpRatio()
    {
        if (ShipData == null)
            return 1;

        return CurHp / ShipData.GetFinalState(CombatStateType.Hp, BuffManager.GetFinalState(CombatStateType.Hp));
    }

    Action _onDead;
}
