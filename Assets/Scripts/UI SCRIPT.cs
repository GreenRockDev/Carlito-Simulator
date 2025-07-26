using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

public class UISCRIPT : MonoBehaviour
{
    public Transform menu1; // se asignan los menues en el editos
    public Transform menu2;
    public Transform menu3;

    public List<Transform> menu1Buttons; // se asignan los botones
    public List<Transform> menu2Buttons;
    public List<Transform> menu3Buttons;

    public List<Transform> menu1Sprites;
    public List<Transform> menu2Sprites;// y estos son los sprites
    public List<Transform> menu3Sprites;


    public float transitionTime = 0.3f;                     // se dicta cuanto tiempo van a tardar
    public Vector3 visiblePos = new Vector3(0, 0, 0);       //y se dicta a donde van cuando estan en uso y cuando no
    public Vector3 hiddenPos = new Vector3(0, 10f, 0);      
    public Vector3 buttonOffset = new Vector3(0, -5, 0);
    public Vector3 spriteOffset = new Vector3(0, -5, 0);// esto es cuanto se tiene que mover un sprite

    private int currentMenu = 0; // menu inicial osea el mostrador
    private Transform[] menus; //lista de los menus
    private List<Transform>[] menuButtons;  //lista de todos los objetos con los menus
    private List<Transform>[] menuSprites;
    private Coroutine currentTransition;

    void Start()

    {
        //se crea una lista con todos los menus
        menus = new Transform[] { null, menu1, menu2, menu3 };
        menuButtons = new List<Transform>[] { null, menu1Buttons, menu2Buttons, menu3Buttons };
        menuSprites = new List<Transform>[] { null, menu1Sprites, menu2Sprites, menu3Sprites };

        // se esconden todos
        for (int i = 1; i < menus.Length; i++)
        {
            menus[i].position = hiddenPos;

            foreach (var btn in menuButtons[i])
            {
                btn.position += hiddenPos; 
            }
            foreach (var sprite in menuSprites[i])
            {
                sprite.position += hiddenPos;
            }
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
        List<Coroutine> running = new List<Coroutine>();
        // el actual menu se mueve arriva si no es el mostrador
        if (currentMenu != 0)
        {
            Transform current = menus[currentMenu];
            yield return StartCoroutine(SmoothMove(current, hiddenPos)); //llendo a hiddenpos

            foreach (var btn in menuButtons[currentMenu])
            {
                Vector3 upPos = btn.position - buttonOffset;
                running.Add(StartCoroutine(SmoothMove(btn, upPos)));
            }
            foreach (var Sprite in menuSprites[currentMenu])
            {
                Vector3 upPos = Sprite.position - spriteOffset;
                running.Add(StartCoroutine(SmoothMove(Sprite, upPos)));
            }
        }

        // y se mueve el nuevo menu abajo si no es el mostrador
        if (newIndex != 0)
        {
            Transform next = menus[newIndex];
            yield return StartCoroutine(SmoothMove(next, visiblePos));
            foreach (var btn in menuButtons[newIndex])
            {
                Vector3 downPos = btn.position + buttonOffset;
                running.Add(StartCoroutine(SmoothMove(btn, downPos)));
            }
            foreach (var Sprite in menuSprites[newIndex])
            {
                Vector3 downPos = Sprite.position + spriteOffset;
                running.Add(StartCoroutine(SmoothMove(Sprite, downPos)));
            }
        }
        foreach (var c in running) yield return c;
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

    
   

