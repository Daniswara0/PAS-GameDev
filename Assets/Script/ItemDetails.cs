using UnityEngine;

// Kategori item untuk memisahkan jenisnya
public enum ItemType
{
    Consumable,
    Material,
    QuestItem
}

[System.Serializable]
public class ItemDetails
{
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int quantity = 1;
    [SerializeField] private Sprite itemIcon;
    [SerializeField, TextArea] private string description;

    public string ItemID => itemID;
    public string ItemName => itemName;
    public ItemType ItemType => itemType;
    public int Quantity => quantity;
    public Sprite ItemIcon => itemIcon;
    public string Description => description;

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
}