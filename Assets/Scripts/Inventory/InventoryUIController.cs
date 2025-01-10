using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public Transform DraggingTransform;
    public Item DraggingItem;
    public int DraggingCount;
    public Item TempItem;
    public int TempItemsCount;
    public bool IsDragging;
    [SerializeField] private List<InventoryUI> _inventories = new List<InventoryUI>();

    private void Start()
    {
        for(int i = 0; i < _inventories.Count; i++)
        {
            _inventories[i].InventoryInitialize();
        }
    }

    private void Update()
    {
        Dragging();
    }

    private void Dragging()
    {
        if(IsDragging)
        {
            DraggingTransform.position = Input.mousePosition;
        }
    }

    public void PlaceItem(IInventory inventory, GameObject cellObject, IInventoryCell cell)
    {
        IsDragging = false;
        DraggingTransform.SetParent(cellObject.transform);
        DraggingTransform.localPosition = Vector2.zero;
        inventory.AddItemToInventory(cell.Id, DraggingItem, DraggingCount);
        DraggingItem = null;
        DraggingTransform = null;
        DraggingCount = 0;
    }
    public void TakeItem(GameObject itemObject, Item item, int count)
    {
        IsDragging = true;
        DraggingTransform = itemObject.transform;
        DraggingItem = item;
        DraggingCount = count;
    }


}
