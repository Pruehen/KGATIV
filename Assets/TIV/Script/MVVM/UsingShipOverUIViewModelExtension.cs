using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

namespace ViewModel.Extensions
{
    public static class UsingShipOverUIViewModelExtension
    {
        public static void RefreshViewModel(this UsingShipOverUIViewModel vm)//��û �ͽ��ټ�
        {            
            //kjh.GameLogicManager.Instance.RefreshActiveShip(vm.OnAllRefreshViewModel);//�ݹ� ȣ��
        }

        //public static void OnAllRefreshViewModel(this UsingShipOverUIViewModel vm, Dictionary<int, ShipMaster> dic)//�ݹ�
        //{
        //    vm.ActiveShipDic = dic;
        //}
    }
}
