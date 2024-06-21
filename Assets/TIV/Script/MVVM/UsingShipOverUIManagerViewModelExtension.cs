using System.Collections.Generic;

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

        public static void RefreshViewModel(this UsingShipOverUIManagerViewModel vm)//요청 익스텐션
        {            
            kjh.GameLogicManager.Instance.RefreshActiveShip(vm.OnRefreshViewModel);//콜백 호출
        }
        //public static void CommandAddShip(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster)//요청 익스텐션
        //{
        //    kjh.GameLogicManager.Instance.AddActiveShip(shipMaster);//콜백 호출
        //}
        //public static void CommandRemoveShip(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster)//요청 익스텐션
        //{
        //    kjh.GameLogicManager.Instance.RemoveActiveShip(shipMaster);//콜백 호출
        //}

        public static void OnRefreshViewModel(this UsingShipOverUIManagerViewModel vm, Dictionary<int, ShipMaster> dic)//콜백
        {
            vm.ActiveShipDic = dic;
        }
    }
}
