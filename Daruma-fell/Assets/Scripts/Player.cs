using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤー
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 選択中のアイコン
    /// </summary>
    [SerializeField] private Image selectedIcon = null;

    /// <summary>
    /// フェード
    /// </summary>
    [SerializeField] private Image fade = null;

    /// <summary>
    /// レイヤーマスク
    /// </summary>
    [SerializeField] private LayerMask mask = new LayerMask();

    /// <summary>
    /// 垂直ローテーション
    /// </summary>
    private Transform verRot;

    /// <summary>
    /// 水平ローテーション
    /// </summary>
    private Transform horRot;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        verRot = transform.parent;
        horRot = GetComponent<Transform>();
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        float xRotation = Input.GetAxis("Mouse X");
        float yRotation = Input.GetAxis("Mouse Y");

        verRot.transform.Rotate(0, xRotation, 0);
        horRot.transform.Rotate(-yRotation, 0, 0);
#endif 
    }

    /// <summary>
    /// 選択アイコンコントロール中
    /// </summary>
    /// <returns></returns>
    public IEnumerator Selected()
    {
        while (true)
        {
            yield return null;

            // 選択項目を注視している間ゲージがたまる
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, mask))
            {
                selectedIcon.fillAmount += Time.deltaTime;
                if (selectedIcon.fillAmount >= 1) break;
            }
            else
            {
                selectedIcon.fillAmount = 0;
            }
        }
        selectedIcon.fillAmount = 0;
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeIn()
    {
        Color fadeIn = new Color(0, 0, 0, -Time.deltaTime);
        while (fade.color.a > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            fade.color += fadeIn;
        }
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut()
    {
        Color fadeOut = new Color(0, 0, 0, Time.deltaTime);
        while (fade.color.a < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            fade.color += fadeOut;
        }
    }
}
