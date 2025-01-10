using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float _pickupDistance = 2f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerInventoryUI _playerInventoryUI;
    [SerializeField] private HandsUI _handsUI;
    private IPlayersInput _playerInput;
    private IPlayersUIInput _uiInput;
    private Vector3 _screenCenter;
    private IPlayerHandsInventory _hands;
    private IPlayerInventory _inventory;

    private void Start()
    {
        _hands = gameObject.GetComponent<IPlayerHandsInventory>();
        _inventory = gameObject.GetComponent<IPlayerInventory>();
        _playerInput = gameObject.GetComponent<IPlayersInput>();
        _uiInput = gameObject.GetComponent<IPlayersUIInput>();
        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    public void OnPickup(InputValue value)
    {
        if(!_uiInput.Inventory)
        {
            Pickup();
        }
    }
    private void Pickup()
    {
        _playerInput.Pickup = false;
        Ray ray = _camera.ScreenPointToRay(_screenCenter);
        if(Physics.Raycast(ray, out RaycastHit hit, _pickupDistance, _layer))
        {
            Item itemInfo = hit.transform.GetComponent<IItem>().ItemInfo;
            if(_hands.IsFree() || _hands.IsItemUnstack(itemInfo))
            {
                int freeCellIndex = _hands.GetFreeCell();

                if(_hands.IsItemUnstack(itemInfo))
                {
                    int itemIndex = _hands.GetFirstUnstackItem(itemInfo);
                    _hands.AddToStack(itemIndex, 1);
                    _handsUI.UpdateItemCount(itemIndex);
                }
                else
                {
                    _hands.AddItemToInventory(freeCellIndex, itemInfo);
                    _handsUI.InitializeItem(freeCellIndex);
                }
            }
            else if(_inventory.IsFree() || _inventory.IsItemUnstack(itemInfo))
            {
                int freeCellIndex = _inventory.GetFreeCell();
                if(_inventory.IsItemUnstack(itemInfo))
                {
                    int itemIndex = _inventory.GetFirstUnstackItem(itemInfo);
                    _inventory.AddToStack(itemIndex, 1);
                    _playerInventoryUI.UpdateItemCount(itemIndex);
                }
                else
                {
                    _inventory.AddItemToInventory(freeCellIndex, itemInfo);
                    _playerInventoryUI.InitializeItem(freeCellIndex);
                }

            }
        }
    }
}
