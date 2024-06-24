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

        public static void RefreshViewModel(this UsingShipOverUIManagerViewModel vm)//��û �ͽ��ټ�
        {            
            kjh.GameLogicManager.Instance.RefreshActiveShip(vm.OnRefreshViewModel);//�ݹ� ȣ��
        }

        public static void OnRefreshViewModel(this UsingShipOverUIManagerViewModel vm, Dictionary<int, ShipMaster> dic)//�ݹ�
        {
            vm.ActiveShipDic = dic;
        }

        public static void Register_onDmgedCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_onDmgedCallBack(vm.OnRefreshViewModel);
        }

        public static void UnRegister_onDmgedCallBack(this UsingShipOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_onDmgedCallBack(vm.OnRefreshViewModel);
        }

        public static void OnRefreshViewModel(this UsingShipOverUIManagerViewModel vm)//�ݹ�
        {
            vm.OnPropertyChanged_ActiveShipDic();
        }
    }
}
