using System.ComponentModel;

public class UserDataViewModel
{
    long _credit;
    int _superCredit;
    int _fuel;
    int _reFuelRemaning;
    int _curPrmStage;
    int _curSecStage;

    public long Credit
    {
        get { return _credit; }
        set
        {
            if (_credit == value) return;
            _credit = value;
            OnPropertyChanged(nameof(Credit));
        }
    }
    public int SuperCredit
    {
        get { return _superCredit; }
        set
        {
            if (_superCredit == value) return;
            _superCredit = value;
            OnPropertyChanged(nameof(SuperCredit));
        }
    }
    public int Fuel
    {
        get { return _fuel; }
        set
        {
            if (_fuel == value) return;
            _fuel = value;
            OnPropertyChanged(nameof(Fuel));
        }
    }
    public int ReFuelRemaning
    {
        get { return _reFuelRemaning; }
        set
        {
            if (_reFuelRemaning == value) return;
            _reFuelRemaning = value;
            OnPropertyChanged(nameof(ReFuelRemaning));
        }
    }

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

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)//값이 변경되었을 때 이벤트를 발생시키기 위한 용도 (데이터 바인딩)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
