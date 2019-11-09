using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蛍光灯
/// </summary>
public class FluorescentLight : MonoBehaviour
{
    #region "変数宣言"

    /// <summary>
    /// 
    /// </summary>
    private LIGHT type = LIGHT.LIGHT_UP;

    /// <summary>
    /// 
    /// </summary>
    private const float FlashIntervalMax = 0.5f;

    /// <summary>
    /// 
    /// </summary>
    private const float FlashIntervalMin = 0.1f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private new Renderer renderer;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private new Light light;

    #endregion


    /// <summary>
    /// 
    /// </summary>
    #region "Reset"
    private void Reset()
    {
        renderer = GetComponent<Renderer>();
        light = GetComponentInChildren<Light>();
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    #region "Start"
    private void Start()
    {
        type = LIGHT.FLASHING;
        StartCoroutine(Flashing());
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    #region "Flashing"
    private IEnumerator Flashing()
    {
        float interval = Random.Range(FlashIntervalMin, FlashIntervalMax);

        while (type == LIGHT.FLASHING)
        {
            yield return new WaitForSeconds(interval);
            Off();

            yield return new WaitForSeconds(interval);
            On();
            interval = Random.Range(FlashIntervalMin, FlashIntervalMax);
        }
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    #region "On"
    private void On()
    {
        light.enabled = true;
        renderer.material.EnableKeyword("_EMISSION");
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    #region "Off"
    private void Off()
    {
        light.enabled = false;
        renderer.material.DisableKeyword("_EMISSION");
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void ChangeType(LIGHT value)
    {
        type = value;
        StopCoroutine(Flashing());

        switch (type)
        {
            case LIGHT.LIGHT_UP: On(); break;
            case LIGHT.FLASHING: StartCoroutine(Flashing()); break;
            case LIGHT.OFF: Off(); break;
            default: break;
        }
    }

}
