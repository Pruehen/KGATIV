using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using EnumTypes;

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

    public ShipTable(int key, float hp, float atk, float def, float accuracy, float evade, string name, int shipClass, int star)
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
    }
}
public class ShipTableList
{
    public List<ShipTable> list = new List<ShipTable>();
    public ShipTableList(List<ShipTable> list)
    {
        this.list = list;
    }
    public static string FilePath()
    {
        return "/TIV/DataBase/Table/Ship/ShipTable.json";
    }
}

public class EquipTable
{    
    public EquipType _type;
    public string _name;

    public EquipTable(EquipType type, string name)
    {
        _type = type;
        _name = name;
    }
}
public class EquipTableList
{
    public List<EquipTable> list = new List<EquipTable>();
    public EquipTableList(List<EquipTable> list)
    {
        this.list = list;
    }
    public static string FilePath()
    {
        return "/TIV/DataBase/Table/Equip/EquipTeble.json";
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
        int random = Random.Range(0, mainStateList.Count);
        return mainStateList[random];
    }
    public IncreaseableStateType GetRandomSubState()
    {
        int random = Random.Range(0, subStateList.Count);
        return subStateList[random];
    }
}
public class EquipType_PROListTableList
{
    public List<EquipType_PossibleReinforcementOptionsListTable> list = new List<EquipType_PossibleReinforcementOptionsListTable>();
    public EquipType_PROListTableList(List<EquipType_PossibleReinforcementOptionsListTable> list)
    {
        this.list = list;
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
    public StateType_StateMultipleTableList(List<StateType_StateMultipleTable> list)
    {
        this.list = list;
    }
    public static string FilePath()
    {
        return "/TIV/DataBase/Table/Value/StateType_StateMultipleTable.json";
    }
}


public class UserHaveShipData
{
    public int _key;
    public int _level;

    public UserHaveShipData(int key, int level)
    {
        _key = key;
        _level = level;
    }
}
public class UserHaveShipDataList
{
    public List<UserHaveShipData> list = new List<UserHaveShipData>();
    public UserHaveShipDataList(List<UserHaveShipData> list)
    {
        this.list = list;
    }
    public static string FilePath()
    {
        return "/TIV/DataBase/UserData/UserData_UseShip.json";
    }
}

public class UserHaveEquipData
{
    public EquipType _key;
    public int _level;
    public int _optimizedLevel;
    public EquipStateSet _mainState;
    public List<EquipStateSet> _subStateList;

    [JsonConstructor]
    public UserHaveEquipData(EquipType key, int level, int optimizedLevel, EquipStateSet mainState, List<EquipStateSet> subStateList)
    {
        _key = key;
        _level = level;
        _optimizedLevel = optimizedLevel;
        _mainState = mainState;
        _subStateList = subStateList;
    }
    public UserHaveEquipData(EquipType key, int level, IncreaseableStateType mainStateType)
    {
        _key = key;
        _level = level;
        _optimizedLevel = 0;
        _mainState = new EquipStateSet(mainStateType, level);
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
    /// 무기 레벨 강화 메서드
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
                IncreaseableStateType randomType = JsonDataManager.DataLode_EquipType_PROTable((int)_key).GetRandomSubState();
                int stateLevel = Random.Range(3, 6);
                _subStateList.Add(new EquipStateSet(randomType, stateLevel));
            }

            msg = $"옵티마이즈 {plusLevel}회 성공";
        }
    }
}
public class UserHaveEquipDataList
{
    public List<UserHaveEquipData> list = new List<UserHaveEquipData>();
    public UserHaveEquipDataList(List<UserHaveEquipData> list)
    {
        this.list = list;
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
}
public class UserHaveItemDataList
{
    public List<UserHaveItemData> list = new List<UserHaveItemData>();
    public UserHaveItemDataList(List<UserHaveItemData> list)
    {
        this.list = list;
    }
    public static string FilePath()
    {
        return "/TIV/DataBase/UserData/UserData_Item.json";
    }
}

public class JsonDataCreator : MonoBehaviour
{
    string saveDataFileName = UserHaveEquipDataList.FilePath();
    string saveFolderPath = "/TIV/DataBase/UserData";

    public UserHaveEquipDataList saveData;

    private void OnEnable()
    {
        DataCreate();
    }

    void SetData()
    {
        List<UserHaveEquipData> list = new List<UserHaveEquipData>();
        list.Add(EquipManager.CreateEquip(EquipType.Weapon));

        string temp = string.Empty;
        list[0].LevelUp(19, out temp);
        Debug.Log(temp);
        list[0].Optimize(5, out temp);
        Debug.Log(temp);

        saveData = new UserHaveEquipDataList(list);        
    }

    public void DataCreate()//Dictionary 데이터를 json으로 저장하는 함수
    {
        string folderPath = Application.dataPath + saveFolderPath;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        SetData();
        string data = JsonConvert.SerializeObject(saveData, Formatting.Indented);

        File.WriteAllText(Application.dataPath + saveDataFileName, data);

        Debug.Log("데이터 생성 및 저장 완료");
    }
}