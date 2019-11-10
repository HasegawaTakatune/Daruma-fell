using System.Collections;
using UnityEngine;

/// <summary>
/// 蛍光灯
/// </summary>
public class FluorescentLight : MonoBehaviour
{
    /// <summary>
    /// タイプ
    /// </summary>
    private LIGHT type = LIGHT.LIGHT_UP;

    /// <summary>
    /// タイプを変更する
    /// </summary>
    /// <param name="value"></param>
    public void ChangeType(LIGHT value)
    {
        // タイプの変更
        type = value;
        StopCoroutine(Flashing());

        // 変更したタイプごとに蛍光灯を制御する
        switch (type)
        {
            case LIGHT.LIGHT_UP: On(); break;
            case LIGHT.FLASHING: StartCoroutine(Flashing()); break;
            case LIGHT.OFF: Off(); break;
            default: break;
        }
    }

    /// <summary>
    /// 点滅間隔(最大値)
    /// </summary>
    private const float FlashIntervalMax = 0.5f;

    /// <summary>
    /// 点滅間隔(最小値)
    /// </summary>
    private const float FlashIntervalMin = 0.1f;

    /// <summary>
    /// レンダラー
    /// </summary>
    [SerializeField] private new Renderer renderer;

    /// <summary>
    /// ライト
    /// </summary>
    [SerializeField] private new Light light;

    /// <summary>
    /// リセットイベント(スクリプトアタッチ時)
    /// </summary>
    private void Reset()
    {
        renderer = GetComponent<Renderer>();
        light = GetComponentInChildren<Light>();
    }

    /// <summary>
    /// スタートイベント
    /// </summary>    
    private void Start()
    {
        ChangeType(LIGHT.LIGHT_UP);
    }

    /// <summary>
    /// 点滅処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator Flashing()
    {
        float interval = Random.Range(FlashIntervalMin, FlashIntervalMax);

        // タイプが点滅の間点滅させる
        while (type == LIGHT.FLASHING)
        {
            // 蛍光灯を消す
            yield return new WaitForSeconds(interval);
            Off();

            // 蛍光灯を点ける
            yield return new WaitForSeconds(interval);
            On();

            interval = Random.Range(FlashIntervalMin, FlashIntervalMax);
        }
    }

    /// <summary>
    /// 点灯
    /// </summary>
    private void On()
    {
        light.enabled = true;
        renderer.material.EnableKeyword("_EMISSION");
    }

    /// <summary>
    /// 消灯
    /// </summary>
    private void Off()
    {
        light.enabled = false;
        renderer.material.DisableKeyword("_EMISSION");
    }


}
