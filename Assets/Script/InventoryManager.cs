using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    [Header("Input Settings")]
    [SerializeField] private InputActionReference inventoryActionRef; // Tarik Input Action kamu ke sini di Inspector

    [Header("UI References")]
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;
    [SerializeField] public ItemDetailPanel detailsPanel;

    private bool InventoryActive;
    public ItemSlot[] itemSlot;

    void OnEnable()
    {
        if (inventoryActionRef != null && inventoryActionRef.action != null)
        {
            inventoryActionRef.action.Enable();
        }
    }

    void OnDisable()
    {
        if (inventoryActionRef != null && inventoryActionRef.action != null)
        {
            inventoryActionRef.action.Disable();
        }
    }

    void Start()
    {
        CreateSlots();

        if (InventoryCanvas != null)
        {
            InventoryCanvas.SetActive(false);
        }

        InventoryActive = false;

        if (detailsPanel == null)
        {
            Debug.LogWarning("Details Panel not assigned in InventoryManager!", this);
        }
    }

    void Update()
    {
        // Memeriksa apakah tombol inventory ditekan melalui InputActionReference
        if (inventoryActionRef != null && inventoryActionRef.action != null && inventoryActionRef.action.triggered)
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        InventoryActive = !InventoryActive;

        if (InventoryActive)
        {
            Time.timeScale = 0f; // Pause game saat inventory dibuka
            Debug.Log("Inventory is active");
            if (InventoryCanvas != null) InventoryCanvas.SetActive(true);
            ShowFirstItem();
        }
        else
        {
            Time.timeScale = 1f; // Lanjut game saat inventory ditutup
            Debug.Log("Inventory is deactivated");
            if (InventoryCanvas != null) InventoryCanvas.SetActive(false);
        }
    }

    // Build slot objek secara otomatis saat runtime tanpa pasang manual di scene
    private void CreateSlots()
    {
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");
        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found in a Resources folder.", this);
            itemSlot = new ItemSlot[0];
            return;
        }

        Transform parent = InventoryCanvas.transform;
        GridLayoutGroup grid = InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);
        if (grid != null)
        {
            parent = grid.transform;
        }

        itemSlot = new ItemSlot[slotCount];
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, parent);
        }
    }

    // Fungsi kontrak dari Interface GrabableItem saat player mengambil item di lantai
    public void AddItem(ItemDetails item)
    {
        Debug.Log("Item added to inventory: " + item.ItemName + " Quantity: " + item.Quantity);

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Inventory has no slots, '" + item.ItemName + "' was lost.");
            return;
        }

        // 1. Cek apakah ada slot yang sama yang bisa ditumpuk (Stacking berdasarkan ID)
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].CanStackWith(item))
            {
                itemSlot[i].AddQuantity(item.Quantity);
                return;
            }
        }

        // 2. Jika tidak ada yang bisa ditumpuk, masukkan ke slot kosong pertama
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isEmpty)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.LogWarning("Inventory is full, '" + item.ItemName + "' was lost.");
    }

    public void DeselectAllSlot()
    {
        foreach (ItemSlot slot in itemSlot)
        {
            slot.isItemSelected = false;
            if (slot.SelectedPanel != null)
            {
                slot.SelectedPanel.SetActive(false);
            }
        }
    }

    public void SelectSlot(ItemSlot slot)
    {
        DeselectAllSlot();
        if (slot.SelectedPanel != null)
        {
            slot.SelectedPanel.SetActive(true);
        }
        slot.isItemSelected = true;

        if (detailsPanel != null)
        {
            detailsPanel.Show(slot.ItemDetails, slot.itemQuantity);
        }
    }

    public void ShowFirstItem()
    {
        if (itemSlot != null)
        {
            foreach (ItemSlot slot in itemSlot)
            {
                if (!slot.isEmpty)
                {
                    SelectSlot(slot);
                    return;
                }
            }
        }
    }
}