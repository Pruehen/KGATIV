using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopUpUI : MonoBehaviour
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

    private void Awake()
    {
        Btn_UIBtn.onClick.AddListener(ClosePopUp);
    }
    private void Update()
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
