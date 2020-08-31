using MirkoZambito;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReviveViewController : MonoBehaviour {

    [SerializeField] private RectTransform reviveSliderViewTrans = null;
    [SerializeField] private Image reviveSliderImg = null;
    [SerializeField] private Text reviveCountTxt = null;
    [SerializeField] private RectTransform reviveBtnTrans = null;
    [SerializeField] private RectTransform closeReviveViewBtnTrans = null;


    public void OnShow()
    {
        reviveSliderImg.fillAmount = 1;
        reviveCountTxt.text = IngameManager.Instance.ReviveWaitTime.ToString();
        StartCoroutine(CROnShow());
    }

    private void OnDisable()
    {
        reviveSliderViewTrans.localScale = Vector3.zero;
        closeReviveViewBtnTrans.localScale = Vector2.zero;
    }



    private IEnumerator CROnShow()
    {
        ViewManager.Instance.ScaleRect(reviveSliderViewTrans, Vector2.zero, Vector2.one, 0.5f);
        yield return new WaitForSeconds(0.5f);
        ViewManager.Instance.ScaleRect(reviveBtnTrans, Vector2.zero, Vector2.one, 0.5f);
        StartCoroutine(CRReviveCountDown());
        StartCoroutine(CRFillingReviveSlider());
        StartCoroutine(CRScaleReviveButton());
        yield return new WaitForSeconds(1f);
        ViewManager.Instance.ScaleRect(closeReviveViewBtnTrans, Vector2.zero, Vector2.one, 0.5f);
    }



    /// <summary>
    /// Start counting down revive wait time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CRReviveCountDown()
    {
        float t = IngameManager.Instance.ReviveWaitTime;
        float decreaseAmount = 1f;
        while (t > 0)
        {
            if (IngameManager.Instance.IngameState != IngameState.Ingame_Revive)
                yield break;
            t -= decreaseAmount;
            reviveCountTxt.text = t.ToString();
            yield return new WaitForSeconds(decreaseAmount);
        }
        IngameManager.Instance.GameOver();
    }


    private IEnumerator CRFillingReviveSlider()
    {
        float t = 0;
        float fillingTime = IngameManager.Instance.ReviveWaitTime;
        while (t < fillingTime)
        {
            if (IngameManager.Instance.IngameState != IngameState.Ingame_Revive)
                yield break;
            t += Time.deltaTime;
            float factor = t / fillingTime;
            reviveSliderImg.fillAmount = Mathf.Lerp(1f, 0f, factor);
            yield return null;
        }
    }



    /// <summary>
    /// Scale the revive button.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CRScaleReviveButton()
    {
        float time = 0.3f;
        float t = 0;
        Vector2 startScale = Vector2.one;
        Vector2 endScale = Vector2.one * 1.15f;
        reviveBtnTrans.localScale = startScale;
        while (gameObject.activeInHierarchy)
        {
            t = 0;
            while (t < time)
            {
                t += Time.deltaTime;
                float factor = t / time;
                reviveBtnTrans.localScale = Vector2.Lerp(startScale, endScale, factor);
                yield return null;
            }

            t = 0;
            while (t < time)
            {
                t += Time.deltaTime;
                float factor = t / time;
                reviveBtnTrans.localScale = Vector2.Lerp(endScale, startScale, factor);
                yield return null;
            }
        }
    }


    public void ReviveBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
    }

    public void CloseReviveViewBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
        IngameManager.Instance.GameOver();
    }
}
