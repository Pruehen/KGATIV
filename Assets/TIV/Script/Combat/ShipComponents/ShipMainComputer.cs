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

            foreach (ITargetable target in targets)//���� ���. ���� ����� �� �Լ��� ���� Ÿ������ ������. ����ü�� Ÿ�������� ����.
            {
                if (target.GetVelocity().sqrMagnitude >= 10000) continue;

                float distance = Vector3.SqrMagnitude(originPos - target.GetPosition());
                if (target.IFF(thisID) == false)
                {
                    priorityQueue_MainTarget.TryAdd(distance, target);
                }
            }

            _FCS.SetMainTarget(priorityQueue_MainTarget);

            if (true)//��� ���. ���� ����� ����ü�� ���� Ÿ������ ������.
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

        // ���� ��ġ�� �������� searchRadius �ݰ� ���� ��� �ݶ��̴� �˻�
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3000);
        bool thisID = _Master.GetID();

        foreach (Collider collider in colliders)
        {
            // �� �ݶ��̴��� ���� ������Ʈ���� ITargetable �������̽� ���� ���� Ȯ��
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
