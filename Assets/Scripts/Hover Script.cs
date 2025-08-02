
//HoverScript
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public GameObject menu;
    private bool isPointerOverTrigger = false;
    private bool isPointerOverMenu = false;

    void FixedUpdate()
    {
        if (menu != null)
        {
            menu.SetActive(isPointerOverTrigger || isPointerOverMenu);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverTrigger = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverTrigger = false;
    }

    public void SetMenuHover(bool value)
    {
        isPointerOverMenu = value;
    }
}