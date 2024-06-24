using EnumTypes;
using UnityEngine;

public interface ITargetable
{
    public Vector3 GetPosition();
    public Vector3 GetVelocity();
    /// <summary>
    /// Ÿ���� �����ϸ� false ��ȯ, �Ʊ��̸� true ��ȯ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IFF(bool id);
    public bool GetID();
    public void Hit(float dmg, WeaponProjectileType type, bool isCrit);
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
        if(this == null || transform == null)
        {
            return Vector3.zero;
        }
        return this.transform.position;
    }
    public Vector3 GetVelocity()
    {
        if (this == null || transform == null)
        {
            return Vector3.zero;
        }
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
    public void Hit(float dmg, WeaponProjectileType type, bool isCrit)
    {
        CombatData.Hit(dmg, type, isCrit);
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
        Debug.Log("��� ó��");
        Debug.Log("���� ����Ʈ ����");
        Destroy(this.gameObject);
    }
}
