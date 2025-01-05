using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventoryCell
{
    public int Id {get; set;}
}
public class InventoryCell : MonoBehaviour, IInventoryCell
{
    public int Id {get; set;}

    // void IDragHandler.OnDrag(PointerEventData eventData)
    // {

    // }

    // void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    // {

    // }

    // void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    // {

    // }
}


