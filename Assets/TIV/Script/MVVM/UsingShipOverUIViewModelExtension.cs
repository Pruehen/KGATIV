using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

namespace ViewModel.Extensions
{
    public static class UsingShipOverUIViewModelExtension
    {
        public static void RefreshViewModel(this UsingShipOverUIViewModel vm)//요청 익스텐션
        {            
            //kjh.GameLogicManager.Instance.RefreshActiveShip(vm.OnAllRefreshViewModel);//콜백 호출
        }

        //public static void OnAllRefreshViewModel(this UsingShipOverUIViewModel vm, Dictionary<int, ShipMaster> dic)//콜백
        //{
        //    vm.ActiveShipDic = dic;
        //}
    }
}
