using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_MsgField;
    [SerializeField] Button Btn_Yes;
    [SerializeField] Button Btn_No;

    private void Awake()
    {        
        Btn_Yes.onClick.AddListener(OnClick_Yes);
        Btn_No.onClick.AddListener(OnClick_No);        
    }

    Action _onClick_YesBtn;
    public void SetPopUpUI(Action callBack, string msg)
    {
        this.gameObject.SetActive(true);
        _onClick_YesBtn = callBack;
        Text_MsgField.text = msg;
    }

    void OnClick_Yes()
    {
        _onClick_YesBtn?.Invoke();

        _onClick_YesBtn = null;
        this.gameObject.SetActive(false);
    }
    void OnClick_No()
    {
        _onClick_YesBtn = null;
        this.gameObject.SetActive(false);
    }
}
