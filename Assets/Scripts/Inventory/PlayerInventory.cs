using Unity.VisualScripting;
using UnityEngine;

internal interface IPlayerInventory : IInventory
{

}
public class PlayerInventory : Inventory, IPlayerInventory
{
    private void Awake()
    {
        InitializeInventory();
    }

    protected override void InitializeInventory()
    {
        base.InitializeInventory();
    }
    public override void AddItemToInventory(int index, Item item)
    {
        base.AddItemToInventory(index, item);
    }
    public override Item GetItemFromInventory(int index)
    {
        return base.GetItemFromInventory(index);
    }
    public override void RemoveItemFromInvenory(int index)
    {
        base.RemoveItemFromInvenory(index);
    }
}
