using UnityEngine;

public interface IItem
{
    public Item ItemInfo {get;}
}
public class ItemObject : MonoBehaviour, IItem
{
    public Item ItemInfo { get { return _itemInfo; } }
    [SerializeField] private Item _itemInfo;

}
