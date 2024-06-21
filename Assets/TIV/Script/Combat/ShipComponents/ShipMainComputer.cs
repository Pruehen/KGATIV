using UnityEngine;

public class ShipMainComputer : MonoBehaviour
{    
    ShipEngine _Engine;
    ShipFCS _FCS;
    [SerializeField] Vector3 TargetPos;

    bool _isInit = false;
    public void Init()
    {
        TryGetComponent(out _Engine);
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

    
    void SetTarget()
    {        
        //_FCS.SetTarget(target);        
    }
}
