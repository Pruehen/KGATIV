using UnityEngine;

public class Weapon : MonoBehaviour
{
    WeaponSkillTable table;
    ShipCombatData CombatData;

    ITargetable _target;    
    float _collDownValue;    
    public void Init(WeaponSkillTable table)
    {
        this.table = table;
        CombatData = this.GetComponent<ShipCombatData>();        
        SetCollDownValue();
    }

    void SetCollDownValue()
    {
        _collDownValue = table._collTime * Random.Range(0.95f, 1.05f);
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
            if (_target != null && Vector3.Distance(this.transform.position, _target.GetPosition()) < table._maxRange)
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
        float projectileVelocity = table._projectileVelocity;        

        for (int i = 0; i < 4; i++)
        {
            float eta = (aimPos - originPos).magnitude / projectileVelocity;
            aimPos = _target.GetPosition() + _target.GetVelocity() * eta;
        }

        Projectile projectile = Instantiate(PrefabManager.Instance.GetProjectilePrf(table._projectileNameKey)).GetComponent<Projectile>();
        bool isCrit;
        float dmg = CombatData.GetDmg(table, out isCrit);
        projectile.Init(originPos, aimPos, table, dmg, isCrit);
    }
}
