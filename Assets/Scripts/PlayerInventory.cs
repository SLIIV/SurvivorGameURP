using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInventory
{
    public int CellsInRow {get;}
    public int HandsCellCount {get;}
    public int CellsCount {get;}

    public Item GetItemFromInventory(int index);
    public void RemoveItemFromInvenory(int index);
    public void AddItemToInventory(int index, Item item);
}

public class PlayerInventory : MonoBehaviour, IPlayerInventory
{
    public int CellsInRow { get { return _cellsInRow; } }
    public int HandsCellCount { get { return _handsCellCount; } }
    public int CellsCount { get { return _cellsCount; } }
    [SerializeField] private int _cellsInRow = 6;
    [SerializeField] private int _handsCellCount = 6;
    [SerializeField] private int _cellsCount = 18;

    //for inventory UI test
    [SerializeField] private Item _testItem;
    [SerializeField] private Item _testItem2;
    //

    private List<Item> _itemsInInventory = new List<Item>();
    private List<Item> _itemsInHands = new List<Item>();

    private void Start()
    {
        InitializeHands();
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for(int i = 0; i < _cellsCount; i++)
        {
            _itemsInInventory.Add(null); //Создаём пустой инвентарь
        }
        _itemsInInventory[0] = _testItem;
        _itemsInInventory[2] = _testItem2;
    }

    private void InitializeHands()
    {
        for(int i = 0; i < _handsCellCount; i++)
        {
            _itemsInHands.Add(null);
        }
    }

    public Item GetItemFromInventory(int index)
    {
        return _itemsInInventory[index];
    }

    private Item GetItemFromHands(int index)
    {
        return _itemsInHands[index];
    }

    public void RemoveItemFromInvenory(int index)
    {
        _itemsInInventory[index] = null;
    }

    private void RemoveItemFromHands(int index)
    {
        _itemsInHands[index] = null;
    }

    public void AddItemToInventory(int index, Item item)
    {
        _itemsInInventory[index] = item;
    }

    private void AddItemToHands(int index, Item item)
    {
        _itemsInHands[index] = item;
    }
}
