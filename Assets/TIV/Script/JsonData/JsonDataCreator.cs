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
                return "ȣ����";
            case 1:
                return "������";
            case 2:
                return "������";
            case 3:
                return "����";
            case 4:
                return "Ÿ��ź";
            default:
                return "�� �� ����";
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
                return "����";
            case EquipType.Armor:
                return "�尩";
            case EquipType.Radiator:
                return "�濭��";
            case EquipType.Reactor:
                return "������";
            case EquipType.Thruster:
                return "������";
            default:
                return "�� �� ����";
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
                return "ü��";                                
            case IncreaseableStateType.HpMultiple:
                return "ü��";
            case IncreaseableStateType.Atk:
                return "���ݷ�";
            case IncreaseableStateType.AtkMultiple:
                return "���ݷ�";
            case IncreaseableStateType.Def:
                return "����";
            case IncreaseableStateType.DefMultiple:
                return "����";
            case IncreaseableStateType.CritRate:
                return "ġ��Ÿ Ȯ��";
            case IncreaseableStateType.CritDmg:
                return "ġ��Ÿ ����";
            case IncreaseableStateType.PhysicsDmg:
                return "���� ���� ���ʽ�";
            case IncreaseableStateType.OpticsDmg:
                return "���� ���� ���ʽ�";
            case IncreaseableStateType.ParticleDmg:
                return "���� ���� ���ʽ�";
            case IncreaseableStateType.PlasmaDmg:
                return "�ö�� ���� ���ʽ�";
            default:
                return "�� �� ����";
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
                Debug.Log("5��!!!!");
                dropItemList.Add(_itemTableDic[GachaItemType.Star5Ship].GetRandomCode());
            }
            else if(randomNum < _itemTableDic[GachaItemType.Star4Ship]._percentage)
            {
                Debug.Log("4��!");
                dropItemList.Add(_itemTableDic[GachaItemType.Star4Ship].GetRandomCode());
            }
            else
            {
                Debug.Log("����");
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
                    Debug.Log("�̹� ������ �ִ� �Լ�.");
                    //�ߺ� �Լ� ȹ�� ó��
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
    /// �� �Լ��� ����� �� ����ϴ� ������
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
        if(equipData._equipedShipKey >= 0)//�����Ϸ��� �������� �̹� �Լ��� �����Ǿ� ���� ���
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
                    Debug.Log("���뷮�� �ʰ��ϰų� ��� �� �̻� ������ �� �����ϴ�");
                    return false;
                }
        }        
    }
    public void Unequip(string userEquipKey)
    {
        if (userEquipKey == null || userEquipKey == string.Empty)
        {
            //Debug.LogError("���� ������ ����� Ű�� �����ϴ�.");
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
        int slotAvailability = GetSlotAvailability();

        UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(newkey);
        EquipTable table = JsonDataManager.DataLode_EquipTable(data._equipTableKey);
        int itemOccupiedSlot = table._slotUsage;

        return (slotAvailability >= itemOccupiedSlot && _combatSlotItemKeyList.Count < 5);
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
    /// ���� ����ϰ� �ִ� ��� ��� string Ű ����Ʈ ���·� ��ȯ
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
    /// ���ο� ��� ����� �� ����ϴ� ������
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
    /// ���� ���縦 �����ϱ� ���� ������
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
    /// ��� ���� ��ȭ �޼���
    /// </summary>
    /// <param name="plusLevel"></param>
    /// <param name="msg"></param>
    public void LevelUp(int plusLevel)
    {
        if(_level + plusLevel > 20 || plusLevel <= 0)
        {
            Debug.Log("�߸��� ��ȭ ��ġ");
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
            Debug.Log("��ȭ ����");
        }
    }
    /// <summary>
    /// ��� ��Ƽ������ �޼���
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

        Debug.Log("��Ƽ������ ����");
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
    public void AllDicItemUpdate_EquipedShipKey()
    {
        UserHaveShipDataList shipDataList = JsonDataManager.jsonCache.UserHaveShipDataListCache;

        foreach (UserHaveShipData shipData in shipDataList.list)
        {
            foreach (string equipedKey in shipData.GetAllEquipedItemKey())
            {
                _dic[equipedKey]._equipedShipKey = shipData._shipTablekey;
            }
        }
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
    [JsonProperty] public int CurPrmStage { get; private set; }
    [JsonProperty] public int CurSecStage { get; private set; }

    [JsonConstructor]
    public UserData(long credit, int superCredit, int fuel, int curPrmStage, int curSecStage)
    {
        Credit = credit;
        SuperCredit = superCredit;
        Fuel = fuel;
        CurPrmStage = curPrmStage;
        CurSecStage = curSecStage;
    }
    public UserData()
    {
        Credit = 0;
        SuperCredit = 0;
        Fuel = 240;
        CurPrmStage = 1;
        CurSecStage = 1;
    }    
    public static string FilePath()
    {
        return "/Resources/DataBase/UserData/UserData.json";
    }

    /// <summary>
    /// ũ���� ȹ�� �޼���
    /// </summary>
    /// <param name="value"></param>
    public void AddCredit(long value)
    {
        Credit += value;
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
    }
    /// <summary>
    /// ũ���� ��� �޼���. ����ϸ� ��� �� true ��ȯ, �ƴϸ� false ��ȯ
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryUseCredit(long value)
    {
        if(Credit < value)
        {
            return false;
        }
        else
        {
            Credit -= value;
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
            return true;
        }
    }
    /// <summary>
    /// ����ũ���� ȹ�� �޼���
    /// </summary>
    /// <param name="value"></param>
    public void AddSuperCredit(int value)
    {
        SuperCredit += value;
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
    }
    /// <summary>
    /// ���� ũ���� ��� �޼���. ����ϸ� ��� �� true ��ȯ, �ƴϸ� false ��ȯ
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryUseSuperCredit(int value)
    {
        if (SuperCredit < value)
        {
            return false;
        }
        else
        {
            SuperCredit -= value;
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
            return true;
        }
    }
    /// <summary>
    /// ������ �ڿ� ���� �޼���. 6�п� 1ȸ�� ȣ���ؾ� ��.
    /// </summary>
    /// <returns></returns>
    public bool TryAddFuel_Nature()
    {
        if(Fuel >= 240)
        {
            return false;
        }
        else
        {
            Fuel++;
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
            return true;
        }
    }
    /// <summary>
    /// ������ ��뿡 ���� ���� ���� �޼���
    /// </summary>
    /// <param name="value"></param>
    public void AddFuel_OnUseItem(int value)
    {
        Fuel += value;
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
    }
    /// <summary>
    /// ���� ��� �õ� �޼���. ���ᰡ ����ϸ� ���Ḧ �Ҹ��ϰ� true ��ȯ. �ƴϸ� false ��ȯ
    /// </summary>
    /// <param name="value"></param>
    public bool TryUseFuel(int value)
    {
        if (Fuel < value)
        {
            return false;
        }
        else
        {
            Fuel -= value;
            JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
            return true;
        }
    }

    /// <summary>
    /// �������� ���� �޼���. ���� ���������� 10�� �Ѿ ���, 1�� ���� �� ���� ���������� 1 ����.
    /// </summary>
    public void StageUp()
    {
        CurSecStage++;

        if(CurSecStage >= 11)
        {
            CurSecStage = 1;
            CurPrmStage++;
        }
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
    }
    /// <summary>
    /// �������� ���� �޼���. ���� ������������ �й����� ��쿡�� ȣ��.
    /// </summary>
    public void StageDown_OnBossStageDefeat()
    {
        CurPrmStage = 9;
        JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.UserDataCache, UserData.FilePath());
    }

    /// <summary>
    /// �������� �¼� ��ȯ. ���������� ���.
    /// </summary>
    /// <returns></returns>
    public float GetValue_StageState()
    {
        float value = (CurPrmStage + CurSecStage * 0.05f) * ((100 + CurPrmStage) / 100);
        Debug.Log($"���� �¼� : {value}");
        return value;
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
        //EquipManager.RandomEquipDrop(SetType.Alpha, 20);
        //EquipManager.RandomEquipDrop(SetType.Beta, 20);
        //EquipManager.RandomEquipDrop(SetType.Gamma, 40);
        //EquipManager.RandomEquipDrop(SetType.Delta, 20);

        //foreach (EquipSetTable Table in JsonDataManager.jsonCache.EquipSetTableListCache.list)
        //{
        //    JsonDataManager.jsonCache.BuffTableDictionaryCache._dic.Add(Table._set1Key, new BuffTable(Table._set1Key, "���� ����", new List<float>() { 18 }));
        //    JsonDataManager.jsonCache.BuffTableDictionaryCache._dic.Add(Table._set2Key, new BuffTable(Table._set2Key, "���� ����", new List<float>() { 18 }));
        //    JsonDataManager.jsonCache.BuffTableDictionaryCache._dic.Add(Table._set4Key, new BuffTable(Table._set4Key, "���� ����", new List<float>() { 18 }));
        //}

        //JsonDataManager.DataSaveCommand(JsonDataManager.jsonCache.BuffTableDictionaryCache, BuffTableDictionary.FilePath());
    }

    void DataCreate()//Dictionary �����͸� json���� �����ϴ� �Լ�
    {
        string folderPath = Application.dataPath + saveFolderPath;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        string data = JsonConvert.SerializeObject(saveData, Formatting.Indented);

        File.WriteAllText(Application.dataPath + saveDataFileName, data);

        Debug.Log("������ ���� �� ���� �Ϸ�");
    }
}