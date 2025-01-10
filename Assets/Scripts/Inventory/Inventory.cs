using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.Playables;

public interface IInventory
{
    public int CellsInRow {get;}
    public int CellsCount {get;}

    public Item GetItemFromInventory(int index);
    public void RemoveItemFromInvenory(int index);
    public void AddItemToInventory(int index, Item item);
    public void AddItemToInventory(int index, Item item, int count);
    public bool IsFree();
    public int GetFreeCell();
    public int GetCountOfItem(int index);
    public bool IsItemUnstack(Item item);
    public int GetFirstUnstackItem(Item item);
    public void AddToStack(int index, int count);
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
    private List<int> _itemsCount = new List<int>();
    private void Awake()
    {
        InitializeInventory();
    }

    protected virtual void InitializeInventory()
    {
        for(int i = 0; i < _cellsCount; i++)
        {
            _itemsInInventory.Add(null); //Создаём пустой инвентарь
            _itemsCount.Add(0);
        }
        _itemsInInventory[0] = _testItem;
        _itemsCount[0] = 1;
        _itemsInInventory[2] = _testItem2;
        _itemsCount[2] = 1;
    }


    public virtual Item GetItemFromInventory(int index)
    {
        return _itemsInInventory[index];
    }

    public virtual int GetCountOfItem(int itemIndex)
    {
        return _itemsCount[itemIndex];
    }

    public virtual void RemoveItemFromInvenory(int index)
    {
        _itemsInInventory[index] = null;
        _itemsCount[index] = 0;
    }

    public virtual void AddItemToInventory(int index, Item item)
    {
        _itemsInInventory[index] = item;
        _itemsCount[index] = 1;
    }

    public virtual void AddItemToInventory(int index, Item item, int count)
    {
        _itemsInInventory[index] = item;
        _itemsCount[index] = count;
    }
    public virtual void AddToStack(int index, int count)
    {
        _itemsCount[index] += count;
    }
    public virtual void RemoveFromStack(int index, int count)
    {
        _itemsCount[index] -= count;
    }

    public bool IsFree()
    {
        return _itemsInInventory.Contains(null); //Содержит ли пустой слот
    }


    public int GetFreeCell()
    {
        for(int i = 0; i < _itemsInInventory.Count; i++)
        {
            if(_itemsInInventory[i] == null)
            {
                return i;
            }
        }
        return 0;
    }


    public bool IsItemUnstack(Item item)
    {
        for(int i = 0; i < _itemsInInventory.Count; i++)
        {
            if(_itemsInInventory[i] == item)
            {
                if(_itemsCount[i] < item.MaxStack)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetFirstUnstackItem(Item item)
    {
        for(int i = 0; i < _itemsInInventory.Count; i++)
        {
            if(_itemsInInventory[i] == item)
            {
                if(_itemsCount[i] < item.MaxStack)
                {
                    return i;
                }
            }
        }
        return 0;
    }

}
