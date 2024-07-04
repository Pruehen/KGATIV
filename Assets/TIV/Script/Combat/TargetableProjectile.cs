using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

public class TargetableProjectile : Projectile, ITargetable
{
    float _hp;
    public override void Init(Vector3 initPos, Vector3 targetPos, WeaponSkillTable table, float dmg, bool isCrit, List<string> hasDebuffKey, int projectileLayer)
    {
        base.Init(initPos, targetPos, table, dmg, isCrit, hasDebuffKey, projectileLayer);
        _hp = dmg;
    }
    public Vector3 GetPosition()
    {
        if (this == null || transform == null)
        {
            return Vector3.zero;
        }
        return this.transform.position;
    }
    public Vector3 GetVelocity()
    {
        if (this == null || transform == null)
        {
            return Vector3.zero;
        }
        return _rigidbody.velocity;
    }

    public bool IFF(bool id)
    {
        bool thisId = GetID();
        return thisId == id;
    }

    public bool GetID()
    {
        if(_rigidbody.velocity.z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Hit(float dmg, WeaponProjectileType type, bool isCrit, List<string> hasDebuffKey)
    {
        _hp -= dmg;        

        if (_hp <= 0)
        {
            base.Destroy(this.transform.position);
        }
    }

    public bool IsActive() { return true; }
}
