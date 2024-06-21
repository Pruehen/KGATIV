using System.Collections.Generic;
using UnityEngine;

public class ShipFCS : MonoBehaviour
{
    List<Weapon> _usingWeaponList;

    [Header("적 함선일 경우, 사용할 무기 키")]
    [SerializeField] List<EnumTypes.WeaponSkillKeyCode> usingWeaponList_Key;

    int _curShipKey;
    public void Init(int shipKey)
    {
        _curShipKey = shipKey;
        _usingWeaponList = new List<Weapon>();

        if (shipKey >= 0)
        {
            List<WeaponSkillTable> usingWeaponListTemp = JsonDataManager.DataLode_UserHaveShipData(shipKey).GetAllWeaponSkill();
            foreach (WeaponSkillTable item in usingWeaponListTemp)
            {
                Weapon weapon = this.gameObject.AddComponent<Weapon>();
                weapon.Init(item);
                _usingWeaponList.Add(weapon);
            }
        }
        else
        {            
            foreach (int key in usingWeaponList_Key)
            {
                WeaponSkillTable item = JsonDataManager.DataLode_WeaponSkillTableList(key);
                Weapon weapon = this.gameObject.AddComponent<Weapon>();
                weapon.Init(item);
                _usingWeaponList.Add(weapon);
            }
        }
    }
    public void UpdateUsingWeapon_OnEquipChange()
    {
        if (_curShipKey >= 0)
        {
            foreach (var item in _usingWeaponList)
            {
                Destroy(item);
            }

            List<WeaponSkillTable> usingWeaponListTemp = JsonDataManager.DataLode_UserHaveShipData(_curShipKey).GetAllWeaponSkill();
            foreach (WeaponSkillTable item in usingWeaponListTemp)
            {
                Weapon weapon = this.gameObject.AddComponent<Weapon>();
                weapon.Init(item);
                _usingWeaponList.Add(weapon);
            }
        }
    }

    public void SetMainTarget(ITargetable mainTarget)
    {
        foreach (Weapon weapon in _usingWeaponList)
        {
            weapon.SetTarget(mainTarget);
        }
    }
}
