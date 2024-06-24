using EnumTypes;
using System;

namespace ViewModel.Extensions
{
    public static class ShipMenuUIViewModelExtension
    {
        public static void Register_OnShipDataChange(this ShipMenuUIViewModel vm)
        {
            kjh.GameLogicManager.Instance.Register_OnShipDataChange(vm.OnChangeShipData);
        }

        public static void UnRegister_OnShipDataChange(this ShipMenuUIViewModel vm)
        {
            kjh.GameLogicManager.Instance.UnRegister_OnShipDataChange(vm.OnChangeShipData);
        }

        public static void RefreshShipData(this ShipMenuUIViewModel vm, int shipKey)//요청 익스텐션
        {            
            kjh.GameLogicManager.Instance.RefreshShipInfo(shipKey, vm.OnChangeShipData);//콜백 호출
        }
        public static void CommandEquip(this ShipMenuUIViewModel vm, string equipUniqeKey, int shipKey)
        {
            kjh.GameLogicManager.Instance.ShipEquipItem(equipUniqeKey, shipKey, vm.OnChangeShipData);//콜백 호출
        }
        public static void CommandUnEquip(this ShipMenuUIViewModel vm, string equipUniqeKey)
        {
            kjh.GameLogicManager.Instance.ShipUnEquipItem(equipUniqeKey, vm.OnChangeShipData);//콜백 호출
        }
        public static void RefreshUserItemData(this ShipMenuUIViewModel vm)//요청 익스텐션
        {
            kjh.GameLogicManager.Instance.RefreshUserItem(vm.OnChangeUserItemData);//콜백 호출
        }

        public static void OnChangeShipData(this ShipMenuUIViewModel vm, ShipData shipData)//콜백
        {
            vm.Name = shipData.GetName();
            vm.Class = shipData.GetShipClass();
            vm.Star = shipData.GetShipStar();
            vm.Hp = shipData.GetFinalState(CombatStateType.Hp);
            vm.Atk = shipData.GetFinalState(CombatStateType.Atk);
            vm.Def = shipData.GetFinalState(CombatStateType.Def);
            vm.CritRate = shipData.GetFinalState(CombatStateType.CritRate);
            vm.CritDmg = shipData.GetFinalState(CombatStateType.CritDmg);
            vm.PhysicsDmg = shipData.GetFinalState(CombatStateType.PhysicsDmg);
            vm.OpticsDmg = shipData.GetFinalState(CombatStateType.OpticsDmg);
            vm.ParticleDmg = shipData.GetFinalState(CombatStateType.ParticleDmg);
            vm.PlasmaDmg = shipData.GetFinalState(CombatStateType.PlasmaDmg);
            vm.SlotCount = shipData.GetMaxSlot();

            vm.EquipedCombatKeyList = shipData.GetEquipedCombatItemList();
            vm.EquipedEngineKey = shipData.GetEquipedEngine();
            vm.EquipedReactorKey = shipData.GetEquipedReactor();
            vm.EquipedRadiatorKey = shipData.GetEquipedRadiator();
        }
        public static void OnChangeUserItemData(this ShipMenuUIViewModel vm, long credit, long superCredit)//콜백
        {
            vm.Credit = credit;
            vm.SuperCredit = superCredit;
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
