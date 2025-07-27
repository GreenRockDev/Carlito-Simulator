
//HoverScript
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menu;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menu != null) {
            UnityEngine.Debug.Log("hover");
            menu.SetActive(true);
            //menu.OrderLayer(1);
        }

}

    public void OnPointerExit(PointerEventData eventData)
    {
        if (menu != null) {
            menu.SetActive(false);
            //menu.OrderLayer(-1);
        }
    }
}