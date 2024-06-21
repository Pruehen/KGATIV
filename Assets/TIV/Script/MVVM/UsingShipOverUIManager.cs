using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class UsingShipOverUIManager : MonoBehaviour
{
    [SerializeField] GameObject Prefab_Icon_ShipOverUIPrf;

    List<UsingShipOverUI> usingShipOverUIList = new List<UsingShipOverUI>();
    int _iconUsingCount = 0;

    UsingShipOverUIManagerViewModel _vm;
    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new UsingShipOverUIManagerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.Register_shipListChangeCallBack();
            _vm.RefreshViewModel();
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
            case nameof(_vm.ActiveShipDic):
                _iconUsingCount = 0;
                foreach (var item in _vm.ActiveShipDic)
                {
                    GetUIIcon().SetTargetObject(item.Value);
                }
                break;
        }
    }

    UsingShipOverUI GetUIIcon()
    {
        if(usingShipOverUIList.Count == _iconUsingCount)
        {
            UsingShipOverUI ui = Instantiate(Prefab_Icon_ShipOverUIPrf, this.transform).GetComponent<UsingShipOverUI>();
            usingShipOverUIList.Add(ui);            
        }
        return usingShipOverUIList[_iconUsingCount++];
    }
}
