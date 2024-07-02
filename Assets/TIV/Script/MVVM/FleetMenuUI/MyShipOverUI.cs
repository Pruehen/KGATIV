using UnityEngine;
using UnityEngine.EventSystems;
using UI.Extension;

//[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class MyShipOverUI : MonoBehaviour
{            
    ShipMaster _uiTargetShipMaster;
    RectTransform _rectTransform;
    ShipController _myShipOverUIManager;

    private void Awake()
    {                
        _rectTransform = GetComponent<RectTransform>();        
    }

    private void Update()
    {
        if(_uiTargetShipMaster != null)
        {
            SetUIPosition_OnUpdate();
        }
        else
        {
            DragAndDropManager.Instance.UnRegister_OnPointerUp(MoveTargetObject_OnPointerUp);
            ObjectPoolManager.Instance.EnqueueObject(this.gameObject);            
        }
    }

    public void Init(ShipMaster target, ShipController manager)
    {
        _uiTargetShipMaster = target;
        _myShipOverUIManager = manager;
    }

    void SetUIPosition_OnUpdate()
    {
        _rectTransform.SetUIPos_WorldToScreenPos(_uiTargetShipMaster.transform.position);
    }

    /// <summary>
    /// 버튼 자체의 이벤트 트리거로 작동함.
    /// </summary>
    public void SelectTargetObject_OnBtnPointerDown()
    {
        _myShipOverUIManager.SelectTargetObject_OnBtnPointerDown(_uiTargetShipMaster);
        DragAndDropManager.Instance.Register_OnPointerUp(MoveTargetObject_OnPointerUp);
    }
    void MoveTargetObject_OnPointerUp(Vector3 pos, bool isDeleteZone)
    {
        _myShipOverUIManager.MoveTargetObject_OnPointerUp(pos, isDeleteZone);
        DragAndDropManager.Instance.UnRegister_OnPointerUp(MoveTargetObject_OnPointerUp);
    }
}
