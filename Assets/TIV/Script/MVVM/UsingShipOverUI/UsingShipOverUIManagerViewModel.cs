using System.ComponentModel;

public class UsingShipOverUIManagerViewModel
{
    ShipMaster _changedShipMaster;
    bool _isAdded;

    bool _isActiveInfoView;
    float _hp = 1;
    float _maxHp = 1;
    float _atk = 1;
    float _def = 1;
    float _critRate = 1;
    float _critDmg = 1;
    float _physicsDmg = 1;
    float _opticsDmg = 1;
    float _particleDmg = 1;
    float _plasmaDmg = 1;
    ShipBuffManager _buffManagerRef;

    public ShipMaster ChangedShipMaster
    {
        get 
        {
            return _changedShipMaster; 
        }
        set
        {
            _changedShipMaster = value;
            OnPropertyChanged(nameof(ChangedShipMaster));
        }
    }
    public bool IsAdded
    {
        get
        {
            return _isAdded;
        }
        set
        {
            _isAdded = value;
            OnPropertyChanged(nameof(IsAdded));
        }
    }
    public bool IsActiveInfoView
    {
        get { return _isActiveInfoView; }
        set
        {
            _isActiveInfoView = value;
            OnPropertyChanged(nameof(IsActiveInfoView));
        }
    }
    public float Hp
    {
        get { return  _hp; }
        set
        {
            if (_hp == value) return;
            _hp = value;
            OnPropertyChanged(nameof(Hp));
        }
    }
    public float MaxHp
    {
        get { return _maxHp; }
        set
        {
            if (_maxHp == value) return;
            _maxHp = value;
            OnPropertyChanged(nameof(MaxHp));
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

    public ShipBuffManager BuffManagerRef
    {
        get { return _buffManagerRef; }
        set
        {
            _buffManagerRef = value;
            OnPropertyChanged(nameof(BuffManagerRef));
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
