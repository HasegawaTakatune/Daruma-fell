using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 幽霊
/// </summary>
public class Ghost : MonoBehaviour
{
    /// <summary>
    /// モード
    /// </summary>
    private GHOST_MODE mode = GHOST_MODE.GAME_END;

    /// <summary>
    /// モード変更
    /// </summary>
    /// <param name="value"></param>
    public void ChangeMode(GHOST_MODE value)
    {
        StopAllCoroutines();
        mode = value;

        // モードごとに制御を変更する
        switch (mode)
        {
            case GHOST_MODE.MOVE: StartCoroutine(Move()); break;
            case GHOST_MODE.STOP: StartCoroutine(Stop()); break;
            case GHOST_MODE.FOUL_TURNED_AROUND: StartCoroutine(TurnedAround()); break;
            case GHOST_MODE.FOUL_NOT_TURN_AROUND: StartCoroutine(NotTurnAround()); break;
            default: break;
        }
    }

    /// <summary>
    /// 最大値
    /// </summary>
    private const float Max = 5.0f;

    /// <summary>
    /// 最小値
    /// </summary>
    private const float Min = 3.0f;

    /// <summary>
    /// ターゲット
    /// </summary>
    [SerializeField] private Transform target;

    /// <summary>
    /// オーディオソース
    /// </summary>
    [SerializeField] private AudioSource audioSource;

    /// <summary>
    /// 蛍光灯
    /// </summary>
    [SerializeField] private FluorescentLight[] lights = new FluorescentLight[9];

    /// <summary>
    /// 幽霊のトランスフォーム格納
    /// </summary>
    private Transform trans;

    /// <summary>
    /// 幽霊の進む方向(前方)
    /// </summary>
    private Vector3 front;

    /// <summary>
    /// 速度
    /// </summary>
    private float speed = 1.5f;

    /// <summary>
    /// 視界に入っているかの判定
    /// </summary>
    private bool Looked = false;

    /// <summary>
    /// リセットイベント(スクリプトアタッチ時)
    /// </summary>
    private void Reset()
    {
        target = GameObject.Find("Player").transform;
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i] = GameObject.Find("Light" + (i + 1)).GetComponent<FluorescentLight>();
        }
    }

    /// <summary>
    /// スタートイベント
    /// </summary>
    void Start()
    {
        ChangeMode(GHOST_MODE.MOVE);
        trans = transform;
        trans.LookAt(target);
        front = trans.forward * Time.deltaTime;
    }

    /// <summary>
    /// オブジェクトが判定に重なった時のイベント
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (mode == GHOST_MODE.MOVE || mode == GHOST_MODE.STOP)
            other.gameObject.GetComponent<FluorescentLight>().ChangeType(LIGHT.FLASHING);
    }

    /// <summary>
    /// オブジェクトが判定から外れた時のイベント
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (mode == GHOST_MODE.MOVE || mode == GHOST_MODE.STOP)
            other.gameObject.GetComponent<FluorescentLight>().ChangeType(LIGHT.LIGHT_UP);
    }

    /// <summary>
    /// カメラの視界に入った時のイベント
    /// </summary>
    private void OnBecameVisible()
    {
        Debug.Log("視界内");
        Looked = true;
    }

    /// <summary>
    /// カメラの視界から外れた時のイベント
    /// </summary>
    private void OnBecameInvisible()
    {
        Debug.Log("視界外");
        Looked = false;
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move()
    {
        Debug.Log("移動中");
        //StartCoroutine(Judgment());

        // 幽霊の掛け声の時間を取得・掛け声を発生
        float limit = 3.0f;//audioSource.clip.length;
        audioSource.Play();

        // 掛け声を発している間、ターゲットに近づく
        while (mode == GHOST_MODE.MOVE && limit > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            trans.position += front * speed;
            limit -= Time.deltaTime;
        }

        ChangeMode(GHOST_MODE.STOP);
    }

    /// <summary>
    /// 停止
    /// </summary>    
    private IEnumerator Stop()
    {
        Debug.Log("停止中");
        //StartCoroutine(Judgment());

        float interval = Random.Range(Min, Max);
        yield return new WaitForSeconds(interval);
        ChangeMode(GHOST_MODE.MOVE);
    }

    /// <summary>
    /// ターゲットが掛け声の途中で振り返った際の処理
    /// </summary>
    private IEnumerator TurnedAround()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        trans.position = target.position - target.forward;

        audioSource.Play();
    }

    /// <summary>
    /// ターゲットが掛け声を言い終わっても振り返らない際の処理
    /// </summary>
    private IEnumerator NotTurnAround()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        trans.position = target.position - target.forward;

        audioSource.Play();
    }

    /// <summary>
    /// 反則行為の判定
    /// </summary>
    /// <returns></returns>
    private IEnumerator Judgment()
    {
        yield return new WaitForSeconds(1.0f);

        // 掛け声を発し始めても幽霊を見続けているもしくは、
        // 掛け声を発し終えても幽霊の方向に向かなかった場合、反則行為とみなす
        switch (mode)
        {
            case GHOST_MODE.MOVE: if (Looked) ChangeMode(GHOST_MODE.FOUL_TURNED_AROUND); break;
            case GHOST_MODE.STOP: if (!Looked) ChangeMode(GHOST_MODE.FOUL_NOT_TURN_AROUND); break;
        }
    }
}
