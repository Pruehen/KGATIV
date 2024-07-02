using System.ComponentModel;

public class NavMissionUIManagerViewModel
{
    int _curPrmStage;
    int _curSecStage;
    bool _canGiveUp;
    bool _canRetry;

    public int CurPrmStage
    {
        get { return _curPrmStage; } 
        set
        {
            if (_curPrmStage == value) return;
            _curPrmStage = value;
            OnPropertyChanged(nameof(CurPrmStage));
        }
    }
    public int CurSecStage
    {
        get { return _curSecStage; }
        set
        {
            if (_curSecStage == value) return;
            _curSecStage = value;
            OnPropertyChanged(nameof(CurSecStage));
        }
    }
    public bool CanGiveUp
    {
        get { return _canGiveUp; }
        set
        {            
            _canGiveUp = value;
            OnPropertyChanged(nameof(CanGiveUp));
        }
    }
    public bool CanRetry
    {
        get { return _canRetry; }
        set
        {
            _canRetry = value;
            OnPropertyChanged(nameof(CanRetry));
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
