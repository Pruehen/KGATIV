using TMPro;
using UnityEngine;

public class AddCreditViewOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP_Credit;
    [SerializeField] Transform Transform_CoinImage;

    RectTransform _rectTransform;
    float _activeTime;
    float _lifeTime;    


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(int addedCredit, float lifeTime, RectTransform originTrf)
    {
        _activeTime = 0;
        _lifeTime = lifeTime;
        _rectTransform.anchoredPosition = originTrf.position;        
        this.transform.SetParent(originTrf);

        TMP_Credit.text = $" + {addedCredit}";

        TMP_Credit.fontSize = 60;
        Transform_CoinImage.rotation = Quaternion.identity;
    }

    private void Update()
    {        
        _rectTransform.anchoredPosition += new Vector2(0, Time.deltaTime * 20);
        Transform_CoinImage.Rotate(Vector3.up, Time.deltaTime * 180);
        TMP_Credit.fontSize = Mathf.Lerp(TMP_Credit.fontSize, 30, Time.deltaTime);

        _activeTime += Time.deltaTime;
        if(_activeTime > _lifeTime )
        {
            ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
        }
    }
}
