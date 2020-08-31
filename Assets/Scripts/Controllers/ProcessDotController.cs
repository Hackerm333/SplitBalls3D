using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProcessDotController : MonoBehaviour
{
    [SerializeField] private Image dotFilterImg = null;
    [SerializeField] private Image processFilterImg = null;
    [SerializeField] private Text indexTxt = null;

    public void StartFilter()
    {
        StartCoroutine(CRFilteringDotImg());
        StartCoroutine(CRFilteringProcessImg());
    }
    private IEnumerator CRFilteringDotImg()
    {
        float fillingTime = 0.25f;
        float t = 0;
        while (t < fillingTime)
        {
            t += Time.deltaTime;
            float factor = t / fillingTime;
            dotFilterImg.fillAmount = Mathf.Lerp(0, 1f, factor);
            yield return null;
        }
    }


    private IEnumerator CRFilteringProcessImg()
    {
        yield return new WaitForSeconds(0.125f);
        float fillingTime = 0.75f;
        float t = 0;
        while (t < fillingTime)
        {
            t += Time.deltaTime;
            float factor = t / fillingTime;
            processFilterImg.fillAmount = Mathf.Lerp(0, 1f, factor);
            yield return null;
        }
    }



    /// <summary>
    /// Set the index for this process dot.
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(int index)
    {
        indexTxt.text = index.ToString();
    }



    /// <summary>
    /// Reset the filter images back to 0.
    /// </summary>
    public void ResetFilters()
    {
        dotFilterImg.fillAmount = 0;
        processFilterImg.fillAmount = 0;
    }
}
