using EnumTypes;
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
    public void Init(Vector3 initPos, Vector3 targetPos, float velocity, float safeDistance, float dmg, WeaponProjectileType type)
    {
        _Collider = GetComponent<SphereCollider>();
        _Collider.enabled = false;

        this.transform.position = initPos;
        this.transform.LookAt(targetPos);

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = this.transform.forward * velocity;

        _cumulativeDistance = 0;
        _safeDistance = safeDistance;

        _dmg = dmg;
        _type = type;

        _isInit = true;

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
        if (collision.gameObject.TryGetComponent<ITargetable>(out ITargetable target))
        {
            target.Hit(_dmg, _type);

            childEffects.SetParent(null);
            Vector3 hitPos = collision.contacts[0].point;
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
