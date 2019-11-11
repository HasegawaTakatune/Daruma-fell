using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Image selectedIcon;

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
    void Start()
    {
        verRot = transform.parent;
        horRot = GetComponent<Transform>();
    }

    /// <summary>
    /// メインループ
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        float xRotation = Input.GetAxis("Mouse X");
        float yRotation = Input.GetAxis("Mouse Y");

        verRot.transform.Rotate(0, xRotation, 0);
        horRot.transform.Rotate(-yRotation, 0, 0);
#endif 
    }

    public IEnumerator Selected()
    {
        while (true)
        {
            yield return null;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                selectedIcon.fillAmount += Time.deltaTime;
            }
            else
            {
                selectedIcon.fillAmount = 0;
            }
        }
    }
}
