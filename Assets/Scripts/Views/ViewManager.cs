using MirkoZambito;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }

    [SerializeField] private HomeViewController homeViewControl = null;
    [SerializeField] private LoadingViewController loadingViewControl = null;
    [SerializeField] private IngameViewController ingameViewControl = null;
    public HomeViewController HomeViewController
    {
        get { return homeViewControl; }
    }
    public LoadingViewController LoadingViewController
    {
        get { return loadingViewControl; }
    }
    public IngameViewController IngameViewController
    {
        get { return ingameViewControl; }
    }

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private IEnumerator CRMovingRect(RectTransform rect, Vector2 startPos, Vector2 endPos, float movingTime)
    {
        Vector2 currentPos = new Vector2(Mathf.RoundToInt(rect.anchoredPosition.x), Mathf.RoundToInt(rect.anchoredPosition.y));
        if (!currentPos.Equals(endPos))
        {
            rect.anchoredPosition = startPos;
            float t = 0;
            while (t < movingTime)
            {
                t += Time.deltaTime;
                float factor = EasyType.MatchedLerpType(LerpType.EaseInOutQuart, t / movingTime);
                rect.anchoredPosition = Vector2.Lerp(startPos, endPos, factor);
                yield return null;
            }
        }
    }

    private IEnumerator CRScalingRect(RectTransform rect, Vector2 startScale, Vector2 endScale, float scalingTime)
    {
        rect.localScale = startScale;
        float t = 0;
        while (t < scalingTime)
        {
            t += Time.deltaTime;
            float factor = EasyType.MatchedLerpType(LerpType.EaseInOutQuart, t / scalingTime);
            rect.localScale = Vector2.Lerp(startScale, endScale, factor);
            yield return null;
        }
    }

    private IEnumerator CRFadingImage(Image image, float startAlpha, float endAlpha, float fadingTime)
    {
        Color startColor = image.color;
        startColor.a = startAlpha;
        image.color = startColor;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);
        float t = 0;
        while (t < fadingTime)
        {
            t += Time.deltaTime;
            float factor = EasyType.MatchedLerpType(LerpType.EaseInOutQuart, t / fadingTime);
            image.color = Color.Lerp(startColor, endColor, factor);
            yield return null;
        }
    }


    private IEnumerator CRLoadingScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneLoader.SetTargetScene(sceneName);
        SceneManager.LoadScene("Loading");
    }

    /////////////////////////////////////////////////////////////////////////////////Public functions


    /// <summary>
    /// Play the sound effect when you click a button.
    /// </summary>
    public void PlayClickButtonSound()
    {
        ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.button);
    }


    /// <summary>
    /// Move the given rect transform from startPos to endPos with movingTime,
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="movingTime"></param>
    public void MoveRect(RectTransform rect, Vector2 startPos, Vector2 endPos, float movingTime)
    {
        StartCoroutine(CRMovingRect(rect, startPos, endPos, movingTime));
    }


    /// <summary>
    /// Scale the given rect from startScale to endScale with scalingTime.
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="startScale"></param>
    /// <param name="endScale"></param>
    /// <param name="scalingTime"></param>
    public void ScaleRect(RectTransform rect, Vector2 startScale, Vector2 endScale, float scalingTime)
    {
        StartCoroutine(CRScalingRect(rect, startScale, endScale, scalingTime));
    }

    /// <summary>
    /// Load Loading scene with a delay time then use LoadSceneAsync to load the given scene.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="delay"></param>
    public void LoadScene(string sceneName, float delay)
    {
        StartCoroutine(CRLoadingScene(sceneName, delay));
    }





    /// <summary>
    /// Show the correct view base on sceneName.
    /// </summary>
    /// <param name="sceneName"></param>
    public void OnLoadingSceneDone(string sceneName)
    {
        //Load HomeView
        if (sceneName.Equals("Home"))
        {
            homeViewControl.gameObject.SetActive(true);
            homeViewControl.OnShow();

            //Hide all other views
            loadingViewControl.gameObject.SetActive(false);
            ingameViewControl.gameObject.SetActive(false);
        }
        else if (sceneName.Equals("Ingame"))
        {
            ingameViewControl.gameObject.SetActive(true);
            ingameViewControl.OnShow();

            //Hide all other views
            loadingViewControl.gameObject.SetActive(false);
            homeViewControl.gameObject.SetActive(false);
        }
        else if (sceneName.Equals("Loading"))
        {
            loadingViewControl.gameObject.SetActive(true);
            loadingViewControl.OnShow();

            //Hide all other views
            ingameViewControl.gameObject.SetActive(false);
            homeViewControl.gameObject.SetActive(false);
        }
    }
}
