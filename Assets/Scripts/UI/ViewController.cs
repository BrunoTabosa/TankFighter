using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ViewController : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        
    }

    public virtual void Show()
    {
        StartCoroutine(FadeAlpha(0f, 1f, 1f));
    }

    public virtual void Hide()
    {
        if (canvasGroup.alpha == 0) return;
        StartCoroutine(FadeAlpha(1f, 0f, 1f));
    }

    IEnumerator FadeAlpha(float begin, float final, float time)
    {
        begin = canvasGroup.alpha;
        for (float t = 0.0f; t < 1f; t += Time.deltaTime / time)
        {
            float newAlpha = Mathf.Lerp(begin, final, t);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }
        canvasGroup.alpha = final;
        if (final >= 1)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
