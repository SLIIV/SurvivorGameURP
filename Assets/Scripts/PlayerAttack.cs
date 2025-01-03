using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent OnPunch { get { return _punch; } }
    [SerializeField] private StarterAssetsInputs _input;
    private UnityEvent _punch = new UnityEvent();


    private void Start()
    {
        _punch.AddListener(() => Attack());
    }
    private void Update()
    {
        if(_input.attack)
        {
            _punch.Invoke();
            _input.attack = false;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");

    }


}
