using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using EnumTypes;
using System;
using static UserData;
using Unity.VisualScripting;

public class ShipTable
{
    [JsonProperty] public int _key { get; private set; }
    [JsonProperty] public float _hp { get; private set; }

    [JsonProperty] public float _atk { get; private set; }

    [JsonProperty] public float _def { get; private set; }

    [JsonProperty] public string _name { get; private set; }

    [JsonProperty] public int _shipClass { get; private set; }

    [JsonProperty] public int _star { get; private set; }

    [JsonProperty] public int _maxCombatSlot { get; private set; }
    [JsonProperty] public int _cost { get; private set; }

    [JsonConstructor]
    public ShipTable(int key, float hp, float atk, float def, string name, int shipClass, int star, int maxCombatSlot, int cost)
    {
        _key = key;
        _hp = hp;
        _atk = atk;
        _def = def;
        _name = name;
        _shipClass = shipClass;
        _star = star;
        _maxCombatSlot = maxCombatSlot;
        _cost = cost;
    }

    float StateMultiple(int level)
    {
        return ((level - 1) * 0.78f) + 1;
    }
    public static string GetClass(int shipClass) 
    {
        switch (shipClass)
        {
            case 0:
                return "호위함";
            case 1:
                return "구축함";
            case 2:
                return "순양함";
            case 3:
                return "전함";
            case 4:
                return "타이탄";
            default:
                return "알 수 없음";
        }
    }
    public static string GetStar(int star)
    {
        if (star == 4)
        {
            return "****";
        }
        else
        {
            return "*****";
        }
    }
    public float GetHp(int level) { return _hp * StateMultiple(level); }
    public float GetAtk(int level) { return _atk * StateMultiple(level); }
    public float GetDef(int level) { return _def * StateMultiple(level); }    
}
public class ShipTableList
{
    public List<ShipTable> list = new List<ShipTable>();
    [JsonConstructor]
    public ShipTableList(List<ShipTable> list)
    {
        this.list = list;
    }
    public ShipTableList()
    {
        list = new List<ShipTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Ship/ShipTable.json";
    }
}

public class EquipTable
{    
    public int _key;
    public EquipType _type;
    public int _star;
    public string _name;
    public int _weaponSkillKey;
    public List<SetType> _validSetList = new List<SetType>();
    public string _info;
    public int _slotUsage;
    public string _spriteName;

    public EquipTable(int key, EquipType type, int star, string name, int weaponSkillKey, List<SetType> validSetList, string info, int slotUsage, string spriteName)
    {
        _key = key;
        _type = type;
        _star = star;
        _name = name;
        _weaponSkillKey = weaponSkillKey;
        _validSetList = validSetList;
        _info = info;
        _slotUsage = slotUsage;
        _spriteName = spriteName;
    }

    public static string GetEquipType(EquipType equipType)
    {
        switch (equipType) 
        {
            case EquipType.Weapon:
                return "무기";
            case EquipType.Armor:
                return "장갑";
            case EquipType.Radiator:
                return "방열기";
            case EquipType.Reactor:
                return "반응로";
            case EquipType.Thruster:
                return "추진기";
            default:
                return "알 수 없음";
        }
    }
}
public class EquipTableList
{
    public List<EquipTable> list = new List<EquipTable>();
    [JsonConstructor]
    public EquipTableList(List<EquipTable> list)
    {
        this.list = list;
    }
    public EquipTableList()
    {
        list = new List<EquipTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Equip/EquipTeble.json";
    }
}
public class EquipSetTable
{
    public SetType _setTypeKey;
    public string _setEffectName;
    public string _set1Key;
    public string _set2Key;
    public string _set4Key;

    [JsonConstructor]
    public EquipSetTable(SetType setTypeKey, string setEffectName, string set1Key, string set2Key, string set4Key)
    {
        _setTypeKey = setTypeKey;
        _setEffectName = setEffectName;
        _set1Key = set1Key;
        _set2Key = set2Key;
        _set4Key = set4Key;
    }
}
public class EquipSetTableList
{
    public List<EquipSetTable> list = new List<EquipSetTable>();
    [JsonConstructor]
    public EquipSetTableList(List<EquipSetTable> list)
    {
        this.list = list;
    }
    public EquipSetTableList()
    {
        list = new List<EquipSetTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Equip/EquipSetTable.json";
    }
}
public class BuffTable
{    
    public string _buffKey;
    public string _buffInfo;
    public List<float> _buffValueList = new List<float>();

    [JsonConstructor]
    public BuffTable(string buffKey, string buffInfo, List<float> buffValueList)
    {
        _buffKey = buffKey;
        _buffInfo = buffInfo;
        _buffValueList = buffValueList;
    }
}

