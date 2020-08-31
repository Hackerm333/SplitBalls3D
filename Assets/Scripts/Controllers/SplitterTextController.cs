using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitterTextController : MonoBehaviour
{
    [SerializeField] private Text textNumber = null;

    bool isDoneCoroutine = true;
    public void OnSetup(int number)
    {
        textNumber.text = "X" + number.ToString();
    }


    public void ScaleBounce()
    {
        if (isDoneCoroutine)
        {
            isDoneCoroutine = false;
            StartCoroutine(CRScaleBouncing());
        }
    }

    private IEnumerator CRScaleBouncing()
    {
        float bouncingTime = 0.05f;
        float t = 0;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = startScale * 1.1f;

        while (t < bouncingTime)
        {
            t += Time.deltaTime;
            float factor = t / bouncingTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, factor);
            yield return null;
        }

        t = 0;
        while (t < bouncingTime)
        {
            t += Time.deltaTime;
            float factor = t / bouncingTime;
            transform.localScale = Vector3.Lerp(endScale, startScale, factor);
            yield return null;
        }

        isDoneCoroutine = true;
    }
}
