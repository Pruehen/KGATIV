using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipIcon : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject IsEquipedLabel;
    [SerializeField] UnityEngine.UI.Image Image;

    bool _isEquiedItem = false;
    private void Awake()
    {
        _isEquiedItem = false;
        if (IsEquipedLabel != null)
        {
            IsEquipedLabel.SetActive(false);
        }
    }

    public void AddListener(UnityAction call)
    {
        if (button != null)
        {
            button.onClick.AddListener(call);
        }
    }
    public void RemoveAllListeners()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void SetIsEquipedLabel(bool value)
    {
        if(value != _isEquiedItem)
        {
            _isEquiedItem = value;
            IsEquipedLabel.SetActive(_isEquiedItem);
        }
    }
    public void SetSprite(Sprite sprite)
    {
        Image.sprite = sprite;
    }
    public void SetSprite(int tableKey)
    {
        if(tableKey == -1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            EquipTable table = JsonDataManager.DataLode_EquipTable(tableKey);
            Sprite sprite = Resources.Load<Sprite>("Sprites/ShipBuilderIcon/Sprites/" + table._spriteName);
            if (sprite != null)
            {
                SetSprite(sprite);
            }
        }
    }
}
