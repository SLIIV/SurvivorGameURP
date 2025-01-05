using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CursorLockRules : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectForCheck;
    [SerializeField] private GameObject _player;
    private IPlayersInput _input;
    private bool _cursorLocked;
    private void Start()
    {
        _cursorLocked = true;
        _input = _player.GetComponent<IPlayersInput>();
    }

    private void Update()
    {
        _cursorLocked = true;
        for(int i = 0; i < _objectForCheck.Count; i++)
        {
            if(_objectForCheck[i].activeSelf)
            {
                _cursorLocked = false;
            }
        }
        if(_cursorLocked)
        {
            _input.CursorInputForLook = true;
            _input.CursorLocked = true;
        }
        else
        {
            _input.CursorInputForLook = false;
            _input.CursorLocked = false;
        }
    }
}
