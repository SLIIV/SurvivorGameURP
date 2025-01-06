using UnityEngine;

internal interface IPlayerHandsInventory : IInventory
{
}
public class PlayerHandsInventory : Inventory, IPlayerHandsInventory
{
    private void Awake()
    {
        InitializeInventory();
    }

    protected override void InitializeInventory()
    {
        base.InitializeInventory();
    }
}
