using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

namespace ViewModel.Extensions
{
    public static class UsingShipOverUIManagerViewModelExtension
    {
        public static void Register_shipListChangeCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_shipListChangeCallBack(vm.OnRefreshViewModel);
        }

        public static void UnRegister_shipListChangeCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_shipListChangeCallBack(vm.OnRefreshViewModel);
        }

        public static void OnRefreshViewModel(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster, bool isAdd)//ฤÝน้
        {
            vm.IsAdded = isAdd;
            vm.ChangedShipMaster = shipMaster;            
        }
    }
}
