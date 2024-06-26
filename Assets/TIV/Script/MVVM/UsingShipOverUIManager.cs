using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class UsingShipOverUIManager : MonoBehaviour
{
    [SerializeField] GameObject Prefab_Icon_ShipOverUIPrf;

    //List<UsingShipOverUI> usingShipOverUIList = new List<UsingShipOverUI>();    

    UsingShipOverUIManagerViewModel _vm;
    private void Awake()
    {
        if (_vm == null)
        {
            _vm = new UsingShipOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();
        }
    }
    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new UsingShipOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();                      
        }
    }
    private void OnDisable()
    {
        if (_vm != null)
        {            
            _vm.UnRegister_shipListChangeCallBack();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.ChangedShipMaster):
                if (_vm.IsAdded)//새로 생성된 경우
                {
                    UsingShipOverUI useUI = ObjectPoolManager.Instance.DequeueObject(Prefab_Icon_ShipOverUIPrf).GetComponent<UsingShipOverUI>();
                    useUI.transform.SetParent(this.transform);
                    useUI.SetViewTargetObject(_vm.ChangedShipMaster);
                }
                else//기존게 지워진 경우
                {

                }    
                break;
        }
    }
}
