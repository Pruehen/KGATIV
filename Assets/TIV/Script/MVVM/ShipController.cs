using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class ShipController : MonoBehaviour
{
    [SerializeField] FleetMenuUIManager _fleetMenuUIManager;

    [Header("UI 프리팹")]
    [SerializeField] GameObject Prefab_Icon_MyShipOverUIPrf;

    [Header("더미 함선")]
    [SerializeField] List<GameObject> GameObject_DummyShipList;

    ShipDummy _selectDummy;
    ShipMaster _selectShip;
    ShipControllerViewModel _vm;    
    LineRenderer _lineRenderer;
    Material _lineMaterial;
    Vector2 _lineOffset = Vector2.zero;
    Vector2 _lineScale = Vector2.one;
    int _selectKey;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineMaterial = _lineRenderer.material;
        if (_vm == null)
        {
            _vm = new ShipControllerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();                        
        }

        foreach (var item in GameObject_DummyShipList)
        {
            item.SetActive(false);
        }
    }
    

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.ChangedShipMaster):
                if (_vm.ChangedShipMaster.IFF(true) == true)//아군 함선인 경우
                {
                    MyShipOverUI useUI = ObjectPoolManager.Instance.DequeueObject(Prefab_Icon_MyShipOverUIPrf).GetComponent<MyShipOverUI>();
                    useUI.transform.SetParent(this.transform);
                    useUI.Init(_vm.ChangedShipMaster, this);
                }
                break;
        }
    }

    public void SelectTargetObject_OnBtnPointerDown(ShipMaster selectShip)
    {
        DeleteDummy();
        _selectShip = selectShip;

        int key = selectShip.CombatData.GetShipTableKey();
        CreateDummy(key);

        DragAndDropManager.Instance.Register_OnDrag(DrawLine_OnDrag);
        DragAndDropManager.Instance.Register_OnDrag(SetDummyPos);
    }
    void CreateDummy(int key)
    {
        _selectDummy = GameObject_DummyShipList[key].GetComponent<ShipDummy>();
        _selectDummy.gameObject.SetActive(true);
        _selectKey = key;
    }
    void CreateDummy(int key, Vector3 initPos)
    {
        _selectDummy = GameObject_DummyShipList[key].GetComponent<ShipDummy>();
        _selectDummy.gameObject.SetActive(true);
        _selectDummy.transform.position = initPos;
        _selectKey = key;
    }
    void DeleteDummy()
    {
        if (_selectDummy != null)
        {
            _selectDummy.gameObject.SetActive(false);
            _selectDummy = null;
        }
    }

    public void MoveTargetObject_OnPointerUp(Vector3 pos, bool isDeleteZone)
    {        
        if (_selectShip != null)
        {
            SelectShipMoveOrExit_OnPointerUp(pos, isDeleteZone);
        }
    }

    void SelectShipMoveOrExit_OnPointerUp(Vector3 targetPos, bool isDeleteZone)
    {
        if (isDeleteZone == false)
        {
            _selectShip.Engine.SetMoveTargetPos(targetPos);            
        }
        else
        {
            _selectShip.CommandExit();
        }

        _selectShip = null;
        _selectDummy.gameObject.SetActive(false);
        _selectDummy = null;

        DrawLine(Vector3.zero, Vector3.zero);
        DragAndDropManager.Instance.UnRegister_OnDrag(DrawLine_OnDrag);
        DragAndDropManager.Instance.UnRegister_OnDrag(SetDummyPos);
    }

    //======================================================
    public void SelectTargetObject_OnEventTriggerPointerDown(int shipKey)
    {
        if (_selectDummy != null)
        {
            _selectDummy.gameObject.SetActive(false);
        }

        CreateDummy(shipKey, new Vector3(0, 0, -250));
        DragAndDropManager.Instance.Register_OnDrag(SetDummyPos);
        DragAndDropManager.Instance.Register_OnPointerUp(ShipCreateOrCancel_OnPointerUp);
    }
    void ShipCreateOrCancel_OnPointerUp(Vector3 targetPos, bool isDeleteZone)
    {
        Debug.Log("ShipCreateOrCancel_OnPointerUp");
        if (isDeleteZone == false)
        {            
            PlayerSpawner.Instance.NewShipSpawn(_selectKey, targetPos);                       
        }

        _fleetMenuUIManager.ExitCreateMode_OnPointerUp();
        DeleteDummy();
        DragAndDropManager.Instance.UnRegister_OnDrag(SetDummyPos);
        DragAndDropManager.Instance.UnRegister_OnPointerUp(ShipCreateOrCancel_OnPointerUp);
    }
    //======================================================

    void DrawLine_OnDrag(Vector3 pos2, bool isDeleteZone)
    {
        Vector3 pos1 = _selectShip.transform.position;
        DrawLine(pos1, pos2);
    }

    void DrawLine(Vector3 pos1, Vector3 pos2)
    {
        _lineRenderer.SetPosition(0, pos1);
        _lineRenderer.SetPosition(1, pos2);

        float lineLength = Vector3.Distance(pos1, pos2);

        if (lineLength > 0)
        {
            _lineOffset -= new Vector2(Time.deltaTime * 4, 0);
            if (_lineOffset.x < -1)
            {
                _lineOffset = Vector2.zero;
            }
            _lineScale = new Vector2(lineLength * 0.03f, 1);
            _lineMaterial.SetTextureOffset("_BaseMap", _lineOffset);
            _lineMaterial.SetTextureScale("_BaseMap", _lineScale);
        }
    }

    void SetDummyPos(Vector3 pos, bool isDeleteZone)
    {
        _selectDummy.transform.position = pos;
        _selectDummy.SetMatColor(isDeleteZone);
    }
}
