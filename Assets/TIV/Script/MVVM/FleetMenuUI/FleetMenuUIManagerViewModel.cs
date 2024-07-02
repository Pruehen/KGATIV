using System.ComponentModel;
public class FleetMenuUIManagerViewModel
{
    int _maxFleetCost;
    int _usingFleetCost;
    long _upgradeCost;
    public int MaxFleetCost
    {
        get { return _maxFleetCost; }
        set
        {
            if (_maxFleetCost == value) return;
            _maxFleetCost = value;
            OnPropertyChanged(nameof(MaxFleetCost));
        }
    }
    public int UsingFleetCost
    {
        get { return _usingFleetCost; }
        set
        {
            if (_usingFleetCost == value) return;
            _usingFleetCost = value;
            OnPropertyChanged(nameof(UsingFleetCost));
        }
    }
    public long UpgradeCost
    {
        get { return _upgradeCost; }
        set
        {
            if (_upgradeCost == value) return;
            _upgradeCost = value;
            OnPropertyChanged(nameof(UpgradeCost));
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
