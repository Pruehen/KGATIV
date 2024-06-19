using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipIcon : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] GameObject IsEquipedLabel;
    [SerializeField] UnityEngine.UI.Image Image;

    bool _isEquiedItem = false;
    private void Awake()
    {
        _isEquiedItem = false;
        IsEquipedLabel.SetActive(false);
    }

    public void AddListener(UnityAction call)
    {        
        Button.onClick.AddListener(call);
    }
    public void RemoveAllListeners()
    {
        Button.onClick.RemoveAllListeners();
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
        EquipTable table = JsonDataManager.DataLode_EquipTable(tableKey);
        Sprite sprite = Resources.Load<Sprite>("Sprites/ShipBuilderIcon/Sprites/" + table._spriteName);
        if (sprite != null)
        {
            SetSprite(sprite);
        }
    }
}
