using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class MyShipOverUIManager : MonoBehaviour
{
    [Header("UI 프리팹")]
    [SerializeField] GameObject Prefab_Icon_MyShipOverUIPrf;

    ShipMaster _selectShip;
    UsingShipOverUIManagerViewModel _vm;    
    LineRenderer _lineRenderer;
    Material _lineMaterial;
    Vector2 _lineOffset = Vector2.zero;
    Vector2 _lineScale = Vector2.one;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineMaterial = _lineRenderer.material;
        if (_vm == null)
        {
            _vm = new UsingShipOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();                        
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
        _selectShip = selectShip;
    }
    public void MoveTargetObject_OnPointerUp()
    {
        Vector3 targetPos = UIManager.Instance.FleetMenuUIManager.RayCast_ScreenPointToRay();        
        _selectShip.Engine.SetMoveTargetPos(targetPos);
        _selectShip = null;
    }

    private void Update()
    {
        if(_selectShip != null )
        {
            DrawLine_OnUpdate(_selectShip.transform.position, UIManager.Instance.FleetMenuUIManager.RayCast_ScreenPointToRay());
        }
        else
        {
            DrawLine_OnUpdate(Vector3.zero, Vector3.zero);
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
}