public class BuffTableDictionary
{
    public Dictionary<string, BuffTable> _dic = new Dictionary<string, BuffTable>();
    [JsonConstructor]
    public BuffTableDictionary(Dictionary<string, BuffTable> dic)
    {
        _dic = dic;
    }
    public BuffTableDictionary()
    {
        _dic = new Dictionary<string, BuffTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Value/BuffTable.json";
    }
}

public class WeaponSkillTable
{
    public int _key;
    public WeaponProjectileType _weaponProjectileType;
    public float _dmg;
    public float _collTime;
    public float _maxRange;
    public float _projectileVelocity;
    public float _halfDistance;
    public bool _canIntercept;
    public string _projectileNameKey;
    public string _info;
    public string _spriteName;
    public WeaponSkillTable(int key, WeaponProjectileType weaponProjectileType, float dmg, float collTime, float maxRange, float projectileVelocity, float halfDistance, bool canIntercept, string projectileNameKey, string info, string spriteName)
    {
        _key = key;
        _weaponProjectileType = weaponProjectileType;
        _dmg = dmg;
        _collTime = collTime;
        _maxRange = maxRange;
        _projectileVelocity = projectileVelocity;
        _halfDistance = halfDistance;
        _canIntercept = canIntercept;
        _projectileNameKey = projectileNameKey;
        _info = info;
        _spriteName = spriteName;
    }
}
public class WeaponSkillTableList
{
    public List<WeaponSkillTable> list = new List<WeaponSkillTable>();
    [JsonConstructor]
    public WeaponSkillTableList(List<WeaponSkillTable> list)
    {
        this.list = list;
    }
    public WeaponSkillTableList()
    {
        list = new List<WeaponSkillTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Equip/WeaponSkillTable.json";
    }
}
public class EquipType_PossibleReinforcementOptionsListTable
{
    public EquipType _type;
    public List<IncreaseableStateType> mainStateList;
    public List<IncreaseableStateType> subStateList;

    public EquipType_PossibleReinforcementOptionsListTable(EquipType type, List<IncreaseableStateType> mainStateList, List<IncreaseableStateType> subStateList)
    {
        _type = type;
        this.mainStateList = mainStateList;
        this.subStateList = subStateList;
    }

    public IncreaseableStateType GetRandomMainState()
    {
        int random = UnityEngine.Random.Range(0, mainStateList.Count);
        return mainStateList[random];
    }
    public IncreaseableStateType GetRandomSubState()
    {
        int random = UnityEngine.Random.Range(0, subStateList.Count);
        return subStateList[random];
    }
}
public class EquipType_PROListTableList
{
    public List<EquipType_PossibleReinforcementOptionsListTable> list = new List<EquipType_PossibleReinforcementOptionsListTable>();
    [JsonConstructor]
    public EquipType_PROListTableList(List<EquipType_PossibleReinforcementOptionsListTable> list)
    {
        this.list = list;
    }
    public EquipType_PROListTableList()
    {
        list = new List<EquipType_PossibleReinforcementOptionsListTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Equip/EquipType_PROListTable.json";
    }
}

public class StateType_StateMultipleTable
{
    public IncreaseableStateType _type;
    public float _multipleValue;
    static string[] _stateTextTemp = new string[2];
    public StateType_StateMultipleTable(IncreaseableStateType type, float multipleValue)
    {
        _type = type;
        _multipleValue = multipleValue;
    }
    public static string[] GetStateText(IncreaseableStateType type, int level)
    {
        float value = level * JsonDataManager.DataLode_StateType_StateMultipleTable(type)._multipleValue;
        _stateTextTemp[0] = GetStateText(type);

        switch (type)
        {
            case IncreaseableStateType.Hp:
                _stateTextTemp[1] = $"{value:F0}";
                break;
            case IncreaseableStateType.HpMultiple:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.Atk:
                _stateTextTemp[1] = $"{value:F0}";
                break;
            case IncreaseableStateType.AtkMultiple:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.Def:
                _stateTextTemp[1] = $"{value:F0}";
                break;
            case IncreaseableStateType.DefMultiple:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.CritRate:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.CritDmg:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.PhysicsDmg:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.OpticsDmg:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.ParticleDmg:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            case IncreaseableStateType.PlasmaDmg:
                _stateTextTemp[1] = $"{value:F1}%";
                break;
            default:
                _stateTextTemp[1] = $"???";
                break;
        }
        return _stateTextTemp;
    }
    public static string GetStateText(IncreaseableStateType type)
    {        
        switch (type)
        {
            case IncreaseableStateType.Hp:
                return "체력";                                
            case IncreaseableStateType.HpMultiple:
                return "체력";
            case IncreaseableStateType.Atk:
                return "공격력";
            case IncreaseableStateType.AtkMultiple:
                return "공격력";
            case IncreaseableStateType.Def:
                return "방어력";
            case IncreaseableStateType.DefMultiple:
                return "방어력";
            case IncreaseableStateType.CritRate:
                return "치명타 확률";
            case IncreaseableStateType.CritDmg:
                return "치명타 피해";
            case IncreaseableStateType.PhysicsDmg:
                return "물리 피해 보너스";
            case IncreaseableStateType.OpticsDmg:
                return "광학 피해 보너스";
            case IncreaseableStateType.ParticleDmg:
                return "입자 피해 보너스";
            case IncreaseableStateType.PlasmaDmg:
                return "플라즈마 피해 보너스";
            default:
                return "알 수 없음";
        }        
    }
}
public class StateType_StateMultipleTableList
{
    public List<StateType_StateMultipleTable> list = new List<StateType_StateMultipleTable>();
    [JsonConstructor]
    public StateType_StateMultipleTableList(List<StateType_StateMultipleTable> list)
    {
        this.list = list;
    }
    public StateType_StateMultipleTableList()
    {
        list = new List<StateType_StateMultipleTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Value/StateType_StateMultipleTable.json";
    }
}
public class GachaTable
{
    public Dictionary<GachaItemType, GachaItemTable> _itemTableDic;
    public Dictionary<int, GachaCodeTable> _gachaCodeTable;
    [JsonConstructor]
    public GachaTable(Dictionary<GachaItemType, GachaItemTable> itemTableDic, Dictionary<int, GachaCodeTable> gachaCodeTable)
    {
        _itemTableDic = itemTableDic;
        _gachaCodeTable = gachaCodeTable;
    }
    public GachaTable()
    {
        _itemTableDic = new Dictionary<GachaItemType, GachaItemTable>();
        _gachaCodeTable = new Dictionary<int, GachaCodeTable>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/Table/Value/GachaTable.json";
    }

