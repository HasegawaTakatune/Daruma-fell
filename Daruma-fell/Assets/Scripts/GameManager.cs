using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private GAME_STATE state = GAME_STATE.MENU;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject startUI;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject endUI;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Player player;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Ghost ghost;

    private string[] message = { "", "" }; 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void ChangeStatus(GAME_STATE value)
    {
        state = value;

        switch (state)
        {
            case GAME_STATE.MENU: StartCoroutine(Menu()); break;
            case GAME_STATE.PLAYING: break;
            case GAME_STATE.END: break;
            default: break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Menu()
    {
        startUI.SetActive(true);

        yield return StartCoroutine(player.FadeIn());

        yield return StartCoroutine(player.Selected());

        startUI.SetActive(false);
        ghost.ChangeMode(GHOST_MODE.MOVE);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator End()
    {
        endUI.SetActive(true);
        Coroutine coroutine = StartCoroutine(player.Selected());

        yield return coroutine;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        ChangeStatus(GAME_STATE.MENU);
    }


}
