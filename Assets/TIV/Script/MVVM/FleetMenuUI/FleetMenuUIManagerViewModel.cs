using System.ComponentModel;
public class FleetMenuUIManagerViewModel
{
    int _maxFleetCost;
    int _usingFleetCost;

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

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)//���� ����Ǿ��� �� �̺�Ʈ�� �߻���Ű�� ���� �뵵 (������ ���ε�)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
