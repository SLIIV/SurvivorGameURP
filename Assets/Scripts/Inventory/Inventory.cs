using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public interface IInventory
{
    public int CellsInRow {get;}
    public int CellsCount {get;}

    public Item GetItemFromInventory(int index);
    public void RemoveItemFromInvenory(int index);
    public void AddItemToInventory(int index, Item item);
}

public class Inventory : MonoBehaviour, IInventory
{
    public int CellsInRow { get { return _cellsInRow; } }
    public int CellsCount { get { return _cellsCount; } }
    [SerializeField] private int _cellsInRow = 6;

    [SerializeField] private int _cellsCount = 18;

    //for inventory UI test
    [SerializeField] private Item _testItem;
    [SerializeField] private Item _testItem2;
    //

    private List<Item> _itemsInInventory = new List<Item>();

    private void Awake()
    {
        InitializeInventory();
    }

    protected virtual void InitializeInventory()
    {
        for(int i = 0; i < _cellsCount; i++)
        {
            _itemsInInventory.Add(null); //Создаём пустой инвентарь
        }
        _itemsInInventory[0] = _testItem;
        _itemsInInventory[2] = _testItem2;
    }


    public virtual Item GetItemFromInventory(int index)
    {
        Debug.Log(gameObject + " " + index);
        return _itemsInInventory[index];

    }

    public virtual void RemoveItemFromInvenory(int index)
    {
        _itemsInInventory[index] = null;
    }

    public virtual void AddItemToInventory(int index, Item item)
    {
        _itemsInInventory[index] = item;
    }

}
