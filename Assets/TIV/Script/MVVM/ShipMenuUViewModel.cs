using System.Collections.Generic;
using System.ComponentModel;

public class ShipMenuUIViewModel
{
    string _name;
    int _class;
    int _star;
    float _hp;
    float _atk;
    float _def;
    float _critRate;
    float _critDmg;
    float _physicsDmg;
    float _opticsDmg;
    float _particleDmg;
    float _plasmaDmg;
    int _slotCount;
    List<string> _equipedCombatKeyList;
    string _equipedEngineKey;
    string _equipedReactorKey;
    string _equipedRadiatorKey;

    long _credit;
    int _superCredit;

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
    public int Class
    {
        get { return _class; }
        set
        {
            _class = value;
            OnPropertyChanged(nameof(Class));
        }
    }
    public int Star
    {
        get { return _star; }
        set
        {
            _star = value;
            OnPropertyChanged(nameof(Star));
        }
    }
    public float Hp
    {
        get { return _hp; }
        set
        {
            if (_hp == value) return;
            _hp = value;
            OnPropertyChanged(nameof(Hp));
        }
    }
    public float Atk
    {
        get { return _atk; }
        set
        {
            if (_atk == value) return;
            _atk = value;
            OnPropertyChanged(nameof(Atk));
        }
    }
    public float Def
    {
        get { return _def; }
        set
        {
            if (_def == value) return;
            _def = value;
            OnPropertyChanged(nameof(Def));
        }
    }
    public float CritRate
    {
        get { return _critRate; }
        set
        {
            _critRate = value;
            OnPropertyChanged(nameof(CritRate));
        }
    }
    public float CritDmg
    {
        get { return _critDmg; }
        set
        {
            _critDmg = value;
            OnPropertyChanged(nameof(CritDmg));
        }
    }
    public float PhysicsDmg
    {
        get { return _physicsDmg; }
        set
        {
            _physicsDmg = value;
            OnPropertyChanged(nameof(PhysicsDmg));
        }
    }
    public float OpticsDmg
    {
        get { return _opticsDmg; }
        set
        {
            _opticsDmg = value;
            OnPropertyChanged(nameof(OpticsDmg));
        }
    }
    public float ParticleDmg
    {
        get { return _particleDmg; }
        set
        {
            _particleDmg = value;
            OnPropertyChanged(nameof(ParticleDmg));
        }
    }
    public float PlasmaDmg
    {
        get { return _plasmaDmg; }
        set
        {
            _plasmaDmg = value;
            OnPropertyChanged(nameof(PlasmaDmg));
        }
    }
    public int SlotCount
    {
        get { return _slotCount; }
        set
        {
            _slotCount = value;
            OnPropertyChanged(nameof(SlotCount));
        }
    }
    public List<string> EquipedCombatKeyList
    {
        get { return _equipedCombatKeyList; }
        set
        {            
            _equipedCombatKeyList = value;
            OnPropertyChanged(nameof(EquipedCombatKeyList));            
        }
    }
    public string EquipedEngineKey
    {
        get { return _equipedEngineKey; }
        set
        {
            _equipedEngineKey = value;
            OnPropertyChanged(nameof(EquipedEngineKey));
        }
    }
    public string EquipedReactorKey
    {
        get { return _equipedReactorKey; }
        set
        {
            _equipedReactorKey = value;
            OnPropertyChanged(nameof(EquipedReactorKey));
        }
    }
    public string EquipedRadiatorKey
    {
        get { return _equipedRadiatorKey; }
        set
        {
            _equipedRadiatorKey = value;
            OnPropertyChanged(nameof(EquipedRadiatorKey));
        }
    }
    public long Credit
    {
        get { return _credit; }
        set
        {
            if (_credit == value) return;
            _credit = value;
            OnPropertyChanged(nameof(Credit));
        }
    }
    public int SuperCredit
    {
        get { return _superCredit; }
        set
        {
            if (_superCredit == value) return;
            _superCredit = value;
            OnPropertyChanged(nameof(SuperCredit));
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
