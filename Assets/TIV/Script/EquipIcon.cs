using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipIcon : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] GameObject IsEquipedLabel;
    [SerializeField] Image Image;

    bool _isEquiedItem = false;
    private void Awake()
    {
        _isEquiedItem = false;
        IsEquipedLabel.SetActive(false);
    }

    public void SetListener(UnityAction call)
    {
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(call);
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
}
