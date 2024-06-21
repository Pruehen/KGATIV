using UnityEngine;

public interface ITargetable
{
    public Vector3 GetPosition();
    public Vector3 GetVelocity();
}

[RequireComponent(typeof(ShipCombatData))]
[RequireComponent(typeof(ShipMainComputer))]
[RequireComponent(typeof(ShipEngine))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipFCS))]
public class ShipMaster : MonoBehaviour, ITargetable
{
    public Rigidbody rigidbody { get; private set; }    

    public ShipCombatData CombatData { get; private set; }
    public ShipMainComputer MainComputer { get; private set; }
    public ShipEngine Engine { get; private set; }    
    public ShipFCS FCS { get; private set; }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
    public Vector3 GetVelocity()
    {
        return rigidbody.velocity;
    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        CombatData = GetComponent<ShipCombatData>();        
        MainComputer = GetComponent<ShipMainComputer>();
        Engine = GetComponent<ShipEngine>();        
        FCS = GetComponent<ShipFCS>();

        kjh.GameLogicManager.Instance.AddActiveShip(this);

        CombatData.Init();
        CombatData.Register_OnDead(Destroy_OnDead);
        MainComputer.Init();
        Engine.Init();
        FCS.Init(CombatData.GetShipTableKey());
    }

    void Destroy_OnDead()
    {
        Debug.Log("»ç¸Á Ã³¸®");
        Debug.Log("Æø¹ß ÀÌÆåÆ® ½ÇÇà");
        Destroy(this.gameObject);
    }
}
