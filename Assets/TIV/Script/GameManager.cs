using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
    void Start()
    {
        UserData.Instance.AddFuel_ProportionalToUnconnectedTime();
    }
}
