using UnityEngine;


public class PlayerInventoryUI : InventoryUI
{

    private void Start()
    {
        Inventory = PlayerInventory.GetComponent<IPlayerInventory>();
        Debug.Log(Inventory);
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
