using MirkoZambito;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class HomeViewController : MonoBehaviour {

    [SerializeField] private RectTransform topBarTrans = null;
    [SerializeField] private RectTransform gameNameTrans = null;
    [SerializeField] private RectTransform playBtnTrans = null;
    [SerializeField] private RectTransform soundBtnsTrans = null;
    [SerializeField] private RectTransform musicBtnsTrans = null;
    [SerializeField] private GameObject soundOnBtn = null;
    [SerializeField] private GameObject soundOffBtn = null;
    [SerializeField] private GameObject musicOnBtn = null;
    [SerializeField] private GameObject musicOffBtn = null;
    [SerializeField] private Text currentLevelTxt = null;
    [SerializeField] private EnvironmentViewController environmentViewControl = null;

    public void OnShow()
    {
        ViewManager.Instance.MoveRect(topBarTrans, topBarTrans.anchoredPosition, new Vector2(topBarTrans.anchoredPosition.x, 0), 0.5f);
        ViewManager.Instance.ScaleRect(gameNameTrans, Vector2.zero, Vector2.one, 1f);
        StartCoroutine(CRShowingBottomButtons());

        environmentViewControl.gameObject.SetActive(false);

        currentLevelTxt.text = "LEVEL: " + PlayerPrefs.GetInt(PlayerPrefsKey.SAVED_LEVEL_PPK, 1).ToString();

        //Update sound btns
        if (ServicesManager.Instance.SoundManager.IsSoundOff())
        {
            soundOnBtn.gameObject.SetActive(false);
            soundOffBtn.gameObject.SetActive(true);
        }
        else
        {
            soundOnBtn.gameObject.SetActive(true);
            soundOffBtn.gameObject.SetActive(false);
        }

        //Update music btns
        if (ServicesManager.Instance.SoundManager.IsMusicOff())
        {
            musicOffBtn.gameObject.SetActive(true);
            musicOnBtn.gameObject.SetActive(false);
        }
        else
        {
            musicOffBtn.gameObject.SetActive(false);
            musicOnBtn.gameObject.SetActive(true);
        }
    }


    private void OnDisable()
    {
        topBarTrans.anchoredPosition = new Vector2(topBarTrans.anchoredPosition.x, 100);
        gameNameTrans.localScale = Vector2.zero;

        playBtnTrans.localScale = Vector3.zero;
        soundBtnsTrans.anchoredPosition = new Vector2(soundBtnsTrans.anchoredPosition.x, -200);
        musicBtnsTrans.anchoredPosition = new Vector2(musicBtnsTrans.anchoredPosition.x, -200);
    }



    /// <summary>
    /// Handle the Home view when Environment view closes.
    /// </summary>
    public void OnSubViewClose()
    {
        ViewManager.Instance.ScaleRect(gameNameTrans, Vector2.zero, Vector2.one, 1f);
        StartCoroutine(CRShowingBottomButtons());
    }


    private IEnumerator CRShowingBottomButtons()
    {
        ViewManager.Instance.ScaleRect(playBtnTrans, Vector2.zero, Vector2.one, 0.5f);
        yield return new WaitForSeconds(0.15f);
        ViewManager.Instance.MoveRect(soundBtnsTrans, soundBtnsTrans.anchoredPosition, new Vector2(soundBtnsTrans.anchoredPosition.x, 150), 0.5f);
        yield return new WaitForSeconds(0.15f);
        yield return new WaitForSeconds(0.15f);
        ViewManager.Instance.MoveRect(musicBtnsTrans, musicBtnsTrans.anchoredPosition, new Vector2(musicBtnsTrans.anchoredPosition.x, 150), 0.5f);
    }


    //////////////////////////////////////////////////////////////////////UI Functions


    public void PlayBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
        StartCoroutine(CRHandlePlayBtn());
    }
    private IEnumerator CRHandlePlayBtn()
    {
        ViewManager.Instance.ScaleRect(gameNameTrans, Vector2.one, Vector2.zero, 0.5f);
        ViewManager.Instance.ScaleRect(playBtnTrans, Vector2.one, Vector2.zero, 0.5f);
        yield return new WaitForSeconds(0.1f);
        ViewManager.Instance.MoveRect(soundBtnsTrans, soundBtnsTrans.anchoredPosition, new Vector2(soundBtnsTrans.anchoredPosition.x, -200), 0.5f);
        ViewManager.Instance.MoveRect(musicBtnsTrans, musicBtnsTrans.anchoredPosition, new Vector2(musicBtnsTrans.anchoredPosition.x, -200), 0.5f);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.1f);
        environmentViewControl.gameObject.SetActive(true);
        environmentViewControl.OnShow();
    }

    public void ToggleSound()
    {
        ViewManager.Instance.PlayClickButtonSound();
        ServicesManager.Instance.SoundManager.ToggleSound();
        if (ServicesManager.Instance.SoundManager.IsSoundOff())
        {
            soundOnBtn.gameObject.SetActive(false);
            soundOffBtn.gameObject.SetActive(true);
        }
        else
        {
            soundOnBtn.gameObject.SetActive(true);
            soundOffBtn.gameObject.SetActive(false);
        }
    }

    public void ToggleMusic()
    {
        ViewManager.Instance.PlayClickButtonSound();
        ServicesManager.Instance.SoundManager.ToggleMusic();
        if (ServicesManager.Instance.SoundManager.IsMusicOff())
        {
            musicOffBtn.gameObject.SetActive(true);
            musicOnBtn.gameObject.SetActive(false);
        }
        else
        {
            musicOffBtn.gameObject.SetActive(false);
            musicOnBtn.gameObject.SetActive(true);
        }
    }
}
