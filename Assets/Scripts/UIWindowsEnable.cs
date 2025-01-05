using StarterAssets;
using UnityEngine;

public class UIWindowsEnable : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _inventory;
    private IPlayersUIInput _input;

    private void Start()
    {
        _input = _player.GetComponent<IPlayersUIInput>();
    }

    private void Update()
    {
        if(_input.Inventory)
        {
            _inventory.SetActive(true);
        }
        else
        {
            _inventory.SetActive(false);
        }
    }
}
