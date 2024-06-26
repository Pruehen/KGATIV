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

    protected virtual void OnPropertyChanged(string propertyName)//���� ����Ǿ��� �� �̺�Ʈ�� �߻���Ű�� ���� �뵵 (������ ���ε�)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
