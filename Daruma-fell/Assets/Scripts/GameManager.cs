using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームマネージャー
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームステータス
    /// </summary>
    private GAME_STATE state = GAME_STATE.MENU;

    /// <summary>
    /// ゲームUI
    /// </summary>
    [SerializeField] private GameUI GameUI = null;

    /// <summary>
    /// 黒板
    /// </summary>
    [SerializeField] private Text blackboard = null;

    /// <summary>
    /// プレイヤー
    /// </summary>
    [SerializeField] private Player player = null;

    /// <summary>
    /// 幽霊
    /// </summary>
    [SerializeField] private Ghost ghost = null;

    /// <summary>
    /// 目
    /// </summary>
    [SerializeField] private GameObject Eye = null;

    /// <summary>
    /// ルール説明
    /// </summary>
    private string msgRule = "だるまさんが転んだを一緒に\n遊んであげてください。";

    /// <summary>
    /// 終了メッセージ
    /// </summary>
    private string msgEnd = "遊んでくれてありがとう。";

    /// <summary>
    /// 反則メッセージ
    /// </summary>
    private string msgViolation = "ルールを守って、楽しく遊んで\nあげてください。";

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        ChangeStatus(GAME_STATE.MENU);
    }

    /// <summary>
    /// ステータス変更
    /// </summary>
    /// <param name="value"></param>
    public void ChangeStatus(GAME_STATE value)
    {
        state = value;

        switch (state)
        {
            case GAME_STATE.MENU: StartCoroutine(Menu()); break;
            case GAME_STATE.PLAYING: StartCoroutine(Playing()); break;
            case GAME_STATE.END: StartCoroutine(End()); break;
            default: break;
        }
    }

    /// <summary>
    /// メニュー選択
    /// </summary>
    /// <returns></returns>
    private IEnumerator Menu()
    {
        blackboard.text = msgRule;
        GameUI.SetStartMessage();

        yield return StartCoroutine(player.FadeIn());

        // ゲーム選択の選択まで待機させる
        yield return StartCoroutine(player.Selected());

        // ゲームを開始する
        blackboard.enabled = false;
        GameUI.MessageActive(false);
        ghost.ChangeMode(GHOST_MODE.MOVE);
        ChangeStatus(GAME_STATE.PLAYING);

    }

    /// <summary>
    /// ゲーム中
    /// </summary>
    /// <returns></returns>
    private IEnumerator Playing()
    {
        // 幽霊の動きが終わるまでループ
        while (ghost.GetMode != GHOST_MODE.GAME_SET && ghost.GetMode != GHOST_MODE.FOUL)
            yield return null;

        // メッセージ設定
        switch (ghost.GetMode)
        {
            case GHOST_MODE.GAME_SET: blackboard.text = msgEnd; blackboard.enabled = true; break;
            case GHOST_MODE.FOUL: blackboard.text = msgViolation; blackboard.enabled = true; break;
            default: break;
        }

        ChangeStatus(GAME_STATE.END);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <returns></returns>
    private IEnumerator End()
    {
        // ゲームメッセージ表示
        GameUI.SetEndMessage();
        yield return StartCoroutine(player.Selected());

        // 赤い目がちらつく
        Eye.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Eye.SetActive(false);

        // フェードアウトした後シーンをリロードする
        yield return StartCoroutine(player.FadeOut());
        SceneManager.LoadScene(0);
    }

}
