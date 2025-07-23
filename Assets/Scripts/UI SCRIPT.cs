using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class UISCRIPT : MonoBehaviour
{
    public Transform menu1; // se asignan los menues en el editos
    public Transform menu2;
    public Transform menu3;

    public float transitionTime = 0.3f;
    public Vector3 visiblePos = new Vector3(0, 0, 0);       // se dicta a donde van cuando estan en uso y cuando no
    public Vector3 hiddenPos = new Vector3(0, 10f, 0);      // y se dicta cuanto tiempo van a tardar

    private int currentMenu = 0; // menu inicial osea el mostrador
    private Transform[] menus;
    private Coroutine currentTransition;

    void Start()

    {
        //se crea una lista con todos los menus
        menus = new Transform[] { null, menu1, menu2, menu3 };

        // se esconden todos
        for (int i = 1; i < menus.Length; i++)
        {
            menus[i].position = hiddenPos;
        }
    }

    public void ShowMenu(int menuIndex) // esta funcion se encarga de mostrar el menu seleccionado
    {
        if (menuIndex == currentMenu) // si es el actual no hace nada
            return;

        if (currentTransition != null) //si hay una transcision activa se para
            StopCoroutine(currentTransition);

        currentTransition = StartCoroutine(SwitchMenus(menuIndex)); // y se inicia la transicion al menu seleccionado
    }

    private IEnumerator SwitchMenus(int newIndex) // esta funcion se encarga de cambiar el menu
    {
        // el actual menu se mueve arriva si no es el mostrador
        if (currentMenu != 0)
        {
            Transform current = menus[currentMenu];
            yield return StartCoroutine(SmoothMove(current, hiddenPos)); //llendo a hiddenpos
        }

        // y se mueve el nuevo menu abajo si no es el mostrador
        if (newIndex != 0)
        {
            Transform next = menus[newIndex];
            yield return StartCoroutine(SmoothMove(next, visiblePos));
        }

        currentMenu = newIndex; // se cambia cual es el menu actual
    }

    private IEnumerator SmoothMove(Transform target, Vector3 toPos) // anima el menu
    {
        Vector3 fromPos = target.position;
        float elapsed = 0f;

        while (elapsed < transitionTime)
        {
            float t = elapsed / transitionTime;
            target.position = Vector3.Lerp(fromPos, toPos, Mathf.SmoothStep(0f, 1f, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.position = toPos;
    }
}

    
   

