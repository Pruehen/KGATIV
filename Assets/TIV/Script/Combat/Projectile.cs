using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform childEffects;
    [SerializeField] ParticleSystem hitEffect;
    SphereCollider _Collider;
    Rigidbody _rigidbody;
    float _cumulativeDistance = 0f;
    float _safeDistance;
    float _dmg;
    WeaponProjectileType _type;
    bool _isInit;
    float _halfDistance;
    Vector3 _initPos;
    bool _isCrit;
    List<string> _hasDebuffKey;

    public void Init(Vector3 initPos, Vector3 targetPos, WeaponSkillTable table, float dmg, bool isCrit, List<string> hasDebuffKey)
    {
        _Collider = GetComponent<SphereCollider>();
        _Collider.enabled = false;

        this.transform.position = initPos;
        this.transform.LookAt(targetPos);

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = this.transform.forward * table._projectileVelocity;

        _cumulativeDistance = 0;
        _safeDistance = Vector3.Distance(initPos, targetPos) * 0.9f;

        _dmg = dmg;
        _type = table._weaponProjectileType;

        _isInit = true;
        _halfDistance = table._halfDistance;
        _initPos = this.transform.position;
        _isCrit = isCrit;

        if (hasDebuffKey.Count > 0)
        {
            _hasDebuffKey = new List<string>();
            foreach (string s in hasDebuffKey)
            {
                _hasDebuffKey.Add(s);
            }
        }

        Destroy(this.gameObject, 10);
    }

    private void FixedUpdate()
    {
        if (_isInit)
        {
            _cumulativeDistance += (_rigidbody.velocity * Time.fixedDeltaTime).magnitude;
            if (_safeDistance <= _cumulativeDistance)
            {
                _Collider.enabled = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ITargetable target))
        {
            Vector3 hitPos = collision.contacts[0].point;
            if (_halfDistance > 0)
            {
                _dmg *= 1 / Mathf.Exp((-Mathf.Log(0.5f) / _halfDistance) * Vector3.Distance(hitPos, _initPos));
            }
            target.Hit(_dmg, _type, _isCrit, _hasDebuffKey);

            childEffects.SetParent(null);            
            childEffects.position = hitPos;
            if (hitEffect != null)
            {
                hitEffect.Play();
            }
            Destroy(childEffects.gameObject, 4);
            Destroy(this.gameObject);
        }
    }
}