    public void TryGacha(int count)
    {
        List<int> dropItemList = new List<int>();

        for (int i = 0; i < count; i++)
        {
            float randomNum = UnityEngine.Random.Range(0f, 100f);

            if(randomNum < _itemTableDic[GachaItemType.Star5Ship]._percentage)
            {
                Debug.Log("5성!!!!");
                dropItemList.Add(_itemTableDic[GachaItemType.Star5Ship].GetRandomCode());
            }
            else if(randomNum < _itemTableDic[GachaItemType.Star4Ship]._percentage)
            {
                Debug.Log("4성!");
                dropItemList.Add(_itemTableDic[GachaItemType.Star4Ship].GetRandomCode());
            }
            else
            {
                Debug.Log("잡템");
                dropItemList.Add(_itemTableDic[GachaItemType.Item].GetRandomCode());
            }
        }

        AddGachaItem(dropItemList);
    }

    void AddGachaItem(List<int> dropList)
    {
        foreach (int dropCode in dropList)
        {
            if (dropCode >= 1000)
            {
                UserData userData = JsonDataManager.DataLode_UserData();
                //item.AddItem(_gachaCodeTable[dropCode]._count);
            }
            else
            {
                int shipTableKey = _gachaCodeTable[dropCode]._itemTableCode;
                if (JsonDataManager.jsonCache.UserHaveShipDataListCache.Contain(shipTableKey) == false)
                {
                    ShipManager.CreateShip(shipTableKey);
                }
                else
                {
                    Debug.Log("이미 가지고 있는 함선.");
                    //중복 함선 획득 처리
                }
            }
        }
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveShipDataListCache, UserHaveShipDataList.FilePath());
    }
}
public class GachaCodeTable
{
    public int _gachaCode;
    public int _itemTableCode;
    public int _count;
    public GachaCodeTable(int gachaCode, int itemTableCode, int count)
    {
        _gachaCode = gachaCode;
        _itemTableCode = itemTableCode;
        _count = count;
    }
}
public class GachaItemTable
{
    GachaItemType _key;
    public float _percentage;
    public List<int> _dropCodeList = new List<int>();
    public GachaItemTable(GachaItemType key, float percentage, List<int> dropCodeList)
    {
        _key = key;
        _percentage = percentage;
        _dropCodeList = dropCodeList;
    }

    public int GetRandomCode()
    {
        int randomCode = _dropCodeList[UnityEngine.Random.Range(0, _dropCodeList.Count)];
        return randomCode;
    }
}

public class UserHaveShipData
{
    public int _shipTablekey;
    public int _level;
    public bool _isHave;
    public string _thrusterSlotItemKey;
    public string _reactorSlotItemKey;
    public string _radiatorSlotItemKey;
    public List<string> _combatSlotItemKeyList = new List<string>();

