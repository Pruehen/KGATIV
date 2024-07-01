using System.ComponentModel;

public class ShipControllerViewModel
{
    ShipMaster _changedShipMaster;

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

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)//���� ����Ǿ��� �� �̺�Ʈ�� �߻���Ű�� ���� �뵵 (������ ���ε�)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
