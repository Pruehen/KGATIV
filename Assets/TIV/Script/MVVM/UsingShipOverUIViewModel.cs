using System.Collections.Generic;
using System.ComponentModel;

public class UsingShipOverUIViewModel
{
    ShipMaster _targetShipMaster;
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
            if(_targetShipMaster == value) return;
            _targetShipMaster = value;

            WeaponList = _targetShipMaster.FCS.UsingWeaponList();
            WeaponCount = WeaponList.Count;

            OnPropertyChanged(nameof(TargetShipMaster));
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
            if(_usingWeaponList == value) return;
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
            if (_isViewData == value) return;
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
            if (_weaponCount == value) return;
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
