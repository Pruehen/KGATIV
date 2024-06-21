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

        public static void RefreshViewModel(this UsingShipOverUIManagerViewModel vm)//��û �ͽ��ټ�
        {            
            kjh.GameLogicManager.Instance.RefreshActiveShip(vm.OnRefreshViewModel);//�ݹ� ȣ��
        }
        //public static void CommandAddShip(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster)//��û �ͽ��ټ�
        //{
        //    kjh.GameLogicManager.Instance.AddActiveShip(shipMaster);//�ݹ� ȣ��
        //}
        //public static void CommandRemoveShip(this UsingShipOverUIManagerViewModel vm, ShipMaster shipMaster)//��û �ͽ��ټ�
        //{
        //    kjh.GameLogicManager.Instance.RemoveActiveShip(shipMaster);//�ݹ� ȣ��
        //}

        public static void OnRefreshViewModel(this UsingShipOverUIManagerViewModel vm, Dictionary<int, ShipMaster> dic)//�ݹ�
        {
            vm.ActiveShipDic = dic;
        }
    }
}
