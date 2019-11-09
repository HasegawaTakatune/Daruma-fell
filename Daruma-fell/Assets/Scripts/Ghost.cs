using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    #region "変数宣言"

    [SerializeField] private Transform target;
    [SerializeField] private AudioSource audioSource;
    private Transform trans;

    private Vector3 front;

    #endregion

    /// <summary>
    /// Reset
    /// </summary>
    #region "Reset"
    private void Reset()
    {
        target = GameObject.Find("Player").transform;
    }
    #endregion

    void Start()
    {
        trans = transform;
        trans.LookAt(target);
        front = trans.forward * Time.deltaTime;
    }

    void Update()
    {

    }

    private void Move()
    {


    }

}
