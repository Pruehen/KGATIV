using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipEngine : MonoBehaviour
{
    Vector3 targetPos;
    Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out _rigidbody);
    }

    private void FixedUpdate()
    {
        if (_rigidbody != null)
        {
            Move();
        }
    }

    public void SetMoveTargetPos(Vector3 pos)
    {
        targetPos = pos;
    }

    void Move()
    {
        Vector3 toTargetVec = targetPos - this.transform.position;
        _rigidbody.velocity = Vector3.ClampMagnitude(toTargetVec, 50);
    }
}
