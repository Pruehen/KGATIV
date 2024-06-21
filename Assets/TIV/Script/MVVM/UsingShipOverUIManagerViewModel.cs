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

    protected virtual void OnPropertyChanged(string propertyName)//값이 변경되었을 때 이벤트를 발생시키기 위한 용도 (데이터 바인딩)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
