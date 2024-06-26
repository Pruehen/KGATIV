using System.Collections.Generic;
using System.ComponentModel;

public class UsingShipOverUIViewModel
{
    ShipMaster _targetShipMaster;
    float _hpRatio;
    List<Weapon> _usingWeaponList;
    int _weaponCount;
    bool _isViewData;

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
            WeaponCount = WeaponList.Count;

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

    public int WeaponCount
    {
        get
        {
            return _weaponCount;
        }
        set
        {
            _weaponCount = value;
            OnPropertyChanged(nameof(WeaponCount));
        }
    }

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)//���� ����Ǿ��� �� �̺�Ʈ�� �߻���Ű�� ���� �뵵 (������ ���ε�)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
