using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using EnumTypes;
using System;

public class ShipTable
{
    public int _key;
    public float _hp;
    public float _atk;
    public float _def;
    public float _accuracy;
    public float _evade;
    public string _name;
    public int _shipClass;
    public int _star;
    public int _maxCombatSlot;

    public ShipTable(int key, float hp, float atk, float def, float accuracy, float evade, string name, int shipClass, int star, int maxCombatSlot)
    {
        _key = key;
        _hp = hp;
        _atk = atk;
        _def = def;
        _accuracy = accuracy;
        _evade = evade;
        _name = name;
        _shipClass = shipClass;
        _star = star;
        _maxCombatSlot = maxCombatSlot;
    }
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
        return "/TIV/DataBase/Table/Ship/ShipTable.json";
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

    public EquipTable(int key, EquipType type, int star, string name, int weaponSkillKey, List<SetType> validSetList, string info, int slotUsage)
    {
        _key = key;
        _type = type;
        _star = star;
        _name = name;
        _weaponSkillKey = weaponSkillKey;
        _validSetList = validSetList;
        _info = info;
        _slotUsage = slotUsage;
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
        return "/TIV/DataBase/Table/Equip/EquipTeble.json";
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
        return "/TIV/DataBase/Table/Equip/WeaponSkillTable.json";
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
        return "/TIV/DataBase/Table/Equip/EquipType_PROListTable.json";
    }
}

public class StateType_StateMultipleTable
{
    public IncreaseableStateType _type;
    public float _multipleValue;
    public StateType_StateMultipleTable(IncreaseableStateType type, float multipleValue)
    {
        _type = type;
        _multipleValue = multipleValue;
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
        return "/TIV/DataBase/Table/Value/StateType_StateMultipleTable.json";
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
        return "/TIV/DataBase/Table/Value/GachaTable.json";
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
        JsonDataManager.DataSave(JsonDataManager.jsonCache.UserHaveItemDataListCache, UserHaveItemDataList.FilePath());
        JsonDataManager.DataSave(JsonDataManager.jsonCache.UserHaveShipDataListCache, UserHaveShipDataList.FilePath());
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

    public bool Equip(EquipSlotType slotType, string newkey)
    {
        switch (slotType)
        {
            case EquipSlotType.Thruster:
                _thrusterSlotItemKey = newkey;
                return true;                
            case EquipSlotType.Reactor:
                _reactorSlotItemKey = newkey;
                return true;
            case EquipSlotType.Radiator:
                _radiatorSlotItemKey = newkey;
                return true;
            case EquipSlotType.Combat:
                if(CanEquip_Combat(newkey))
                {
                    _combatSlotItemKeyList.Add(newkey);
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }
    public void Unequip(EquipSlotType slotType, string newkey)
    {
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
                if(_combatSlotItemKeyList.Contains(newkey))
                {
                    _combatSlotItemKeyList.Remove(newkey);
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
        return "/TIV/DataBase/UserData/UserData_UseShip.json";
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

    [JsonConstructor]
    public UserHaveEquipData(string itemUniqueKey, int equipTableKey, SetType setType, int level, int optimizedLevel, EquipStateSet mainState, List<EquipStateSet> subStateList)
    {
        _itemUniqueKey = itemUniqueKey;
        _equipTableKey = equipTableKey;
        _setType = setType;
        _level = level;
        _optimizedLevel = optimizedLevel;
        _mainState = mainState;
        _subStateList = subStateList;
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
        _level = 1;
        _optimizedLevel = 0;
        _mainState = new EquipStateSet(mainStateType, 1);
        _subStateList = new List<EquipStateSet>();
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
        public void StateUp(int plusValue)
        {
            _level += plusValue;
        }
    }

    /// <summary>
    /// 장비 레벨 강화 메서드
    /// </summary>
    /// <param name="plusLevel"></param>
    /// <param name="msg"></param>
    public void LevelUp(int plusLevel, out string msg)
    {
        if(_level + plusLevel > 20 || plusLevel <= 0)
        {
            msg = "잘못된 강화 수치";
        }
        else
        {
            _level += plusLevel;
            _mainState.StateUp(plusLevel);
            msg = $"{plusLevel}+강화 성공";
        }
    }
    /// <summary>
    /// 장비 옵티마이즈 메서드
    /// </summary>
    /// <param name="plusLevel"></param>
    /// <param name="msg"></param>
    public void Optimize(int plusLevel, out string msg)
    {
        if(_optimizedLevel + plusLevel > _level/4 || plusLevel <= 0)
        {
            msg = "잘못된 옵티마이즈 수치";
        }
        else
        {
            _optimizedLevel += plusLevel;
            for (int i = 0; i < plusLevel; i++)
            {
                IncreaseableStateType randomType = JsonDataManager.DataLode_EquipType_PROTable((int)_equipTableKey).GetRandomSubState();
                int stateLevel = UnityEngine.Random.Range(3, 6);
                _subStateList.Add(new EquipStateSet(randomType, stateLevel));
            }

            msg = $"옵티마이즈 {plusLevel}회 성공";
        }
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
        return "/TIV/DataBase/UserData/UserData_UseEquip.json";
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
        return "/TIV/DataBase/UserData/UserData_Item.json";
    }
}

public class JsonDataCreator : MonoBehaviour
{
    string saveDataFileName = GachaTable.FilePath();
    string saveFolderPath = "/TIV/DataBase/Table/Value";

    public GachaTable saveData;

    private void OnEnable()
    {
        SetData();
    }

    void SetData()
    {
        //JsonDataManager.jsonCache.GachaTableCache.TryGacha(100);
        EquipManager.RandomEquipDrop(SetType.Alpha, 1);
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