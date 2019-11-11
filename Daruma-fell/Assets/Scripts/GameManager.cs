using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GAMESTATE state = GAMESTATE.MENU;

    [SerializeField] private GameObject startUI;

    [SerializeField] private Player player;

   

    public void ChangeStatus(GAMESTATE value)
    {
        state = value;

        switch (state)
        {
            case GAMESTATE.MENU:Menu(); break;
            case GAMESTATE.START: break;
            case GAMESTATE.PLAYING: break;
            case GAMESTATE.END: break;
            default: break;
        }
    }

    private IEnumerator  Menu()
    {
        Coroutine coroutine;
        startUI.SetActive(true);
        coroutine = StartCoroutine(player.Selected());

        yield return coroutine;
    }

    


    void Start()
    {

    }

    void Update()
    {

    }


}
