using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverItemScript : MonoBehaviour
{
    public HoverScript trigger;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Pointer ENTER menu");
        trigger.SetMenuHover(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Pointer EXIT menu");
        trigger.SetMenuHover(false);
    }
}
