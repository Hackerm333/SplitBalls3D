using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    private static string targetScene = string.Empty;

    private void Start()
    {
        ViewManager.Instance.OnLoadingSceneDone(SceneManager.GetActiveScene().name);
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        int temp = 0;
        AsyncOperation asyn = SceneManager.LoadSceneAsync(targetScene);
        while (!asyn.isDone)
        {
            temp++;
            if (temp == 1)
                ViewManager.Instance.LoadingViewController.SetLoadingText("LOADING.");
            else if (temp == 2)
                ViewManager.Instance.LoadingViewController.SetLoadingText("LOADING..");
            else
            {
                ViewManager.Instance.LoadingViewController.SetLoadingText("LOADING...");
                temp = 0;
            }
            yield return new WaitForSeconds(0.05f);

            ViewManager.Instance.LoadingViewController.SetLoadingAmount(asyn.progress);
        }
    }

    /// <summary>
    /// Set target scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public static void SetTargetScene(string sceneName)
    {
        targetScene = sceneName;
    }
}
