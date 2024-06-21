using UnityEngine;

public class Weapon : MonoBehaviour
{
    WeaponSkillTable table;

    ITargetable _target;    
    float _collDownValue;
    public void Init(WeaponSkillTable table)
    {
        this.table = table;        
        SetCollDownValue();
    }

    void SetCollDownValue()
    {
        _collDownValue = table._collTime * Random.Range(0.95f, 1.05f);
    }
    public void SetTarget(ITargetable target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        _collDownValue -= Time.deltaTime;

        if (_collDownValue < 0)
        {
            SetCollDownValue();
            Fire();
        }
    }

    void Fire()
    {

    }
}
