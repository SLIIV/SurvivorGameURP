using UnityEngine;


public class PlayerInventoryUI : InventoryUI
{

    public override void InventoryInitialize()
    {
        Inventory = PlayerInventory.GetComponent<IPlayerInventory>();
        base.InventoryInitialize();
    }

    protected override void CellClick(GameObject cellObject)
    {
        base.CellClick(cellObject);
    }


}
