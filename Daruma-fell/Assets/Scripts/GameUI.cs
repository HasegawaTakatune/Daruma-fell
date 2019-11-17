using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームUI
/// </summary>
public class GameUI : MonoBehaviour
{
    /// <summary>
    /// メッセージ
    /// </summary>
    [SerializeField] private Text message;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        message = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// メッセージの有効/無効化
    /// </summary>
    /// <param name="value"></param>
    public void MessageActive(bool value)
    {
        gameObject.SetActive(value);
    }

    /// <summary>
    /// 始まりメッセージ
    /// </summary>    
    public void SetStartMessage(bool active = true)
    {
        message.text = "はじめる";
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 終わりメッセージ
    /// </summary>
    public void SetEndMessage(bool active = true)
    {
        message.text = "おわる";
        gameObject.SetActive(active);
    }
}
