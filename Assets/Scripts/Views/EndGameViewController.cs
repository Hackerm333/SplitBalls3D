using MirkoZambito;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameViewController : MonoBehaviour {

    [SerializeField] private RectTransform topBarTrans = null;
    [SerializeField] private Text nextLevelTxt = null;
    [SerializeField] private Text levelResultTxt = null;
    [SerializeField] private RectTransform playBtnTrans = null;
    [SerializeField] private Text playTxt = null;
    [SerializeField] private RectTransform homeBtnTrans = null;

    public void OnShow()
    {
        if (IngameManager.Instance.IngameState == IngameState.Ingame_CompletedLevel)
        {
            levelResultTxt.text = "LEVEL COMPLETED !";
            levelResultTxt.color = Color.green;
            playTxt.text = "CONTINUE";
        }
        else if (IngameManager.Instance.IngameState == IngameState.Ingame_GameOver)
        {
            levelResultTxt.text = "LEVEL FAILED !";
            levelResultTxt.color = Color.red;
            playTxt.text = "REPLAY";
        }

        ViewManager.Instance.MoveRect(topBarTrans, topBarTrans.anchoredPosition, new Vector2(topBarTrans.anchoredPosition.x, 0), 0.5f);
        ViewManager.Instance.ScaleRect(levelResultTxt.rectTransform, Vector2.zero, Vector2.one, 0.75f);
        StartCoroutine(CRShowBottomBtns());
        nextLevelTxt.text = "NEXT LEVEL: " + PlayerPrefs.GetInt(PlayerPrefsKey.SAVED_LEVEL_PPK);      
    }


    private void OnDisable()
    {
        topBarTrans.anchoredPosition = new Vector2(topBarTrans.anchoredPosition.x, 200);
        levelResultTxt.rectTransform.localScale = Vector2.zero;

        playBtnTrans.localScale = Vector3.zero;
        homeBtnTrans.anchoredPosition = new Vector2(homeBtnTrans.anchoredPosition.x, -200);
    }


    private IEnumerator CRShowBottomBtns()
    {
        ViewManager.Instance.ScaleRect(playBtnTrans, Vector2.zero, Vector2.one, 0.5f);
        yield return new WaitForSeconds(0.15f);
        ViewManager.Instance.MoveRect(homeBtnTrans, homeBtnTrans.anchoredPosition, new Vector2(homeBtnTrans.anchoredPosition.x, 150), 0.5f);
    }


    public void PlayBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
        ViewManager.Instance.LoadScene("Ingame", 0.15f);
    }

    public void HomeBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
        ViewManager.Instance.LoadScene("Home", 0.15f);
    }
}
