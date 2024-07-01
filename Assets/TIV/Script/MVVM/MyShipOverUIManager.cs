using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class MyShipOverUIManager : MonoBehaviour
{
    [Header("UI 프리팹")]
    [SerializeField] GameObject Prefab_Icon_MyShipOverUIPrf;

    ShipMaster _selectShip;
    UsingShipOverUIManagerViewModel _vm;    
    private void Awake()
    {
        if (_vm == null)
        {
            _vm = new UsingShipOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();
            _vm.Register_OnSelectShipCallBack();
            _vm.OnRefreshViewModel_ShipStateData(null);
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
    }
}