    [JsonConstructor]
    public UserHaveShipData(int shipTablekey, int level, bool isHave, string thrusterSlotItemKey, string reactorSlotItemKey, string radiatorSlotItemKey, List<string> combatSlotItemKeyList)
    {
        _shipTablekey = shipTablekey;
        _level = level;
        _isHave = isHave;
        _thrusterSlotItemKey = thrusterSlotItemKey;
        _reactorSlotItemKey = reactorSlotItemKey;
        _radiatorSlotItemKey = radiatorSlotItemKey;
        _combatSlotItemKeyList = combatSlotItemKeyList;
    }
    /// <summary>
    /// 새 함선을 얻었을 때 사용하는 생성자
    /// </summary>
    /// <param name="shipTablekey"></param>
    public UserHaveShipData(int shipTablekey)
    {
        _shipTablekey = shipTablekey;
        _level = 1;
        _thrusterSlotItemKey = string.Empty;
        _reactorSlotItemKey = string.Empty;
        _radiatorSlotItemKey = string.Empty;
        _combatSlotItemKeyList = new List<string>();
    }

    public bool Equip(string userEquipKey)
    {
        UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(userEquipKey);
        if(equipData._equipedShipKey >= 0)//장착하려는 아이템이 이미 함선에 장착되어 있을 경우
        {
            UserHaveShipData shipData = JsonDataManager.DataLode_UserHaveShipData(equipData._equipedShipKey);
            shipData.Unequip(userEquipKey);
        }
        
        switch (equipData.GetEquipType())
        {
            case EquipType.Thruster:
                Unequip(_thrusterSlotItemKey);
                _thrusterSlotItemKey = userEquipKey;
                equipData.Equip(_shipTablekey);
                return true;                

            case EquipType.Reactor:
                Unequip(_reactorSlotItemKey);
                _reactorSlotItemKey = userEquipKey;
                equipData.Equip(_shipTablekey);
                return true;

            case EquipType.Radiator:
                Unequip(_radiatorSlotItemKey);
                _radiatorSlotItemKey = userEquipKey;
                equipData.Equip(_shipTablekey);
                return true;
            default:
                if(CanEquip_Combat(userEquipKey))
                {
                    _combatSlotItemKeyList.Add(userEquipKey);
                    equipData.Equip(_shipTablekey);
                    return true;
                }
                else
                {                    
                    Debug.Log("수용량을 초과하거나 장비를 더 이상 장착할 수 없습니다");
                    return false;
                }
        }        
    }
    public void Unequip(string userEquipKey)
    {
        if (userEquipKey == null || userEquipKey == string.Empty)
        {
            //Debug.LogError("장착 해제할 장비의 키가 없습니다.");
            return;
        }

        UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(userEquipKey);        

        switch (equipData.GetEquipType())
        {
            case EquipType.Thruster:
                _thrusterSlotItemKey = string.Empty;
                equipData.UnEquip();
                break;
            case EquipType.Reactor:
                _reactorSlotItemKey = string.Empty;
                equipData.UnEquip();
                break;
            case EquipType.Radiator:
                _radiatorSlotItemKey = string.Empty;
                equipData.UnEquip();
                break;
            default:
                if(_combatSlotItemKeyList.Contains(userEquipKey))
                {
                    _combatSlotItemKeyList.Remove(userEquipKey);
                }
                equipData.UnEquip();
                break;
        }
    }
    bool CanEquip_Combat(string newkey)
    {
        ShipTable shipTable = JsonDataManager.DataLode_ShipTable(_shipTablekey);
        int slotAvailability = shipTable._maxCombatSlot - GetUseSlotCount();

        UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(newkey);
        EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
        int itemOccupiedSlot = table._slotUsage;

        return (slotAvailability >= itemOccupiedSlot && _combatSlotItemKeyList.Count < 5);
    }
    public int GetUseSlotCount()
    {
        ShipTable shipTable = JsonDataManager.DataLode_ShipTable(_shipTablekey);
        int useSlot = 0;
        if (_combatSlotItemKeyList != null || _combatSlotItemKeyList.Count > 0)
        {
            foreach (string item in _combatSlotItemKeyList)
            {
                UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(item);
                EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
                useSlot += table._slotUsage;
            }
        }
        return useSlot;
    }
    /// <summary>
    /// 현재 장비하고 있는 모든 장비를 string 키 리스트 형태로 반환
    /// </summary>
    /// <returns></returns>
    public List<string> GetAllEquipedItemKey()
    {
        List<string> keyList = new List<string>();
        if(_thrusterSlotItemKey != null && _thrusterSlotItemKey != string.Empty)
        {
            keyList.Add(_thrusterSlotItemKey);
        }
        if (_reactorSlotItemKey != null && _reactorSlotItemKey != string.Empty)
        {
            keyList.Add(_reactorSlotItemKey);
        }
        if (_radiatorSlotItemKey != null && _radiatorSlotItemKey != string.Empty)
        {
            keyList.Add(_radiatorSlotItemKey);
        }
        if(_combatSlotItemKeyList != null && _combatSlotItemKeyList.Count != 0)
        {
            foreach (string key in _combatSlotItemKeyList)
            {
                keyList.Add(key);
            }
        }

        return keyList;
    }
    public List<WeaponSkillTable> GetAllWeaponSkill()
    {
        List<WeaponSkillTable> keyList = new List<WeaponSkillTable>();
        if (_combatSlotItemKeyList != null && _combatSlotItemKeyList.Count != 0)
        {
            foreach (string key in _combatSlotItemKeyList)
            {
                UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(key);
                EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
                int weaponKey = table._weaponSkillKey;
                if (weaponKey >= 0)
                {
                    WeaponSkillTable skillTable = JsonDataManager.DataLode_WeaponSkillTableList(weaponKey);
                    keyList.Add(skillTable);
                }
            }
        }

        return keyList;
    }
    public string GetUtilEquipedItemKey(EquipType equipType)
    {
        switch (equipType)
        {
            case EquipType.Thruster:
                return _thrusterSlotItemKey;                
            case EquipType.Reactor:
                return _reactorSlotItemKey;
            case EquipType.Radiator:
                return _radiatorSlotItemKey;
            default:
                return null;
        }
    }

