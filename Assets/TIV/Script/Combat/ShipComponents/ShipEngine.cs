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

    float kp = 1.0f; // 비례 게인
    float kd = 3f; // 미분 게인
    float maxForce = 10f; // 최대 적용 힘
    float maxVelocity = 50.0f; // 최대 속도

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

        // 비례 항 (현재 위치와 목표 위치 사이의 오차)
        Vector3 proportionalTerm = toTargetVec * kp;

        // 미분 항 (현재 속도)
        Vector3 velocity = _rigidbody.velocity;
        Vector3 derivativeTerm = velocity * kd;

        // 제어 힘 계산
        Vector3 force = proportionalTerm - derivativeTerm;

        // 힘의 크기를 최대 힘으로 클램프
        force = Vector3.ClampMagnitude(force, maxForce);

        // 힘 적용
        _rigidbody.AddForce(force, ForceMode.Acceleration);

        // 속도 제한
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    }
}
