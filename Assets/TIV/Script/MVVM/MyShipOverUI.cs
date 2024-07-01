using UnityEngine;
using UnityEngine.EventSystems;
using UI.Extension;

//[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class MyShipOverUI : MonoBehaviour
{            
    ShipMaster _targetObject;
    RectTransform _rectTransform;
    MyShipOverUIManager _myShipOverUIManager;

    private void Awake()
    {                
        _rectTransform = GetComponent<RectTransform>();        
    }

    private void Update()
    {
        if(_targetObject != null)
        {
            SetPosition_OnUpdate();
        }
        else
        {
            ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
        }
    }

    public void Init(ShipMaster target, MyShipOverUIManager manager)
    {
        _targetObject = target;
        _myShipOverUIManager = manager;
    }

    void SetPosition_OnUpdate()
    {
        _rectTransform.SetUIPos_WorldToScreenPos(_targetObject.transform.position);
    }

    public void SelectTargetObject_OnPointerDown()
    {
        _myShipOverUIManager.SelectTargetObject_OnPointerDown(_targetObject);
    }
    public void MoveTargetObject_OnPointerUp()
    {
        _myShipOverUIManager.MoveTargetObject_OnPointerUp();
    }
}
