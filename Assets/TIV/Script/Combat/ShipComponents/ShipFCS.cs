using System.Collections.Generic;
using UnityEngine;

public class ShipFCS : MonoBehaviour
{
    ShipCombatData CombatData;
    List<Weapon> _usingWeaponList;
    public List<Weapon> UsingWeaponList() { return _usingWeaponList; }


    [Header("적 함선일 경우, 사용할 무기 키")]
    [SerializeField] List<EnumTypes.WeaponSkillKeyCode> usingWeaponList_Key;

    int _curShipKey;
    public void Init(int shipKey)
    {
        _curShipKey = shipKey;
        CombatData = GetComponent<ShipCombatData>();
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

        CombatData.Register_OnAllStaticDataUpdate(UpdateUsingWeapon_OnEquipChange);//장비 갱신을 위한 이벤트 연결
        CombatData.Register_OnDead(OnDead);
    }
    public void UpdateUsingWeapon_OnEquipChange()//장비가 변경되었을 경우, 자신에게 장착된 장비를 갱신하는 메서드
    {
        if (_curShipKey >= 0)
        {
            foreach (var item in _usingWeaponList)
            {
                Destroy(item);
            }
            _usingWeaponList.Clear();

            List<WeaponSkillTable> usingWeaponListTemp = JsonDataManager.DataLode_UserHaveShipData(_curShipKey).GetAllWeaponSkill();
            foreach (WeaponSkillTable item in usingWeaponListTemp)
            {
                Weapon weapon = this.gameObject.AddComponent<Weapon>();
                weapon.Init(item);
                _usingWeaponList.Add(weapon);
            }
        }
    }

    public void SetMainTarget(SortedDictionary<float, ITargetable> mainTarget)//모든 무기를 타게팅함
    {
        foreach (Weapon weapon in _usingWeaponList)
        {
            weapon.SetTarget(mainTarget);
        }
    }
    public void SetInterceptTarget(SortedDictionary<float, ITargetable> interceptTarget)//요격 가능한 무기를 대상으로 타게팅함.
    {
        foreach (Weapon weapon in _usingWeaponList)
        {
            if (weapon.Table._canIntercept)
            {
                weapon.SetTarget(interceptTarget);                
            }
        }
    }

    void OnDead()
    {
        CombatData.UnRegister_OnAllStaticDataUpdate(UpdateUsingWeapon_OnEquipChange);
    }
}
