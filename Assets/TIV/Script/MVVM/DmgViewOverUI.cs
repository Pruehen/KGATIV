using EnumTypes;
using TMPro;
using UnityEngine;
using UI.Extension;

public class DmgViewOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP_DmgText;

    RectTransform _rectTransform;
    float _activeTime;
    float _lifeTime;
    Vector3 _originPos;



    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(int dmg, WeaponProjectileType type, float lifeTime, Vector3 originPos, bool isCrit)
    {
        _activeTime = 0;
        _lifeTime = lifeTime;
        _originPos = originPos;

        TMP_DmgText.text = $"{dmg}";
        switch (type)
        {
            case WeaponProjectileType.Physics:
                TMP_DmgText.color = Color.yellow;
                break;
            case WeaponProjectileType.Optics:
                TMP_DmgText.color = Color.green;
                break;
            case WeaponProjectileType.Particle:
                TMP_DmgText.color = Color.white;
                break;
            case WeaponProjectileType.Plasma:
                TMP_DmgText.color = Color.cyan;
                break;
            default:
                break;
        }

        if(isCrit)
        {
            TMP_DmgText.fontSize = 120;
        }
        else
        {
            TMP_DmgText.fontSize = 60;
        }
    }

    private void Update()
    {        
        _rectTransform.SetUIPos_WorldToScreenPos(_originPos);
        _rectTransform.anchoredPosition += new Vector2(0, _activeTime * 40);
        TMP_DmgText.fontSize = Mathf.Lerp(TMP_DmgText.fontSize, 40, Time.deltaTime * 5);

        _activeTime += Time.deltaTime;
        if(_activeTime > _lifeTime )
        {
            ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
        }
    }

    //public void SetTargetObject(ShipMaster target)
    //{
    //    targetObject = target;

    //    if (target == null)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        this.gameObject.SetActive(true);
    //    }
    //}
}
