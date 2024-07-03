using System;
using UnityEngine;

public class TimeManager : SceneSingleton<TimeManager>
{
    DateTime _previousTime;
    DateTime _currentTime;
    Action _onSecChange;

    public void Register_onSecChange(Action callBack) { _onSecChange += callBack; }
    public void UnRegister_onSecChange(Action callBack) { _onSecChange -= callBack; }

    void Start()
    {
        // Start���� ���� �ð��� �ʱ�ȭ�մϴ�.
        _previousTime = DateTime.Now;
    }

    void Update()
    {
        _currentTime = DateTime.Now;

        // ���� �ð��� ���� �ð����� 1�� �̻� �������� Ȯ���մϴ�.
        if ((_currentTime - _previousTime).TotalSeconds >= 1)
        {
            // ���� �ð��� ���� �ð����� �����մϴ�.
            _previousTime = _currentTime;

            // Ư�� �޼��带 ȣ���մϴ�.            
            _onSecChange?.Invoke();
        }
    }

    public DateTime GetTime()
    {
        return _currentTime;
    }

    public static int GetDeltaTime(DateTime preTime, DateTime curTime)
    {
        return (int)(curTime - preTime).TotalSeconds;
    }
}
