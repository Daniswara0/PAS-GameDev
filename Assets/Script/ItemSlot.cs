using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 99;
    [SerializeField] public GameObject SelectedPanel;
    public bool isItemSelected;
    private InventoryManager inventoryManager;

    public ItemDetails ItemDetails => itemDetails;
    private ItemDetails itemDetails;

    public void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void AddItem(ItemDetails item)
    {
        itemDetails = item;
        itemName = item.ItemName;
        itemIcon = item.ItemIcon;
        AddQuantity(item.Quantity);
    }

    public bool isEmpty => itemQuantity <= 0;

    // Diperbarui agar mengecek ID item yang sama persis
    public bool CanStackWith(ItemDetails otherItem)
    {
        return !isFull && itemDetails != null && itemDetails.ItemID == otherItem.ItemID;
    }

    public void AddQuantity(int amount)
    {
        itemQuantity += amount;

        if (itemQuantity > maxStackSize)
        {
            itemQuantity = maxStackSize;
        }

        isFull = itemQuantity >= maxStackSize;

        quantityText.text = itemQuantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick(); 
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick(); 
        }
    }

    public void RightClick()
    {
        Debug.Log("Right Click!");
    }

    public void LeftClick()
    {
        Debug.Log("Left Click!");

        if (isEmpty || inventoryManager == null)
        {
            return;
        }
        
        inventoryManager.SelectSlot(this);
    }
}