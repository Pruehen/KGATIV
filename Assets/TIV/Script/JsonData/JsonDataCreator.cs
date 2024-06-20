using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using EnumTypes;
using System;
using System.Drawing.Drawing2D;
using UnityEditor.SceneManagement;

public class ShipTable
{
    public int _key;
    public float _hp;
    public float _atk;
    public float _def;
    public string _name;
    public int _shipClass;
    public int _star;
    public int _maxCombatSlot;

    public ShipTable(int key, float hp, float atk, float def, string name, int shipClass, int star, int maxCombatSlot)
    {
        _key = key;
        _hp = hp;
        _atk = atk;
        _def = def;
        _name = name;
        _shipClass = shipClass;
        _star = star;
        _maxCombatSlot = maxCombatSlot;
    }

    float StateMultiple(int level)
    {
        return ((level - 1) * 0.13f) + 1;
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
    public WeaponSkillTable(int key, WeaponProjectileType weaponProjectileType, float dmg, float collTime, float maxRange, float projectileVelocity, float halfDistance, bool canIntercept)
    {
        _key = key;
        _weaponProjectileType = weaponProjectileType;
        _dmg = dmg;
        _collTime = collTime;
        _maxRange = maxRange;
        _projectileVelocity = projectileVelocity;
        _halfDistance = halfDistance;
        _canIntercept = canIntercept;
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
                UserHaveItemData item = JsonDataManager.DataLode_UserHaveItemData((ItemType)(_gachaCodeTable[dropCode]._itemTableCode));
                item.AddItem(_gachaCodeTable[dropCode]._count);
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
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserHaveItemDataListCache, UserHaveItemDataList.FilePath());
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

public class SetTypeTable
{
    SetType _setTypeKey;
    SetEffectTable _2SetEffect;
    SetEffectTable _6SetEffect;
}
public class SetEffectTable
{

}

public class UserHaveShipData
{
    public int _shipTablekey;
    public int _level;
    public bool _isHave;
    public enum EquipSlotType
    {
        Thruster,
        Reactor,
        Radiator,
        Combat
    }
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

    public bool Equip(EquipSlotType slotType, string userEquipKey)
    {
        UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(userEquipKey);
        if(equipData._equipedShipKey >= 0)
        {
            UserHaveShipData shipData = JsonDataManager.DataLode_UserHaveShipData(equipData._equipedShipKey);
            shipData.Unequip(slotType, userEquipKey);
        }

        switch (slotType)
        {
            case EquipSlotType.Thruster:
                _thrusterSlotItemKey = userEquipKey;
                return true;                
            case EquipSlotType.Reactor:
                _reactorSlotItemKey = userEquipKey;
                return true;
            case EquipSlotType.Radiator:
                _radiatorSlotItemKey = userEquipKey;
                return true;
            case EquipSlotType.Combat:
                if(CanEquip_Combat(userEquipKey))
                {
                    _combatSlotItemKeyList.Add(userEquipKey);
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }
    public void Unequip(EquipSlotType slotType, string userEquipKey)
    {
        UserHaveEquipData equipData = JsonDataManager.DataLode_UserHaveEquipData(userEquipKey);
        equipData._equipedShipKey = -1;

        switch (slotType)
        {
            case EquipSlotType.Thruster:
                _thrusterSlotItemKey = string.Empty;
                break;
            case EquipSlotType.Reactor:
                _reactorSlotItemKey = string.Empty;
                break;
            case EquipSlotType.Radiator:
                _radiatorSlotItemKey = string.Empty;
                break;
            case EquipSlotType.Combat:
                if(_combatSlotItemKeyList.Contains(userEquipKey))
                {
                    _combatSlotItemKeyList.Remove(userEquipKey);
                }
                break;
        }
    }
    bool CanEquip_Combat(string newkey)
    {
        int slotAvailability = GetSlotAvailability();

        UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(newkey);
        EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
        int itemOccupiedSlot = table._slotUsage;

        return (slotAvailability >= itemOccupiedSlot);
    }
    int GetSlotAvailability()
    {
        ShipTable shipTable = JsonDataManager.DataLode_ShipTable(_shipTablekey);
        int maxSlot = shipTable._maxCombatSlot;
        int totalOccupiedSlot = 0;
        if (_combatSlotItemKeyList != null || _combatSlotItemKeyList.Count > 0)
        {
            foreach (string item in _combatSlotItemKeyList)
            {
                UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(item);
                EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
                totalOccupiedSlot += table._slotUsage;
            }
        }
        return maxSlot - totalOccupiedSlot;
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
    public int _equipTableKey;
    public SetType _setType;
    public int _level;
    public int _optimizedLevel;
    public EquipStateSet _mainState;
    public List<EquipStateSet> _subStateList;
    public int _equipedShipKey;

    EquipTable _equipTable;

    [JsonConstructor]
    public UserHaveEquipData(string itemUniqueKey, int equipTableKey, SetType setType, int level, int optimizedLevel, EquipStateSet mainState, List<EquipStateSet> subStateList, int equipedShipKey)
    {
        _itemUniqueKey = itemUniqueKey;
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

        _itemUniqueKey = $"{unixTimeSeconds}_{createNum}{equipTableKey}{(int)setType}{(int)mainStateType}";
        _equipTableKey = equipTableKey;
        _setType = setType;
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
        if(_level + plusLevel > 20 || plusLevel <= 0)
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
}
public class UserHaveEquipDataDictionary
{    
    public Dictionary<string, UserHaveEquipData> _dic = new Dictionary<string, UserHaveEquipData>();
    [JsonConstructor]
    public UserHaveEquipDataDictionary(Dictionary<string, UserHaveEquipData> dic)
    {
        _dic = dic;
    }
    public UserHaveEquipDataDictionary()
    {
        _dic = new Dictionary<string, UserHaveEquipData>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/UserData/UserData_UseEquip.json";
    }
}
public class UserHaveItemData
{
    public ItemType _key;
    public int _value;

    public UserHaveItemData(ItemType key, int value)
    {
        _key = key;
        _value = value;        
    }

    public void AddItem(int value)
    {
        _value += value;
    }
    public bool TryUseItem(int value)
    {
        if(_value - value < 0)
            return false;
        else
        {
            _value -= value;
            return true;
        }
    }
}
public class UserHaveItemDataList
{
    public List<UserHaveItemData> list = new List<UserHaveItemData>();
    [JsonConstructor]
    public UserHaveItemDataList(List<UserHaveItemData> list)
    {
        this.list = list;
    }
    public UserHaveItemDataList()
    {
        list = new List<UserHaveItemData>();
    }
    public static string FilePath()
    {
        return "/Resources/DataBase/UserData/UserData_Item.json";
    }
}

public class JsonDataCreator : MonoBehaviour
{
    string saveDataFileName = GachaTable.FilePath();
    string saveFolderPath = "/Resources/DataBase/Table/Value";

    public GachaTable saveData;

    private void OnEnable()
    {
        SetData();
    }

    void SetData()
    {
        //JsonDataManager.jsonCache.GachaTableCache.TryGacha(100);
        //EquipManager.RandomEquipDrop(SetType.Alpha, 100);
    }

    void DataCreate()//Dictionary 데이터를 json으로 저장하는 함수
    {
        string folderPath = Application.dataPath + saveFolderPath;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        string data = JsonConvert.SerializeObject(saveData, Formatting.Indented);

        File.WriteAllText(Application.dataPath + saveDataFileName, data);

        Debug.Log("데이터 생성 및 저장 완료");
    }
}