    public void LevelUp(int addLevel)
    {
        if(addLevel <= 0)//추가 레벨이 0 이하일 경우
        {
            Debug.Log("0레벨 이하로 레벨업을 할 수 없습니다.");
        }
        else if(_level + addLevel > MaxLevel())//레벨 합이 최대 레벨을 초과할 경우
        {
            Debug.Log("최대 레벨을 초과해서 레벨업을 할 수 없습니다.");
        }
        else
        {
            _level += addLevel;
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveShipDataListCache, UserHaveShipDataList.FilePath());
        }
    }

    public static int MaxLevel() { return 10000; }
}
public class UserHaveShipDataList
{
    public List<UserHaveShipData> list = new List<UserHaveShipData>();
    [JsonConstructor]
    public UserHaveShipDataList(List<UserHaveShipData> list)
    {
        this.list = list;
    }
    public UserHaveShipDataList()
    {
        list = new List<UserHaveShipData>();
    }
    public bool Contain(int shipTablekey)
    {        
        return list[shipTablekey]._isHave;
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/UserData/UserData_UseShip.json";
    }
}

public class UserHaveEquipData
{
    public string _itemUniqueKey;
    public int _itemGetTime;
    public int _equipTableKey;
    public SetType _setType;
    public int _level;
    public int _optimizedLevel;
    public EquipStateSet _mainState;
    public List<EquipStateSet> _subStateList;
    public int _equipedShipKey;

    EquipTable _equipTable;
    [JsonIgnore]public Action OnUpgrade { get; set; }

    [JsonConstructor]
    public UserHaveEquipData(string itemUniqueKey, int itemGetTime, int equipTableKey, SetType setType, int level, int optimizedLevel, EquipStateSet mainState, List<EquipStateSet> subStateList, int equipedShipKey)
    {
        _itemUniqueKey = itemUniqueKey;
        _itemGetTime = itemGetTime;
        _equipTableKey = equipTableKey;
        _setType = setType;
        _level = level;
        _optimizedLevel = optimizedLevel;
        _mainState = mainState;
        _subStateList = subStateList;
        _equipedShipKey = equipedShipKey;

        //_mainState.SetValue(_level + 5);
    }
    /// <summary>
    /// 새로운 장비를 얻었을 때 사용하는 생성자
    /// </summary>
    /// <param name="equipTableKey"></param>
    /// <param name="setType"></param>
    /// <param name="mainStateType"></param>
    public UserHaveEquipData(int createNum, int equipTableKey, SetType setType, IncreaseableStateType mainStateType)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        int unixTimeSeconds = (int)now.ToUnixTimeSeconds();
        _itemGetTime = unixTimeSeconds;

