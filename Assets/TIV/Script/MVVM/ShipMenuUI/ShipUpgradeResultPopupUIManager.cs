using System.Collections;
using TMPro;
using UnityEngine;

public class ShipUpgradeResultPopupUIManager : MonoBehaviour
{
    [Header("함선 강화 정보 필드")]
    [SerializeField] TextMeshProUGUI TMP_DeltaHp;
    [SerializeField] TextMeshProUGUI TMP_DeltaAtk;
    [SerializeField] TextMeshProUGUI TMP_DeltaDef;

    public void ViewResult_WdwPopUp(ShipTable table, int beforeLevel, int affterLevel, float activeTime = 2)
    {
        this.gameObject.SetActive(true);
        StopAllCoroutines();

        float beforeHp = table.GetHp(beforeLevel);
        float beforeAtk = table.GetAtk(beforeLevel);
        float beforeDef = table.GetDef(beforeLevel);

        float affterHp = table.GetHp(affterLevel);
        float affterAtk = table.GetAtk(affterLevel);
        float affterDef = table.GetDef(affterLevel);

        TMP_DeltaHp.text = $"{affterHp - beforeHp:F0}";
        TMP_DeltaAtk.text = $"{affterAtk - beforeAtk:F0}";
        TMP_DeltaDef.text = $"{affterDef - beforeDef:F0}";

        StartCoroutine(CloseWdw(activeTime));
    }

    IEnumerator CloseWdw(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        this.gameObject.SetActive(false);
    }
}
