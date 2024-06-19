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
            if (_class == value) return;
            _class = value;
            OnPropertyChanged(nameof(Class));
        }
    }
    public int Star
    {
        get { return _star; }
        set
        {
            if (_star == value) return;
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
            if (_critRate == value) return;
            _critRate = value;
            OnPropertyChanged(nameof(CritRate));
        }
    }
    public float CritDmg
    {
        get { return _critDmg; }
        set
        {
            if (_critDmg == value) return;
            _critDmg = value;
            OnPropertyChanged(nameof(CritDmg));
        }
    }
    public float PhysicsDmg
    {
        get { return _physicsDmg; }
        set
        {
            if (_physicsDmg == value) return;
            _physicsDmg = value;
            OnPropertyChanged(nameof(PhysicsDmg));
        }
    }
    public float OpticsDmg
    {
        get { return _opticsDmg; }
        set
        {
            if (_opticsDmg == value) return;
            _opticsDmg = value;
            OnPropertyChanged(nameof(OpticsDmg));
        }
    }
    public float ParticleDmg
    {
        get { return _particleDmg; }
        set
        {
            if (_particleDmg == value) return;
            _particleDmg = value;
            OnPropertyChanged(nameof(ParticleDmg));
        }
    }
    public float PlasmaDmg
    {
        get { return _plasmaDmg; }
        set
        {
            if (_plasmaDmg == value) return;
            _plasmaDmg = value;
            OnPropertyChanged(nameof(PlasmaDmg));
        }
    }
    public int SlotCount
    {
        get { return _slotCount; }
        set
        {
            if (_slotCount == value) return;
            _slotCount = value;
            OnPropertyChanged(nameof(SlotCount));
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
