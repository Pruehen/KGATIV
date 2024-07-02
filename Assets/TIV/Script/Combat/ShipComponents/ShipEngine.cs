using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipEngine : MonoBehaviour
{
    Vector3 targetPos;
    Rigidbody _rigidbody;

    Vector3 _initPos;
    Vector3 _initWarpPos;

    float kp = 1.0f; // ��� ����
    float kd = 3f; // �̺� ����
    float maxForce = 10f; // �ִ� ���� ��
    float maxVelocity = 50.0f; // �ִ� �ӵ�

    public Action<Vector3, float> onWarpStart;
    public Action onWarpEnd;

    // Start is called before the first frame update
    public void Init(float warpTime = 2)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initPos = transform.position;
        _initWarpPos = _initPos - (this.transform.forward * warpTime * 3000);
        transform.position = _initWarpPos;
        SetMoveTargetPos(_initPos);
        onWarpStart?.Invoke(_initPos, warpTime);
        StartCoroutine(Warp(warpTime));
    }

    IEnumerator Warp(float warpTime)
    {
        float time = 0;
        while(true)
        {
            yield return null;
            time += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(_initWarpPos, _initPos, time / warpTime);
            if (time >= warpTime)
            {                
                onWarpEnd?.Invoke();
                _rigidbody.velocity = Vector3.zero;
                yield break;
            }
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
    public Vector3 GetMoveTargetPos()
    {
        return targetPos;
    }


    void Move()
    {
        Vector3 currentPosition = this.transform.position;
        Vector3 toTargetVec = targetPos - currentPosition;

        // ��� �� (���� ��ġ�� ��ǥ ��ġ ������ ����)
        Vector3 proportionalTerm = toTargetVec * kp;

        // �̺� �� (���� �ӵ�)
        Vector3 velocity = _rigidbody.velocity;
        Vector3 derivativeTerm = velocity * kd;

        // ���� �� ���
        Vector3 force = proportionalTerm - derivativeTerm;

        // ���� ũ�⸦ �ִ� ������ Ŭ����
        force = Vector3.ClampMagnitude(force, maxForce);

        // �� ����
        _rigidbody.AddForce(force, ForceMode.Acceleration);

        // �ӵ� ����
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    }
}
