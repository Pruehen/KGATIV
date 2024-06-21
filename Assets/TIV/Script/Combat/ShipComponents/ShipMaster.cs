using EnumTypes;
using UnityEngine;

public interface ITargetable
{
    public Vector3 GetPosition();
    public Vector3 GetVelocity();
    /// <summary>
    /// 타게팅 가능하면 false 반환, 아군이면 true 반환
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IFF(bool id);
    public bool GetID();
    public void Hit(float dmg, WeaponProjectileType type);
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
    public bool IFF(bool id)
    {
        bool thisId = GetID();
        return thisId == id;
    }
    public bool GetID()
    {
        return (CombatData.GetShipTableKey() >= 0);
    }
    public void Hit(float dmg, WeaponProjectileType type)
    {
        CombatData.Hit(dmg, type);
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
        Debug.Log("사망 처리");
        Debug.Log("폭발 이펙트 실행");
        Destroy(this.gameObject);
    }
}