        _itemUniqueKey = $"{unixTimeSeconds}_{createNum}{equipTableKey}{(int)setType}{(int)mainStateType}";
        _equipTableKey = equipTableKey;
        if (setType >= 0)
        {
            _setType = setType;
        }
        else
        {
            _setType = (SetType)(-1);
        }
        _level = 0;
        _optimizedLevel = 0;
        _mainState = new EquipStateSet(mainStateType, _level + 5);
        _subStateList = new List<EquipStateSet>();
        _equipedShipKey = -1;
    }
    /// <summary>
    /// 깊은 복사를 수행하기 위한 생성자
    /// </summary>
    /// <param name="userHaveEquipData"></param>
    public UserHaveEquipData(UserHaveEquipData userHaveEquipData)
    {
        _itemUniqueKey = "NULL";
        _equipTableKey = userHaveEquipData._equipTableKey;
        _setType = userHaveEquipData._setType;
        _level = userHaveEquipData._level;
        _optimizedLevel = userHaveEquipData._optimizedLevel;
        _mainState = userHaveEquipData._mainState;
        _subStateList = new List<EquipStateSet>();
        foreach (var item in userHaveEquipData._subStateList)
        {
            _subStateList.Add(item);
        }
        _equipedShipKey = userHaveEquipData._equipedShipKey;

        _mainState.SetValue(_level + 5);
    }
    public static int MaxLevel() { return 20; }

    public class EquipStateSet
    {
        public IncreaseableStateType _stateType;
        public int _level;
        public EquipStateSet(IncreaseableStateType stateType, int level)
        {
            _stateType = stateType;
            _level = level;
        }
        public void SetValue(int value)
        {
            _level = value;
        }
        public float GetValue()
        {
            return _level * JsonDataManager.DataLode_StateType_StateMultipleTable(_stateType)._multipleValue;
        }
        public int GetLevel()
        {
            return _level;
        }
    }
    public List<EquipStateSet> GetAllEquipStateSet()
    {
        List<EquipStateSet> equipStateSetList = new List<EquipStateSet>();
        equipStateSetList.Add(_mainState);

        if(_subStateList != null && _subStateList.Count > 0)
        {
            foreach (EquipStateSet stateSet in _subStateList)
            {
                equipStateSetList.Add(stateSet);
            }
        }
        return equipStateSetList;
    }

    /// <summary>
    /// 장비 레벨 강화 메서드
    /// </summary>
    /// <param name="plusLevel"></param>
    /// <param name="msg"></param>
    public void LevelUp(int plusLevel)
    {
        if(_level + plusLevel > MaxLevel() || plusLevel <= 0)
        {
            Debug.Log("잘못된 강화 수치");
        }
        else
        {            
            for (int i = 0; i < plusLevel; i++)
            {
                _level++;
                if(_level%4 == 0)
                {
                    Optimize();
                }
            }
            _mainState.SetValue(_level + 5);
            OnUpgrade?.Invoke();
            Debug.Log("강화 성공");
        }
    }
    /// <summary>
    /// 장비 옵티마이즈 메서드
    /// </summary>
    /// <param name="plusLevel"></param>
    /// <param name="msg"></param>
    public void Optimize()
    {
        _optimizedLevel++;
        EquipTable table = JsonDataManager.DataLode_EquipTable(_equipTableKey);
        IncreaseableStateType randomType = JsonDataManager.DataLode_EquipType_PROTable(table._type).GetRandomSubState();
        int stateLevel = UnityEngine.Random.Range(3, 6);
        _subStateList.Add(new EquipStateSet(randomType, stateLevel));

        Debug.Log("옵티마이즈 성공");
    }

    public string GetName()
    {
        if(_equipTable == null)
        {
            _equipTable = JsonDataManager.DataLode_EquipTable(this._equipTableKey);
        }
        return _equipTable._name;
    }
    public EquipType GetEquipType()
    {
        if (_equipTable == null)
        {
            _equipTable = JsonDataManager.DataLode_EquipTable(this._equipTableKey);
        }
        return _equipTable._type;
    }
    public void Equip(int shipKey)
    {
        _equipedShipKey = shipKey;
    }
    public void UnEquip()
    {
        _equipedShipKey = -1;
    }
}
public class UserHaveEquipDataPack
{    
    [JsonProperty] Dictionary<string, UserHaveEquipData> _saveDic = new Dictionary<string, UserHaveEquipData>();
    [JsonIgnore] public List<UserHaveEquipData> cacheList = new List<UserHaveEquipData>();

    [JsonConstructor]
    public UserHaveEquipDataPack(Dictionary<string, UserHaveEquipData> saveDic)
    {
        _saveDic = saveDic;
    }
    public UserHaveEquipDataPack()
    {
        _saveDic = new Dictionary<string, UserHaveEquipData>();
        cacheList = new List<UserHaveEquipData>();
    }
    public static UserHaveEquipDataPack Instance { get { return JsonDataManager.jsonCache.UserHaveEquipDataPackCache; } }
    public void AllDicItemUpdate_EquipedShipKey()
    {
        UserHaveShipDataList shipDataList = JsonDataManager.jsonCache.UserHaveShipDataListCache;

        foreach (UserHaveShipData shipData in shipDataList.list)
        {
            foreach (string equipedKey in shipData.GetAllEquipedItemKey())
            {
                _saveDic[equipedKey]._equipedShipKey = shipData._shipTablekey;
            }
        }
    }
    public void DataPackInit_CopyDic_PathList()
    {
        cacheList = new List<UserHaveEquipData>();
        foreach (var item in _saveDic)
        {
            cacheList.Add(item.Value);
        }        
        CacheListSort(SortValue.Level);
    }
    public void Add(string key, UserHaveEquipData item)
    {
        _saveDic.Add(key, item);
        cacheList.Insert(0, item);
    }
    public enum SortValue
    {
        Level,
        Key,
        SetType,
        GetTime,
        IsEquiped
    }
    public void CacheListSort(SortValue sortValue)
    {
        switch (sortValue)
        {
            case SortValue.Level:
                cacheList.Sort((x, y) => y._level.CompareTo(x._level));
                break;
            case SortValue.Key:
                cacheList.Sort((x, y) => y._equipTableKey.CompareTo(x._equipTableKey));
                break;
            case SortValue.SetType:
                cacheList.Sort((x, y) => y._setType.CompareTo(x._setType));
                break;
            case SortValue.GetTime:
                cacheList.Sort((x, y) => y._itemGetTime.CompareTo(x._itemGetTime));
                break;
            case SortValue.IsEquiped:
                cacheList.Sort((x, y) => y._equipedShipKey.CompareTo(x._equipedShipKey));
                break;
            default:
                break;
        }
    }
    public UserHaveEquipData Find(string key)
    {
        return _saveDic[key];
    }

