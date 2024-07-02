using EnumTypes;
using System.ComponentModel;
using UnityEngine;

public class DmgViewOverUIManagerViewModel
{
    int _newDmg;
    WeaponProjectileType _type;
    Vector3 _position;
    bool _isCrit;

    public int NewDmg
    {
        get 
        {
            return _newDmg; 
        }
        set
        {
            _newDmg = value;
            OnPropertyChanged(nameof(NewDmg));
        }
    }

    public WeaponProjectileType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
    public Vector3 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

    public bool IsCrit
    {
        get
        {
            return _isCrit;
        }
        set
        {
            _isCrit = value;
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
