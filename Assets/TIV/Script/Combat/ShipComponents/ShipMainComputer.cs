using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMainComputer : MonoBehaviour
{
    ShipMaster _Master;
    ShipEngine _Engine;
    ShipFCS _FCS;
    [SerializeField] Vector3 TargetPos;

    bool _isInit = false;
    bool _isInterceptMode = false;

    public void Init()
    {
        TryGetComponent(out _Master);
        TryGetComponent(out _Engine);
        TryGetComponent(out _FCS);        

        StartCoroutine(SearchTarget());
        _isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInit == false) return;

        SetMoveTargetPos(TargetPos);
    }

    public void SetMoveTargetPos(Vector3 pos)
    {
        if(_Engine != null)
        {
            _Engine.SetMoveTargetPos(pos);
        }
    }
    
    IEnumerator SearchTarget()
    {        
        while(true)
        {
            yield return new WaitForSeconds(0.3f);

            List<ITargetable> targets = SearchForTargets();
            bool thisID = _Master.GetID();

            float distanceTemp = float.MaxValue;
            ITargetable targetTemp = null;
            Vector3 originPos = this.transform.position;
            foreach (ITargetable target in targets)//공격 모드. 가장 가까운 적 함선을 메인 타겟으로 설정함. 투사체는 타게팅하지 않음.
            {
                if (target.GetVelocity().sqrMagnitude >= 10000) continue;

                float distance = Vector3.Distance(originPos, target.GetPosition());
                if (distance < distanceTemp && target.IFF(thisID) == false)
                {
                    distanceTemp = distance;
                    targetTemp = target;
                }
            }
            _FCS.SetMainTarget(targetTemp);

            if(true)//요격 모드. 가장 가까운 투사체를 메인 타겟으로 설정함.
            {
                distanceTemp = float.MaxValue;
                targetTemp = null;                
                foreach (ITargetable target in targets)
                {
                    if (target.GetVelocity().sqrMagnitude < 10000) continue;

                    float distance = Vector3.Distance(originPos, target.GetPosition());
                    if (distance < distanceTemp && target.IFF(thisID) == false)
                    {
                        distanceTemp = distance;
                        targetTemp = target;
                    }
                }
                if (targetTemp != null)
                {
                    _FCS.SetInterceptTarget(targetTemp);
                }
            }
        }     
    }

    List<ITargetable> SearchForTargets()
    {
        List<ITargetable> targetList = new List<ITargetable>();

        // 현재 위치를 기준으로 searchRadius 반경 내의 모든 콜라이더 검색
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3000);

        foreach (Collider collider in colliders)
        {
            // 각 콜라이더의 게임 오브젝트에서 ITargetable 인터페이스 구현 여부 확인
            ITargetable target;
            if (collider.TryGetComponent(out target))
            {
                targetList.Add(target);
            }
        }

        return targetList;
    }
}
