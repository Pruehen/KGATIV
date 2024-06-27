using EnumTypes;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// 아군일 경우 true, 적일 경우 false 반환
    /// </summary>
    /// <returns></returns>
    public bool GetID();
    public void Hit(float dmg, WeaponProjectileType type, bool isCrit, List<string> hasDebuffKey);
}

[RequireComponent(typeof(ShipCombatData))]
[RequireComponent(typeof(ShipMainComputer))]
[RequireComponent(typeof(ShipEngine))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipFCS))]
[RequireComponent(typeof(ShipBuffManager))]
public class ShipMaster : MonoBehaviour, ITargetable
{
    [SerializeField] string _shipName;
    [SerializeField] long _dropCredit;
    public string ShipName { get; private set; }
    public Rigidbody rigidbody { get; private set; }    
    public ShipCombatData CombatData { get; private set; }
    public ShipMainComputer MainComputer { get; private set; }
    public ShipEngine Engine { get; private set; }    
    public ShipFCS FCS { get; private set; }
    public ShipBuffManager BuffManager { get; private set; }

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
    public void Hit(float dmg, WeaponProjectileType type, bool isCrit, List<string> hasDebuffKey)
    {
        CombatData.Hit(dmg, type, isCrit, hasDebuffKey);
    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        CombatData = GetComponent<ShipCombatData>();        
        MainComputer = GetComponent<ShipMainComputer>();
        Engine = GetComponent<ShipEngine>();        
        FCS = GetComponent<ShipFCS>();
        BuffManager = GetComponent<ShipBuffManager>();             

        CombatData.Init();
        CombatData.Register_OnDead(Destroy_OnDead);
        MainComputer.Init();
        Engine.Init();

        int shipKey = CombatData.GetShipTableKey();
        FCS.Init(shipKey);
        if (shipKey >= 0)
        {
            ShipName = JsonDataManager.DataLode_ShipTable(shipKey)._name;
        }
        else
        {
            ShipName = _shipName;
        }

        kjh.GameLogicManager.Instance.AddActiveShip(this);
    }

    void Destroy_OnDead()
    {
        _onDead?.Invoke(this);
        Debug.Log("사망 처리");
        Debug.Log("폭발 이펙트 실행");
        UserData userDataTemp = JsonDataManager.DataLode_UserData();
        userDataTemp.AddCredit((long)(userDataTemp.GetValue_StageState() * _dropCredit));
        kjh.GameLogicManager.Instance.RemoveActiveShip(this);
        Destroy(this.gameObject);
    }

    Action<ShipMaster> _onDead;

    public void Register_OnDead(Action<ShipMaster> callBack)
    {
        _onDead += callBack;
    }
}
