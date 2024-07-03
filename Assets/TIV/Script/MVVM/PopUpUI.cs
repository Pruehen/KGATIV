using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP_Msg;
    [SerializeField] Button Btn_UIBtn;
    float _activeTime = 0;

    public void SetWarningPopUp(string msg, float activeTime)
    {
        TMP_Msg.text = msg;
        _activeTime = activeTime;
        this.gameObject.SetActive(true);
    }

    void Awake()
    {
        Btn_UIBtn.onClick.AddListener(ClosePopUp);
    }
    void Update()
    {
        _activeTime -= Time.deltaTime;
        if (_activeTime <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void ClosePopUp()
    {
        _activeTime = 0;
    }
}
