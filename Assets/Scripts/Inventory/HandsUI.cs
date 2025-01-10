using UnityEngine;


public class HandsUI : InventoryUI
{
    private void Start()
    {

        //InventoryInitialize();
    }

    public override void InventoryInitialize()
    {
        Inventory = PlayerInventory.GetComponent<IPlayerHandsInventory>();
        base.InventoryInitialize();
    }
    protected override void CellClick(GameObject cellObject)
    {
        base.CellClick(cellObject);
    }
}
