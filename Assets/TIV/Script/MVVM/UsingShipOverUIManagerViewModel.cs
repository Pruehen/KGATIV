using System.ComponentModel;

public class UsingShipOverUIManagerViewModel
{
    ShipMaster _changedShipMaster;
    bool _isAdded;

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

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)//값이 변경되었을 때 이벤트를 발생시키기 위한 용도 (데이터 바인딩)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
