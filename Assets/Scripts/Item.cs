using System.Data.Common;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "GameItems/Items")]
public class Item : ScriptableObject
{
    public int Id { get { return _id; } }
    public string Name { get { return _name; } }
    public Sprite Sprite { get {return _sprite; } }
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

}