    public static Dictionary<string, UserHaveEquipData> GetSaveDictionary()
    {
        return JsonDataManager.jsonCache.UserHaveEquipDataPackCache._saveDic;
    }
    public static List<UserHaveEquipData> GetCacheList()
    {
        return JsonDataManager.jsonCache.UserHaveEquipDataPackCache.cacheList;
    }
    public static void CommandSave()
    {
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveEquipDataPackCache, FilePath());
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/UserData/UserData_UseEquip.json";
    }
}

public class UserData
{
    [JsonProperty] public long Credit { get; private set; }
    [JsonProperty] public int SuperCredit { get; private set; }
    [JsonProperty] public int Fuel { get; private set; }
    [JsonProperty] public int ReFuelRemaning { get; private set; }
    [JsonProperty] public int CurPrmStage { get; set; }
    [JsonProperty] public int CurSecStage { get; set; }
    [JsonProperty] public int FleetCost { get; private set; }
    [JsonProperty] public DateTime LastTime { get; private set; }
    [JsonProperty] List<ShipPositionData> _shipPositionDatas;

    public static UserData Instance { get { return JsonDataManager.DataLode_UserData(); } }
    public static void Save()
    {
        JsonDataManager.jsonCache.UserDataCache.LastTime = DateTime.Now;
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
    }

    [JsonConstructor]
    public UserData(long credit, int superCredit, int fuel, int reFuelRemaning, int curPrmStage, int curSecStage, int fleetCost, DateTime lastTime, List<ShipPositionData> shipPositionDatas)
    {
        Credit = credit;
        SuperCredit = superCredit;
        Fuel = fuel;
        ReFuelRemaning = reFuelRemaning;
        CurPrmStage = curPrmStage;
        CurSecStage = curSecStage;
        FleetCost = fleetCost;
        LastTime = lastTime;
        _shipPositionDatas = shipPositionDatas;        
    }
    public UserData()
    {
        Credit = 0;
        SuperCredit = 0;
        Fuel = MaxFuel();
        ReFuelRemaning = 0;
        CurPrmStage = 1;
        CurSecStage = 1;
        FleetCost = 1;
        LastTime = DateTime.Now;
        _shipPositionDatas = new List<ShipPositionData>();
        _shipPositionDatas.Add(new ShipPositionData());        
    }
    public class ShipPositionData
    {        
        public int _shipKey;
        public float _posX;
        public float _posZ;

        [JsonConstructor]
        public ShipPositionData(int shipKey, float posX, float posZ)
        {            
            _shipKey = shipKey;
            _posX = posX;
            _posZ = posZ;
        }
        public ShipPositionData()
        {            
            _shipKey = 0;
            _posX = 0;
            _posZ = 0;
        }
    }
    public List<ShipPositionData> GetShipPosDataList() { return _shipPositionDatas; }    

    public void Register_onSecChange()
    {
        TimeManager.Instance.Register_onSecChange(OnTimeChange);
    }
    public void SetShipPosDatas(Dictionary<int, ShipMaster> dicData)
    {
        _shipPositionDatas.Clear();
        foreach (var item in dicData)
        {
            int shipKey = item.Value.CombatData.GetShipTableKey();
            Vector3 targetPos = item.Value.Engine.GetMoveTargetPos();
            ShipPositionData data = new ShipPositionData(shipKey, targetPos.x, targetPos.z);

            _shipPositionDatas.Add(data);
        }
    }

