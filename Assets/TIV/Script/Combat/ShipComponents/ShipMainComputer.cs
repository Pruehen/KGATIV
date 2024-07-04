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

            foreach (ITargetable target in targets)//���� ���. ���� ����� �� �Լ��� ���� Ÿ������ ������. ����ü�� Ÿ�������� ����.
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

            if (true)//��� ���. ���� ����� ����ü�� ���� Ÿ������ ������.
            {
                foreach (ITargetable target in targets)
                {
                    if (target.GetVelocity().sqrMagnitude < 10000) continue;

                    Vector3 toTarget = target.GetPosition() - originPos;
                    float distance = toTarget.sqrMagnitude;

                    // Ÿ���� �ڽſ��� ��������� �ִ��� Ȯ��
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

        // ���� ��ġ�� �������� searchRadius �ݰ� ���� ��� �ݶ��̴� �˻�
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3000);
        bool thisID = _Master.GetID();

        foreach (Collider collider in colliders)
        {
            // �� �ݶ��̴��� ���� ������Ʈ���� ITargetable �������̽� ���� ���� Ȯ��
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
