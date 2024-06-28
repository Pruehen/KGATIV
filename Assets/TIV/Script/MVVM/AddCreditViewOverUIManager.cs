using System.Collections.Generic;
using UnityEngine;

public class AddCreditViewOverUIManager : SceneSingleton<AddCreditViewOverUIManager>
{
    [SerializeField] GameObject Prefab_Text_AddCreditView;
    [SerializeField] List<RectTransform> TransformList_CreditViewGenerate;

    public void OnAddCredit(int addCredit)
    {
        if (TransformList_CreditViewGenerate != null || TransformList_CreditViewGenerate.Count > 0)
        {
            foreach (RectTransform parent in TransformList_CreditViewGenerate)
            {
                AddCreditViewOverUI ui = ObjectPoolManager.Instance.DequeueObject(Prefab_Text_AddCreditView).GetComponent<AddCreditViewOverUI>();
                ui.Init(addCredit, 2, parent);
            }
        }
    }
}
