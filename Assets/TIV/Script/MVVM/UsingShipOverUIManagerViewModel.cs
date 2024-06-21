using System.Collections.Generic;
using System.ComponentModel;

public class UsingShipOverUIManagerViewModel
{
    Dictionary<int, ShipMaster> _activeShipDicRef;

    public Dictionary<int, ShipMaster> ActiveShipDic
    {
        get 
        {
            return _activeShipDicRef; 
        }
        set
        {
            _activeShipDicRef = value;
            OnPropertyChanged(nameof(ActiveShipDic));
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
