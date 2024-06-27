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
            foreach (ITargetable target in targets)//���� ���. ���� ����� �� �Լ��� ���� Ÿ������ ������. ����ü�� Ÿ�������� ����.
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

            if(true)//��� ���. ���� ����� ����ü�� ���� Ÿ������ ������.
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

        // ���� ��ġ�� �������� searchRadius �ݰ� ���� ��� �ݶ��̴� �˻�
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3000);

        foreach (Collider collider in colliders)
        {
            // �� �ݶ��̴��� ���� ������Ʈ���� ITargetable �������̽� ���� ���� Ȯ��
            ITargetable target;
            if (collider.TryGetComponent(out target))
            {
                targetList.Add(target);
            }
        }

        return targetList;
    }
}
