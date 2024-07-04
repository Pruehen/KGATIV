using EnumTypes;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    /// <summary>
    /// �Ʊ��� ��� true, ���� ��� false ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool GetID();
    public void Hit(float dmg, WeaponProjectileType type, bool isCrit, List<string> hasDebuffKey);
    public bool IsActive();
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
    bool _isActive;

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
    public bool IsActive()
    {
        return _isActive;
    }

    public void Init(float warpTime)
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
            MainComputer.Init();

            Engine.onWarpStart += CreateDummy_OnWarpStart;
            Engine.onWarpEnd += ShipActivate_onWarpEnd;
            Engine.onWarpEnd += RemoveDummy_OnWarpEnd;
            Engine.Init(warpTime);

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

            CombatData.Register_OnDead(Destroy_OnDead);
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
    void ShipActivate_onWarpEnd()
    {
        kjh.GameLogicManager.Instance.AddActiveShip(this);
        _isActive = true;
    }

    void Destroy_OnDead()
    {
        EffectManager.Instance.ExplosionEffectGenerate(this.transform.position, GetComponent<SphereCollider>().radius);        
        UserData.Instance.AddCredit((long)(NavMissionLogicManager.Instance.GetValue_StageState() * _dropCredit));        
        kjh.GameLogicManager.Instance.RemoveActiveShip(this);
        CreateDebri();
        _onDead?.Invoke(this);
        if (spawnDummyTemp != null) Destroy(spawnDummyTemp);
        Destroy(this.gameObject);
    }
    /// <summary>
    /// �⺻ ��� ó��
    /// </summary>
    Action<ShipMaster> _onDead;
    /// <summary>
    /// �÷��̾� �Լ��� Ż��
    /// </summary>
    Action<ShipMaster> _onExit;
    /// <summary>
    /// �������� ������ ���� ����
    /// </summary>
    Action<ShipMaster> _onRemove;

    public void Register_OnDead(Action<ShipMaster> callBack)
    {
        _onDead += callBack;
    }
    public void Register_OnExit(Action<ShipMaster> callBack)
    {
        _onExit += callBack;
    }
    public void Register_OnRamove(Action<ShipMaster> callBack)
    {
        _onRemove += callBack;
    }
    /// <summary>
    /// ������!
    /// </summary>
    public void CommandExit()
    {
        kjh.GameLogicManager.Instance.RemoveActiveShip(this);
        _onExit?.Invoke(this);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �������� ���� ó���� ���� �Լ� ����
    /// </summary>
    public void CommandRemove()
    {
        kjh.GameLogicManager.Instance.RemoveActiveShip(this);
        _onRemove?.Invoke(this);
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

    GameObject spawnDummyTemp;
    void CreateDummy_OnWarpStart(Vector3 initPos, float warpTime)
    {
        if (spawnDummyTemp == null)
        {
            // 1. �� ���� ������Ʈ�� �����մϴ�.
            GameObject newGameObject = new GameObject("Dummy");

            // 2. �ڱ� �ڽ��� ���� ��ü���� �����ؼ� �� ���� ������Ʈ�� ������ �ٿ��ֽ��ϴ�.
            CopyChildrenRecursive(transform, newGameObject.transform);

            // ���� ������ ���� ������Ʈ�� ��ġ�� ���� ���� ������Ʈ�� �����ϰ� �����մϴ�.            
            ShipDummy shipDummy = newGameObject.AddComponent<ShipDummy>();
            shipDummy.Init(Material_Dummy);
            shipDummy.SetMatColor_Spawn(warpTime);
            spawnDummyTemp = newGameObject;
        }
        else
        {
            spawnDummyTemp.SetActive(true);
        }
        spawnDummyTemp.transform.position = initPos;
    }
    void RemoveDummy_OnWarpEnd()
    {
        spawnDummyTemp.SetActive(false);
    }

    void CopyChildrenRecursive(Transform source, Transform destination)
    {
        foreach (Transform child in source)
        {
            // �ڽ� ��ü�� �����ؼ� ���ο� ���� ������Ʈ�� ������ �߰��մϴ�.
            GameObject newChild = Instantiate(child.gameObject, destination);
            newChild.transform.localPosition = child.localPosition;

            // �ڽ� ��ü�� ���� ��ü�鵵 ��������� �����մϴ�.
            //CopyChildrenRecursive(child, newChild.transform);
        }
    }
}
