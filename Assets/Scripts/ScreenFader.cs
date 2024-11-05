using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenFader : MonoBehaviour
{
    public float solidAlpha = 1f;
    public float clearAlpha = 0f;
    public float delay = 0f;
    public float timeToFade = 1f;
    
    MaskableGraphic m_graphic;
    
    private void Start()
    {
        m_graphic = GetComponent<MaskableGraphic>();
    }

    IEnumerator FadeRoutine(float alpha)
    {
        yield return new WaitForSeconds(delay);
        m_graphic.CrossFadeAlpha(alpha, timeToFade, true);
    }
    
    public void FadeOn()
    {
        StartCoroutine(FadeRoutine(solidAlpha));
    }
    
    public void FadeOff()
    {
        StartCoroutine(FadeRoutine(clearAlpha));
    }
}
