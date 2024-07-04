using System.Collections.Generic;
using System.ComponentModel;

public class UsingShipOverUIViewModel
{
    ShipMaster _targetShipMaster;
    float _hpRatio;
    List<Weapon> _usingWeaponList;
    bool _isViewData;
    float _combatPower;

    public ShipMaster TargetShipMaster
    {
        get 
        {
            return _targetShipMaster; 
        }
        set
        {            
            _targetShipMaster = value;

            IsViewData = false;
            _hpRatio = 1;
            WeaponList = _targetShipMaster.FCS.UsingWeaponList();            

            OnPropertyChanged(nameof(TargetShipMaster));
        }
    }
    public float HPRatio
    {
        get
        {
            return _hpRatio;
        }
        set
        {
            _hpRatio = value;
            OnPropertyChanged(nameof(HPRatio));
        }
    }
    public List<Weapon> WeaponList
    {
        get
        {
            return _usingWeaponList;
        }
        set
        {
            _usingWeaponList = value;
            OnPropertyChanged(nameof(WeaponList));
        }
    }
    public bool IsViewData
    {
        get
        {
            return _isViewData;
        }
        set
        {
            _isViewData = value;
            OnPropertyChanged(nameof(IsViewData));            
        }
    }

    public float CombatPower
    {
        get
        {
            return _combatPower;
        }
        set
        {
            if (_combatPower == value) return;
            _combatPower = value;
            OnPropertyChanged(nameof(CombatPower));
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
