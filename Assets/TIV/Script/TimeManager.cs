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
        // Start에서 현재 시간을 초기화합니다.
        _previousTime = DateTime.Now;
    }

    void Update()
    {
        _currentTime = DateTime.Now;

        // 현재 시간이 이전 시간보다 1초 이상 지났는지 확인합니다.
        if ((_currentTime - _previousTime).TotalSeconds >= 1)
        {
            // 이전 시간을 현재 시간으로 갱신합니다.
            _previousTime = _currentTime;

            // 특정 메서드를 호출합니다.            
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
