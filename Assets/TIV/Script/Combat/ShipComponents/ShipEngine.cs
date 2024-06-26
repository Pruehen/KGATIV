using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipEngine : MonoBehaviour
{
    Vector3 targetPos;
    Rigidbody _rigidbody;

    Vector3 _initPos;
    Vector3 _initWarpPos;

    // Start is called before the first frame update
    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initPos = transform.position;
        _initWarpPos = _initPos - (this.transform.forward * 10000);
        transform.position = _initWarpPos;
        StartCoroutine(Warp());
    }

    IEnumerator Warp()
    {
        float time = 0;
        while(true)
        {
            yield return null;
            time += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(_initWarpPos, _initPos, time);
            if (time >= 1)
                yield break;
        }
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