    //MVVM 관련 코드==============================
    Action<UserData> _onRefreshViewModel_CallBack;
    Action<int> _onRefuelRemaningChange;
    Action<int> _onCreditChange;
    public void Register_OnRefreshViewModel(Action<UserData> callBack)
    {
        _onRefreshViewModel_CallBack += callBack;
    }
    public void UnRegister_OnRefreshViewModel(Action<UserData> callBack)
    {
        _onRefreshViewModel_CallBack -= callBack;
    }
    public void Register_OnRefuelRemaningChange(Action<int> callBack)
    {
        _onRefuelRemaningChange += callBack;
    }
    public void UnRegister_OnRefuelRemaningChange(Action<int> callBack)
    {
        _onRefuelRemaningChange -= callBack;
    }
    public void Register_OnCreditChange(Action<int> callBack)
    {
        _onCreditChange += callBack;
    }
    public void UnRegister_OnCreditChange(Action<int> callBack)
    {
        _onCreditChange -= callBack;
    }

    public void RefreshViewModel()
    {
        _onRefreshViewModel_CallBack?.Invoke(this);
    }

    public static int MaxFuel() { return 240; }
    public static int ReFuelTime() { return 360; }
    public static string FilePath()
    {
        return "/Resources/DataBase/UserData/UserData.json";
    }

    /// <summary>
    /// 크레딧 획득 메서드
    /// </summary>
    /// <param name="value"></param>
    public void AddCredit(long value)
    {
        if (value != 0)
        {
            Credit += value;
            AddCreditViewOverUIManager.Instance.OnAddCredit((int)value);
            _onCreditChange?.Invoke((int)value);
            RefreshViewModel();
        }
    }
    /// <summary>
    /// 크레딧 사용 메서드. 충분하면 사용 후 true 반환, 아니면 false 반환
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryUseCredit(long value)
    {
        if(Credit < value)
        {
            UIManager.Instance.PopUpWdw_WarningPopUpUI("크레딧이 부족합니다.", 2);
            return false;
        }
        else
        {
            Credit -= value;
            _onCreditChange?.Invoke((int)-value);
            RefreshViewModel();
            Save();
            return true;
        }
    }
    /// <summary>
    /// 슈퍼크레딧 획득 메서드
    /// </summary>
    /// <param name="value"></param>
    public void AddSuperCredit(int value)
    {
        SuperCredit += value;
        RefreshViewModel();
        Save();
    }
    /// <summary>
    /// 슈퍼 크레딧 사용 메서드. 충분하면 사용 후 true 반환, 아니면 false 반환
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryUseSuperCredit(int value)
    {
        if (SuperCredit < value)
        {
            UIManager.Instance.PopUpWdw_WarningPopUpUI("슈퍼 크레딧이 부족합니다.", 2);
            return false;
        }
        else
        {
            SuperCredit -= value;
            RefreshViewModel();
            Save();
            return true;
        }
    }
    void OnTimeChange()
    {
        ReFuelRemaning--;

        if(ReFuelRemaning <= 0)
        {
            if(TryAddFuel_Nature())
            {
                ReFuelRemaning = ReFuelTime();
                Save();
            }
            else
            {
                ReFuelRemaning = 0;
            }
        }

        _onRefuelRemaningChange?.Invoke(ReFuelRemaning);        
    }
    /// <summary>
    /// 연료의 자연 충전 메서드. 6분에 1회씩 호출해야 함.
    /// </summary>
    /// <returns></returns>
    public bool TryAddFuel_Nature()
    {
        if(Fuel >= MaxFuel())
        {
            return false;
        }
        else
        {
            Fuel++;            
            RefreshViewModel();
            return true;
        }
    }
    /// <summary>
    /// 아이템 사용에 의한 연료 충전 메서드
    /// </summary>
    /// <param name="value"></param>
    public void AddFuel_OnUseItem(int value)
    {
        Fuel += value;
        RefreshViewModel();
        Save();
    }

    public void AddFuel_ProportionalToUnconnectedTime()
    {
        int deltaTime = TimeManager.GetDeltaTime(LastTime, DateTime.Now);
        int fuelTemp = Fuel;
        Fuel += deltaTime / ReFuelTime();
        if(Fuel > MaxFuel())
        {
            Fuel = MaxFuel();
        }

        if (fuelTemp != Fuel)
        {
            UIManager.Instance.PopUpWdw_MsgPopUpUI($"연료 {Fuel - fuelTemp} 만큼 획득", 7);
            RefreshViewModel();
            Save();
        }
    }

    /// <summary>
    /// 연료 사용 시도 메서드. 연료가 충분하면 연료를 소모하고 true 반환. 아니면 false 반환
    /// </summary>
    /// <param name="value"></param>
    public bool TryUseFuel(int value)
    {
        if (Fuel < value)
        {
            UIManager.Instance.PopUpWdw_WarningPopUpUI("연료가 부족합니다.", 2);
            return false;
        }
        else
        {
            Fuel -= value;
            RefreshViewModel();
            Save();
            return true;
        }
    }

    public void UpgradeFleetCost()
    {
        FleetCost++;        
    }
}