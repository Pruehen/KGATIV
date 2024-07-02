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

    protected virtual void OnPropertyChanged(string propertyName)//���� ����Ǿ��� �� �̺�Ʈ�� �߻���Ű�� ���� �뵵 (������ ���ε�)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
