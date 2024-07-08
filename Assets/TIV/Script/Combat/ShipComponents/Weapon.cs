using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSkillTable Table { get; private set; }
    ShipCombatData CombatData;

    SortedDictionary<float, ITargetable> _targetList;
    ITargetable _target;
    Vector3 _LastTargetPos;
    float _collDownValue;
    float _curMaxCool;
    float _fireRoundSize;
    bool _isInit;
    public void Init(WeaponSkillTable table)
    {
        this.Table = table;
        CombatData = this.GetComponent<ShipCombatData>();
        _fireRoundSize = this.GetComponent<SphereCollider>().radius;
        SetCollDownValue();
        _targetList = new SortedDictionary<float, ITargetable>();
        _isInit = false;
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
    public void SetTarget(SortedDictionary<float, ITargetable> target)
    {
        _targetList.Clear();
        foreach (var item in target)
        {
            _targetList.Add(item.Key, item.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _collDownValue -= Time.deltaTime;

        if (_collDownValue < 0)
        {
            if (_targetList == null || _targetList.Count == 0) return;

            _target = _targetList.First().Value;
            while (_target == null && _targetList.Count > 0)
            {
                _targetList.Remove(_targetList.First().Key);
                if (_targetList.Count == 0) break;
                _target = _targetList.First().Value;
            }

            if(_target == null) return;

            if (_target.GetPosition() != Vector3.zero)
            {
                _LastTargetPos = _target.GetPosition();
            }
            if (Vector3.Distance(this.transform.position, _LastTargetPos) < Table._maxRange)
            {
                SetCollDownValue();
                Fire();
            }
        }
    }

    void Fire()
    {
        Vector3 originPos = this.transform.position + Random.onUnitSphere * _fireRoundSize;
        Vector3 aimPos = _LastTargetPos;        
        float projectileVelocity = Table._projectileVelocity;        

        for (int i = 0; i < 4; i++)
        {
            float eta = (aimPos - originPos).magnitude / projectileVelocity;
            aimPos = _LastTargetPos + _target.GetVelocity() * eta;
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
        projectile.Init(originPos, aimPos, Table, dmg, isCrit, hasDebuffkey, this.gameObject.layer);
    }
}
