using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSkillTable Table { get; private set; }
    ShipCombatData CombatData;

    ITargetable _target;    
    float _collDownValue;
    float _curMaxCool;
    public void Init(WeaponSkillTable table)
    {
        this.Table = table;
        CombatData = this.GetComponent<ShipCombatData>();        
        SetCollDownValue();
    }

    void SetCollDownValue()
    {        
        _collDownValue = CombatData.BuffManager.BuffCheck_B4Set_OnSetCollDownValue(Table._collTime * Random.Range(0.9f, 1.1f));
        _curMaxCool = _collDownValue;
    }
    public float GetCoolDownRatio()
    {
        return _collDownValue / _curMaxCool;
    }
    public void SetTarget(ITargetable target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        _collDownValue -= Time.deltaTime;

        if (_collDownValue < 0)
        {
            if (_target != null && Vector3.Distance(this.transform.position, _target.GetPosition()) < Table._maxRange)
            {
                SetCollDownValue();
                Fire();
            }
        }
    }

    void Fire()
    {
        Vector3 originPos = this.transform.position;
        Vector3 aimPos = _target.GetPosition();        
        float projectileVelocity = Table._projectileVelocity;        

        for (int i = 0; i < 4; i++)
        {
            float eta = (aimPos - originPos).magnitude / projectileVelocity;
            aimPos = _target.GetPosition() + _target.GetVelocity() * eta;
        }

        Projectile projectile = Instantiate(PrefabManager.Instance.GetProjectilePrf(Table._projectileNameKey)).GetComponent<Projectile>();
        bool isCrit;
        float dmg = CombatData.GetDmg(Table, out isCrit);

        List<string> hasDebuffkey = new List<string>();

        if(CombatData.BuffManager.BuffCheck_G4Set_OnFire(out string debuffKey))
        {
            hasDebuffkey.Add(debuffKey);
        }
        CombatData.BuffManager.BuffCheck_D4Set_OnFire();
        projectile.Init(originPos, aimPos, Table, dmg, isCrit, hasDebuffkey);
    }
}
