using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipMainComputer : MonoBehaviour
{
    [SerializeField] bool _AiMove;
    [SerializeField] float _range;

    ShipMaster _Master;
    ShipEngine _Engine;
    ShipFCS _FCS;    

    bool _isInit = false;
    bool _isInterceptMode = false;

    Vector3 _mainTargetPos;    

    public void Init()
    {
        TryGetComponent(out _Master);
        TryGetComponent(out _Engine);
        TryGetComponent(out _FCS);        

        StartCoroutine(SearchTarget());        
        _isInit = true;

        if (_AiMove) { StartCoroutine(CommandMove()); }
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
                if (target.IFF(thisID) == false && target.IsActive())
                {
                    priorityQueue_MainTarget.TryAdd(distance, target);
                }
            }
            if (priorityQueue_MainTarget.Count > 0)
            {
                _mainTargetPos = priorityQueue_MainTarget.First().Value.GetPosition();
            }
            else
            {
                _mainTargetPos = Vector3.zero;
            }
            _FCS.SetMainTarget(priorityQueue_MainTarget);

            if (true)//요격 모드. 가장 가까운 투사체를 메인 타겟으로 설정함.
            {
                foreach (ITargetable target in targets)
                {
                    if (target.GetVelocity().sqrMagnitude < 10000) continue;

                    Vector3 toTarget = target.GetPosition() - originPos;
                    float distance = toTarget.sqrMagnitude;

                    // 타겟이 자신에게 가까워지고 있는지 확인
                    float approachingSpeed = Vector3.Dot(target.GetVelocity(), toTarget.normalized);

                    if (approachingSpeed < 0 && target.IFF(thisID) == false && target.IsActive())
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
                if (target.IFF(thisID) == false && target.IsActive())
                {
                    targetList.Add(target);
                }
            }
        }

        return targetList;
    }


    IEnumerator CommandMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector3 targetPos = _mainTargetPos + Vector3.ClampMagnitude(this.transform.position - _mainTargetPos, _range);
            targetPos.x = this.transform.position.x;
            targetPos.y = this.transform.position.y;
            _Engine.SetMoveTargetPos(targetPos);
        }
    }
}
