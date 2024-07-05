using EnumTypes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionResultPopupUI : MonoBehaviour
{    
    [SerializeField] Button Btn_Exit;
    [SerializeField] TextMeshProUGUI Text_AddedCredit;
    [SerializeField] List<EquipIcon> GameObjectList_DropItemIcon;

    private void Awake()
    {
        Btn_Exit.onClick.AddListener(OnClick_Exit);        
    }

    Action _onClick_ExitBtn;
    int _activeIconCount;
    public void SetPopUpUI(Action callBack, int addedCredit, List<string> addedItemKey)
    {
        this.gameObject.SetActive(true);
        Text_AddedCredit.text = $"+ {addedCredit}";

        for (int i = 0; i < GameObjectList_DropItemIcon.Count; i++)
        {
            GameObjectList_DropItemIcon[i].gameObject.SetActive(false);
        }

        if (addedItemKey != null)
        {
            _activeIconCount = 0;

            foreach (string key in addedItemKey)
            {
                UserHaveEquipData data = JsonDataManager.DataLode_UserHaveEquipData(key);

                EquipIcon icon = GameObjectList_DropItemIcon[_activeIconCount];
                icon.gameObject.SetActive(true);
                _activeIconCount++;

                icon.SetSprite(data._itemUniqueKey);
                icon.SetIsEquipedLabel(data._equipedShipKey >= 0);
            }
        }

        _onClick_ExitBtn = callBack;        
    }

    void OnClick_Exit()
    {
        _onClick_ExitBtn?.Invoke();

        _onClick_ExitBtn = null;
        this.gameObject.SetActive(false);
    }
}
