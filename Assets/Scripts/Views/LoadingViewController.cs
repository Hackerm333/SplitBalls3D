using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingViewController : MonoBehaviour {

    [SerializeField] private Text loadingTxt = null;
    [SerializeField] private Image loadingSliderImg = null;

    public void OnShow()
    {
        loadingSliderImg.fillAmount = 0.5f;
    }


    public void SetLoadingText(string text)
    {
        loadingTxt.text = text;
    }

    public void SetLoadingAmount(float amount)
    {
        loadingSliderImg.fillAmount = amount;
    }
}
