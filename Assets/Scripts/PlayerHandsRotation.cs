using Unity.VisualScripting;
using UnityEngine;

public class PlayerHandsRotation : MonoBehaviour
{
    [SerializeField] private Transform _rootObject;
    private Transform _transform;

    private void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        _transform.localRotation = _rootObject.localRotation;
    }
}
