using System;
using System.Text.RegularExpressions;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _playerInventory;
    [SerializeField] private Transform _cellsParent;
    [SerializeField] private GameObject _cell;
    [SerializeField] private GameObject _item;
    [SerializeField] private Vector2 _offset;
    private IPlayerInventory _inventory;
    private IPlayersInput _input;
    private Vector2 _pointer;
    private int _cellsGenerated;
    private bool _isDragging = false;
    private Transform _draggingTransform;
    private Item _draggingItem;
    private GameObject _lastCellObject;
    private bool _cellIsBusy;
    private Item _tempItem;
    private void Start()
    {
        _cellsGenerated = 0;
        _pointer = Vector2.zero;
        _inventory = _playerInventory.GetComponent<IPlayerInventory>();
        InventoryInitialize();
    }

    private void Update()
    {
        if(_isDragging)
        {
            _draggingTransform.position = Input.mousePosition;
        }
    }

    private void InventoryInitialize()
    {
        for(int i = 0; i < _inventory.CellsCount / _inventory.CellsInRow; i++)
        {
            for(int j = 0; j < _inventory.CellsInRow; j++)
            {
                GameObject cell = Instantiate(_cell, _cellsParent);
                IInventoryCell inventoryCell = cell.GetComponent<IInventoryCell>();
                inventoryCell.Id = _cellsGenerated;
                cell.GetComponent<Button>().onClick.AddListener(() => CellClick(cell));
                if(CellHaveItems(inventoryCell))
                {
                    InitializeItem(inventoryCell, cell);
                }
                _cellsGenerated++;
                cell.transform.localPosition = _pointer - new Vector2(_cellsParent.localPosition.x, _cellsParent.localPosition.y);
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
        itemObject.GetComponent<ItemUIObject>().Image.sprite = _inventory.GetItemFromInventory(cell.Id).Sprite;
    }

    private bool CellHaveItems(IInventoryCell cell)
    {
        if(_inventory.GetItemFromInventory(cell.Id) != null)
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

    private void CellClick(GameObject cellObject)
    {
        IInventoryCell cell = cellObject.GetComponent<IInventoryCell>();
        _cellIsBusy = false;
        if(_isDragging)
        {

            // if(item != null)
            // {
            //     item.transform.SetParent(_lastCellObject.transform);
            //     item.transform.localPosition = Vector2.zero;
            // }
            GameObject item = GetItemObjectFromCell(cellObject);
            if(CellHaveItems(cell))
            {
                _cellIsBusy = true;
                _tempItem = _inventory.GetItemFromInventory(cell.Id);
                _inventory.RemoveItemFromInvenory(cell.Id);
                item.transform.SetParent(transform);
            }
            PlaceItem(cellObject, cell);
            if(_cellIsBusy)
            {
                TakeItem(item, _tempItem);
                _tempItem = null;
                _lastCellObject = cellObject;
            }
        }
        else
        {
            if(CellHaveItems(cell))
            {
                GameObject item = GetItemObjectFromCell(cellObject);
                TakeItem(item, _inventory.GetItemFromInventory(cell.Id));
                item.transform.SetParent(transform);
                _inventory.RemoveItemFromInvenory(cell.Id);
                _lastCellObject = cellObject;
            }
        }
    }
    private void PlaceItem(GameObject cellObject, IInventoryCell cell)
    {
        _isDragging = false;
        _draggingTransform.SetParent(cellObject.transform);
        _draggingTransform.localPosition = Vector2.zero;
        _inventory.AddItemToInventory(cell.Id, _draggingItem);
        //_inventory.RemoveItemFromInvenory(_lastCellObject.GetComponent<IInventoryCell>().Id);
        _draggingItem = null;
        _draggingTransform = null;
    }
    private void TakeItem(GameObject itemObject, Item item)
    {
        _isDragging = true;
        _draggingTransform = itemObject.transform;
        _draggingItem = item;
    }
}
