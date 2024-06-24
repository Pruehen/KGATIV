using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

namespace ViewModel.Extensions
{
    public static class DmgViewOverUIManagerViewModelExtension
    {
        public static void Register_onDmgedCallBack(this DmgViewOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_onDmgedCallBack(vm.OnRefreshViewModel);
        }

        public static void UnRegister_onDmgedCallBack(this DmgViewOverUIManagerViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_onDmgedCallBack(vm.OnRefreshViewModel);
        }

        public static void OnRefreshViewModel(this DmgViewOverUIManagerViewModel vm, int value, WeaponProjectileType type, Vector3 position, bool isCrit)//ฤÝน้
        {
            vm.Type = type;
            vm.IsCrit = isCrit;     
            vm.Position = position;
            vm.NewDmg = value;
        }
    }
}
