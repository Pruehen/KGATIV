using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionResultPopupUI : MonoBehaviour
{    
    [SerializeField] Button Btn_Exit;
    [SerializeField] TextMeshProUGUI Text_AddedCredit;
    [SerializeField] List<GameObject> GameObjectList_DropItemIcon;

    private void Awake()
    {
        Btn_Exit.onClick.AddListener(OnClick_Exit);        
    }

    Action _onClick_ExitBtn;
    public void SetPopUpUI(Action callBack, int addedCredit, List<string> addedItemKey)
    {
        this.gameObject.SetActive(true);
        Text_AddedCredit.text = $"+ {addedCredit}";
        _onClick_ExitBtn = callBack;        
    }

    void OnClick_Exit()
    {
        _onClick_ExitBtn?.Invoke();

        _onClick_ExitBtn = null;
        this.gameObject.SetActive(false);
    }
}
