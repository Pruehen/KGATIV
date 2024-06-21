using UnityEngine;

[RequireComponent(typeof(ShipCombatData))]
[RequireComponent(typeof(ShipMainComputer))]
[RequireComponent(typeof(ShipEngine))]
[RequireComponent(typeof(ShipFCS))]
public class ShipMaster : MonoBehaviour
{
    public ShipCombatData CombatData { get; private set; }
    public ShipMainComputer MainComputer { get; private set; }
    public ShipEngine Engine { get; private set; }
    public ShipFCS FCS { get; private set; }

    private void Awake()
    {
        CombatData = GetComponent<ShipCombatData>();        
        MainComputer = GetComponent<ShipMainComputer>();
        Engine = GetComponent<ShipEngine>();
        FCS = GetComponent<ShipFCS>();

        kjh.GameLogicManager.Instance.AddActiveShip(this);

        CombatData.Init();
        CombatData.Register_OnDead(Destroy_OnDead);
    }

    void Destroy_OnDead()
    {
        Debug.Log("»ç¸Á Ã³¸®");
        Debug.Log("Æø¹ß ÀÌÆåÆ® ½ÇÇà");
        Destroy(this.gameObject);
    }
}
