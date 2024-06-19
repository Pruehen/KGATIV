using EnumTypes;
using System.Collections.Generic;

namespace ViewModel.Extensions
{
    public static class EquipInfoUIManagerViewModelExtension
    {
        public static void RefreshVielModel(this EquipInfoUIManagerViewModel vm, string equipUniqeKey)//요청 익스텐션
        {            
            kjh.GameLogicManager.Instance.RefreshEquipInfo(equipUniqeKey, vm.OnRefreshViewModel);//콜백 호출
        }

        public static void OnRefreshViewModel(this EquipInfoUIManagerViewModel vm, UserHaveEquipData equipData)//콜백
        {
            vm.TableKey = equipData._equipTableKey;
            vm.Name = equipData.GetName();
            vm.Type = equipData.GetEquipType();
            vm.MainStateType = equipData._mainState._stateType;
            vm.Level = equipData._level;
            vm.SubStateList = equipData._subStateList;
            vm.SetType = equipData._setType;
        }


        //public static void RegisterEventsOnEnable(this ShipMenuUIViewModel vm)
        //{
        //    ShipMenuUIManager.Instance.RegisterLevelUpCallback(vm.OnResponseLevelUp);
        //}
        //public static void UnRegisterEventsOnDisable(this ShipMenuUIViewModel vm)
        //{
        //    GameLogicManager.Inst.UnRegisterLevelUpCallback(vm.OnResponseLevelUp);
        //}
        //public static void OnResponseLevelUp(this ShipMenuUIViewModel vm, int userId, int level)
        //{
        //    if (vm.UserId != userId) return;

        //    vm.Level = level;
        //}
    }
}
