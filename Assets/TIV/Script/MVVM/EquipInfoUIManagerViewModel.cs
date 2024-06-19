using System.Collections.Generic;
using System.ComponentModel;
using EnumTypes;
using static UserHaveEquipData;
public class EquipInfoUIManagerViewModel
{
    int _tableKey;
    string _name;
    EquipType _type;
    IncreaseableStateType _mainStateType;    
    int _level;

    List<EquipStateSet> _subStateList = new List<EquipStateSet>();    

    SetType _setType;

    public int TableKey
    {
        get { return _tableKey; }
        set
        {
            if (_tableKey == value) return;
            _tableKey = value;
            OnPropertyChanged(nameof(TableKey));
        }
    }
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name == value) return;
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public EquipType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }
    public IncreaseableStateType MainStateType
    {
        get { return _mainStateType; }
        set
        {
            _mainStateType = value;
            OnPropertyChanged(nameof(MainStateType));
        }
    }
    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            OnPropertyChanged(nameof(Level));
        }
    }
    public List<EquipStateSet> SubStateList
    {
        get { return _subStateList; }
        set
        {
            _subStateList = value;
            OnPropertyChanged(nameof(SubStateList));
        }
    }
    public SetType SetType
    {
        get { return _setType; }
        set
        {
            if (_setType == value) return;
            _setType = value;
            OnPropertyChanged(nameof(SetType));
        }
    }

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)//값이 변경되었을 때 이벤트를 발생시키기 위한 용도 (데이터 바인딩)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
