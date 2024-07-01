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

    public void SelectTargetObject_OnPointerDown(ShipMaster selectShip)
    {
        if(_selectDummy != null)
        {
            _selectDummy.gameObject.SetActive(false);
        }

        _selectShip = selectShip;
        
        int key = selectShip.CombatData.GetShipTableKey();
        _selectDummy = GameObject_DummyShipList[key].GetComponent<ShipDummy>();
        _selectDummy.gameObject.SetActive(true);

        _selectKey = key;
    }
    public void SelectTargetObject_OnClick(int shipKey)
    {
        if (_selectDummy != null)
        {
            _selectDummy.gameObject.SetActive(false);
        }

        _selectDummy = GameObject_DummyShipList[shipKey].GetComponent<ShipDummy>();
        _selectDummy.gameObject.SetActive(true);
        _selectDummy.transform.position = new Vector3(0, 0, -250);
        _selectKey = shipKey;
    }

    public void MoveTargetObject_OnPointerUp()
    {
        bool isDeleteZone;

        Vector3 targetPos = UIManager.Instance.FleetMenuUIManager.RayCast_ScreenPointToRay(out isDeleteZone);

        if (_selectShip != null)
        {
            SelectShipMoveOrExit(targetPos, isDeleteZone);
        }
    }

    void SelectShipMoveOrExit(Vector3 targetPos, bool isDeleteZone)
    {
        if (isDeleteZone == false)
        {
            _selectShip.Engine.SetMoveTargetPos(targetPos);
            UserData.Instance.SetShipPosData(_selectShip.ShipIndex, targetPos);
        }
        else
        {
            _selectShip.CommandExit();
        }

        _selectShip = null;

        _selectDummy.gameObject.SetActive(false);
        _selectDummy = null;
    }
    void ShipCreateOrCancel(Vector3 targetPos, bool isDeleteZone)
    {
        if (isDeleteZone == false)
        {
            PlayerSpawner.Instance.NewShipSpawn(_selectKey, targetPos);
        }
        else
        {
            _fleetMenuUIManager.ExitCreateMode();            
        }        

        _selectDummy.gameObject.SetActive(false);
        _selectDummy = null;
    }

    private void Update()
    {
        Vector3 rayPos = UIManager.Instance.FleetMenuUIManager.RayCast_ScreenPointToRay(out bool isDeleteZone);
        if (_selectShip != null )
        {            
            DrawLine_OnUpdate(_selectShip.transform.position, rayPos);
        }        
        else
        {
            DrawLine_OnUpdate(Vector3.zero, Vector3.zero);
        }

        if(_selectDummy != null)
        {            
            SetDummyPos(rayPos, isDeleteZone);
            if(Input.GetMouseButtonUp(0))
            {
                ShipCreateOrCancel(rayPos, isDeleteZone);
            }
        }
    }

    void DrawLine_OnUpdate(Vector3 pos1, Vector3 pos2)
    {        
        _lineRenderer.SetPosition(0, pos1);
        _lineRenderer.SetPosition(1, pos2);
        
        float lineLength = Vector3.Distance(pos1, pos2);

        if(lineLength > 0)
        {
            _lineOffset -= new Vector2(Time.deltaTime * 4, 0);
            if(_lineOffset.x < -1)
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
