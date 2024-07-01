using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMainComputer : MonoBehaviour
{
    ShipMaster _Master;
    ShipEngine _Engine;
    ShipFCS _FCS;    

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
    
    IEnumerator SearchTarget()
    {
        SortedDictionary<float, ITargetable> priorityQueue_MainTarget = new SortedDictionary<float, ITargetable>();
        SortedDictionary<float, ITargetable> priorityQueue_InterceptTarget = new SortedDictionary<float, ITargetable>();

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            List<ITargetable> targets = SearchForTargets();
            bool thisID = _Master.GetID();

            priorityQueue_MainTarget.Clear();
            priorityQueue_InterceptTarget.Clear();

            Vector3 originPos = this.transform.position;

            foreach (ITargetable target in targets)//공격 모드. 가장 가까운 적 함선을 메인 타겟으로 설정함. 투사체는 타게팅하지 않음.
            {
                if (target.GetVelocity().sqrMagnitude >= 10000) continue;

                float distance = Vector3.SqrMagnitude(originPos - target.GetPosition());
                if (target.IFF(thisID) == false)
                {
                    priorityQueue_MainTarget.TryAdd(distance, target);
                }
            }

            _FCS.SetMainTarget(priorityQueue_MainTarget);

            if (true)//요격 모드. 가장 가까운 투사체를 메인 타겟으로 설정함.
            {
                foreach (ITargetable target in targets)
                {
                    if (target.GetVelocity().sqrMagnitude < 10000) continue;

                    float distance = Vector3.SqrMagnitude(originPos - target.GetPosition());
                    if (target.IFF(thisID) == false)
                    {
                        priorityQueue_InterceptTarget.TryAdd(distance, target);
                    }
                }
                if (priorityQueue_InterceptTarget.Count > 0)
                {
                    _FCS.SetInterceptTarget(priorityQueue_InterceptTarget);
                }
            }
        }     
    }

    List<ITargetable> SearchForTargets()
    {
        List<ITargetable> targetList = new List<ITargetable>();

        // 현재 위치를 기준으로 searchRadius 반경 내의 모든 콜라이더 검색
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3000);
        bool thisID = _Master.GetID();

        foreach (Collider collider in colliders)
        {
            // 각 콜라이더의 게임 오브젝트에서 ITargetable 인터페이스 구현 여부 확인
            ITargetable target;
            if (collider.TryGetComponent(out target))
            {
                if (target.IFF(thisID) == false)
                {
                    targetList.Add(target);
                }
            }
        }

        return targetList;
    }
}
