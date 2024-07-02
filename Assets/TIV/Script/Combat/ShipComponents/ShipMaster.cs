using EnumTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] GameObject Prefab_Debri;
    [SerializeField] Material Material_Dummy;
    [SerializeField] bool _isDummy;
    public string ShipName { get; private set; }
    public Rigidbody rigidbody { get; private set; }    
    public ShipCombatData CombatData { get; private set; }
    public ShipMainComputer MainComputer { get; private set; }
    public ShipEngine Engine { get; private set; }    
    public ShipFCS FCS { get; private set; }
    public ShipBuffManager BuffManager { get; private set; }    

    Action InitComplite;
    /// <summary>
    /// 워프 완료 후 호출됨
    /// </summary>
    public void InitCompliteInvoke()
    {
        InitComplite?.Invoke();
    }

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
    public int GetCost()
    {
        return JsonDataManager.DataLode_ShipTable(CombatData.GetShipTableKey())._cost;
    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        CombatData = GetComponent<ShipCombatData>();
        MainComputer = GetComponent<ShipMainComputer>();
        Engine = GetComponent<ShipEngine>();
        FCS = GetComponent<ShipFCS>();
        BuffManager = GetComponent<ShipBuffManager>();

        if (_isDummy == false)
        {
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

            InitComplite += () => kjh.GameLogicManager.Instance.AddActiveShip(this);
        }
        else
        {            
            CombatData.enabled = false;
            MainComputer.enabled = false;
            Engine.enabled = false;
            FCS.enabled = false;
            BuffManager.enabled = false;
            GetComponent<SphereCollider>().enabled = false;

            CreateDummy();
        }
    }

    void Destroy_OnDead()
    {
        _onDead?.Invoke(this);
        Debug.Log("사망 처리");
        EffectManager.Instance.ExplosionEffectGenerate(this.transform.position, GetComponent<SphereCollider>().radius);
        UserData userDataTemp = JsonDataManager.DataLode_UserData();
        userDataTemp.AddCredit((long)(userDataTemp.GetValue_StageState() * _dropCredit));        
        kjh.GameLogicManager.Instance.RemoveActiveShip(this);
        CreateDebri();
        Destroy(this.gameObject);
    }

    Action<ShipMaster> _onDead;
    Action<ShipMaster> _onExit;

    public void Register_OnDead(Action<ShipMaster> callBack)
    {
        _onDead += callBack;
    }
    public void Register_OnExit(Action<ShipMaster> callBack)
    {
        _onExit += callBack;
    }

    public void CommandExit()
    {
        Exit();
    }
    void Exit()
    {        
        kjh.GameLogicManager.Instance.RemoveActiveShip(this);
        _onExit?.Invoke(this);
        Destroy(this.gameObject);
    }


    void CreateDebri()
    {
        if(Prefab_Debri != null)
        {
            GameObject debri = Instantiate(Prefab_Debri, this.transform.position, this.transform.rotation, this.transform.parent);            
            Material material = debri.GetComponent<Renderer>().material;

            while(this.transform.childCount > 0)
            {
                Transform child = this.transform.GetChild(0);
                child.SetParent(debri.transform);
                Renderer[] renderers = child.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.material = material;
                }

                Rigidbody childRb = child.AddComponent<Rigidbody>();
                childRb.useGravity = false;
                childRb.velocity = rigidbody.velocity;
                childRb.AddForce(UnityEngine.Random.onUnitSphere * 2f, ForceMode.VelocityChange);
                childRb.angularDrag = 0;
                childRb.AddTorque(UnityEngine.Random.onUnitSphere * 0.2f, ForceMode.VelocityChange);
                Destroy(child.gameObject, UnityEngine.Random.Range(5f, 8f));
            }
            
            Destroy(debri, 8);
        }
    }

    void CreateDummy()
    {
        if (Material_Dummy != null)
        {
            this.AddComponent<ShipDummy>().Init(Material_Dummy);
        }
    }
}
