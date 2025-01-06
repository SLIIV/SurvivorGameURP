using UnityEngine;


public class HandsUI : InventoryUI
{
    private void Start()
    {
        Inventory = PlayerInventory.GetComponent<IPlayerHandsInventory>();
        InventoryInitialize();
    }

    protected override void InventoryInitialize()
    {
        base.InventoryInitialize();
    }
    protected override void CellClick(GameObject cellObject)
    {
        base.CellClick(cellObject);
    }
}
