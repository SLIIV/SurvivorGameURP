using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public Transform DraggingTransform;
    public Item DraggingItem;
    public Item TempItem;
    public bool IsDragging;
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
        inventory.AddItemToInventory(cell.Id, DraggingItem);
        DraggingItem = null;
        DraggingTransform = null;
    }
    public void TakeItem(GameObject itemObject, Item item)
    {
        IsDragging = true;
        DraggingTransform = itemObject.transform;
        DraggingItem = item;
    }


}
