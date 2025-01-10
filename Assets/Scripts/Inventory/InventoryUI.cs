using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using StarterAssets;
using Unity.Services.Multiplayer;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public IInventory Inventory;
    public GameObject PlayerInventory;
    [SerializeField] private InventoryUIController _inventoryUIController;
    [SerializeField] private Transform _cellsParent;
    [SerializeField] private GameObject _cell;
    [SerializeField] private GameObject _item;
    [SerializeField] private Vector2 _offset;
    private Vector2 _pointer;
    private int _cellsGenerated;
    private bool _isDragging = false;
    private List<GameObject> _cellObjects = new List<GameObject>();
    // private Transform _draggingTransform;
    // private Item _draggingItem;
    private bool _cellIsBusy;
    // private Item _tempItem;
    private void Start()
    {
        Inventory = PlayerInventory.GetComponent<IInventory>();
        InventoryInitialize();
    }

    // private void Update()
    // {
    //     Dragging();
    // }

    // protected virtual void Dragging()
    // {
    //     if(_isDragging)
    //     {
    //         _draggingTransform.position = Input.mousePosition;
    //     }
    // }


    public virtual void InventoryInitialize()
    {
        _cellsGenerated = 0;
        _pointer = Vector2.zero;
        for(int i = 0; i < Inventory.CellsCount / Inventory.CellsInRow; i++)
        {
            for(int j = 0; j < Inventory.CellsInRow; j++)
            {
                GameObject cell = Instantiate(_cell, _cellsParent);
                IInventoryCell inventoryCell = cell.GetComponent<IInventoryCell>();
                _cellObjects.Add(cell);
                inventoryCell.Id = _cellsGenerated;
                cell.GetComponent<Button>().onClick.AddListener(() => CellClick(cell));
                if(CellHaveItems(Inventory, inventoryCell))
                {
                    InitializeItem(inventoryCell, cell);
                }
                _cellsGenerated++;
                float cellWidth = cell.GetComponent<RectTransform>().rect.width;
                float cellsParentWidth = _cellsParent.GetComponent<RectTransform>().rect.width;
                cell.transform.localPosition = _pointer - Vector2.right * ( cellsParentWidth / 2 - cellWidth / 2);
                _pointer.x += _offset.x;
            }
            _pointer.y -= _offset.y;
            _pointer.x = 0;
        }
    }

    private void InitializeItem(IInventoryCell cell, GameObject cellObject)
    {
        // GameObject itemObject = GetItemObjectFromCell(cellObject);
        GameObject itemObject = Instantiate(_item, cellObject.transform);
        itemObject.transform.localPosition = Vector2.zero;
        itemObject.GetComponent<ItemUIObject>().Image.sprite = Inventory.GetItemFromInventory(cell.Id).Sprite;
    }
    public void InitializeItem(int cellId)
    {
        GameObject itemObject = Instantiate(_item, _cellObjects[cellId].transform);
        itemObject.transform.localPosition = Vector2.zero;
        itemObject.GetComponent<ItemUIObject>().Image.sprite = Inventory.GetItemFromInventory(cellId).Sprite;
    }

    private bool CellHaveItems(IInventory inventory, IInventoryCell cell)
    {
        if(inventory.GetItemFromInventory(cell.Id) != null)
        {
            return true;
        }
        return false;
    }

    private GameObject GetItemObjectFromCell(GameObject cellObject)
    {
        ItemUIObject item = cellObject.GetComponentInChildren<ItemUIObject>();
        if(item != null)
        {
            return item.gameObject;
        }
        else
        {
            return null;
        }
    }

    protected virtual void CellClick(GameObject cellObject)
    {
        IInventoryCell cell = cellObject.GetComponent<IInventoryCell>();
        IInventory inventoryClicked = cellObject.GetComponentInParent<InventoryUI>().Inventory;
        _cellIsBusy = false;
        if(_inventoryUIController.IsDragging)
        {
            GameObject item = GetItemObjectFromCell(cellObject);
            if(CellHaveItems(inventoryClicked, cell))
            {
                _cellIsBusy = true;
                _inventoryUIController.TempItem = Inventory.GetItemFromInventory(cell.Id);
                Inventory.RemoveItemFromInvenory(cell.Id);
                item.transform.SetParent(_inventoryUIController.transform);
            }
            _inventoryUIController.PlaceItem(inventoryClicked, cellObject, cell);
            if(_cellIsBusy)
            {
                _inventoryUIController.TakeItem(item, _inventoryUIController.TempItem);
                _inventoryUIController.TempItem = null;
            }
        }
        else
        {
            if(CellHaveItems(inventoryClicked, cell))
            {
                GameObject item = GetItemObjectFromCell(cellObject);
                _inventoryUIController.TakeItem(item, inventoryClicked.GetItemFromInventory(cell.Id));
                item.transform.SetParent(_inventoryUIController.transform);
                inventoryClicked.RemoveItemFromInvenory(cell.Id);
            }
        }
    }


}
