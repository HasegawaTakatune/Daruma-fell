using UnityEngine;

public class Player : MonoBehaviour
{
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
}
