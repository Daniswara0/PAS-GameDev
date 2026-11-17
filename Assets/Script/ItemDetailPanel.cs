using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemQtyText;
    [SerializeField] private TMP_Text itemDescriptionText;

    public void Show(ItemDetails item, int quantity)
    {
        if (item == null)
        {
            Clear();
            return;
        }

        // Pengaman tambahan untuk mencegah error jika teks/gambar belum di-assign di Inspector
        if (itemImage != null)
        {
            itemImage.sprite = item.ItemIcon;
            itemImage.enabled = item.ItemIcon != null; // Otomatis sembunyikan gambar jika ikon kosong
        }

        if (itemNameText != null) itemNameText.text = item.ItemName;  
        if (itemQtyText != null) itemQtyText.text = "Quantity: " + quantity; 
        if (itemDescriptionText != null) itemDescriptionText.text = item.Description; 
    }

    public void Clear()
    {
        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;  
        }

        if (itemNameText != null) itemNameText.text = ""; 
        if (itemQtyText != null) itemQtyText.text = "";  
        if (itemDescriptionText != null) itemDescriptionText.text = ""; 
    }
}