using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverlayController : MonoBehaviour
{
    public Image img;
    public float disappearVelocity;

    public void FadeIn()
    {
        img.enabled = true;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alpha = 1.0f;

        while (alpha > 0.0f)
        {
            alpha -= (1.0f / disappearVelocity) * Time.deltaTime;
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        img.enabled = false;
    }
}
