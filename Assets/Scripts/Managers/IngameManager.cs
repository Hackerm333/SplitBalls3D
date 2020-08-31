using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirkoZambito;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{

    public static IngameManager Instance { private set; get; }
    public static event System.Action<IngameState> GameStateChanged = delegate { };
    public static StageType SelectedStageType { private set; get; }
    public IngameState IngameState
    {
        get
        {
            return ingameState;
        }
        private set
        {
            if (value != ingameState)
            {
                ingameState = value;
                GameStateChanged(ingameState);
            }
        }
    }


    [Header("Enter a number of level to test. Set back to 0 to disable this feature.")]
    [SerializeField] private int testingLevel = 0;



    [Header("Ingame Config")]
    [SerializeField] private float reviveWaitTime = 5f;
    [SerializeField] private float rotatingSpeed = 4f;

    [Header("Levels Config")]
    [SerializeField] private List<LevelConfig> listLevelConfig = new List<LevelConfig>();

    [Header("Ingame References")]
    [SerializeField] private CameraRootController cameraRootControl = null;
    [SerializeField] private Material backgroundMaterial = null;
    [SerializeField] private Transform completedLevelEffectsTrans = null;
    [SerializeField] private ParticleSystem[] completedLevelEffects = null;


    public float ReviveWaitTime { get { return reviveWaitTime; } }
    public float RotatingSpeed { get { return rotatingSpeed; } }
    public float ForceDir { private set; get; }
    public float ForceTurn { private set; get; }
    public int CurrentLevel { private set; get; }
    public bool IsRevived { private set; get; }



    private IngameState ingameState = IngameState.Ingame_GameOver;

    private List<StageController> listStageControl = new List<StageController>();
    private List<BallContainerController> listBallContainerControl = new List<BallContainerController>();
    private StageController previousStageControl = null;
    private SoundClip background = null;
    private Coroutine cRCheckingGameOver = null;
    private float ballContainerSizeY = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(Instance.gameObject);
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // Use this for initialization
    private void Start()
    {
        Application.targetFrameRate = 60;
        ViewManager.Instance.OnLoadingSceneDone(SceneManager.GetActiveScene().name);

        //Setup variables
        IsRevived = false;
        ForceTurn = 1;
        ballContainerSizeY = PoolManager.Instance.GetBallContainerControl().GetSizeY();
        completedLevelEffectsTrans.gameObject.SetActive(false);

        //Set current level
        if (!PlayerPrefs.HasKey(PlayerPrefsKey.SAVED_LEVEL_PPK))
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.SAVED_LEVEL_PPK, 1);
        }

        //Load parameters
        CurrentLevel = (testingLevel != 0) ? testingLevel : PlayerPrefs.GetInt(PlayerPrefsKey.SAVED_LEVEL_PPK);
        foreach (LevelConfig o in listLevelConfig)
        {
            if (CurrentLevel == o.LevelNumber)
            {
                background = o.MusicClip;
                backgroundMaterial.SetColor("_TopColor", o.BackgroundTopColor);
                backgroundMaterial.SetColor("_BottomColor", o.BackgroundBottomColor);

                Vector3 currentstagePos = Vector3.zero;

                Material blockerMat = new Material(Shader.Find("EasyShader/Self Illumin Diffuse"));
                blockerMat.SetColor("_Color", o.BlockerColor);


                Material duplicatorMat = new Material(Shader.Find("EasyShader/Self Illumin Diffuse"));
                duplicatorMat.SetColor("_Color", o.DuplicatorColor);


                Material lockerMat = new Material(Shader.Find("EasyShader/Self Illumin Diffuse"));
                lockerMat.SetColor("_Color", o.LockerColor);

                Material ballContainerMat = new Material(Shader.Find("EasyShader/Self Illumin Diffuse"));
                ballContainerMat.SetColor("_Color", o.LockerColor);


                for (int i = 0; i < o.ListStageControlPrefab.Count; i++)
                {
                    //Create the stage
                    Quaternion quaternion = (i % 2 == 0) ? Quaternion.identity : Quaternion.Euler(new Vector3(0, 180f, 0));
                    StageController stageControl = Instantiate(o.ListStageControlPrefab[i], currentstagePos, quaternion);
                    stageControl.transform.position = currentstagePos;
                    stageControl.gameObject.SetActive(true);
                    listStageControl.Add(stageControl);

                    //Create the ball container object
                    Vector3 ballContainerPos = stageControl.GetBottomPosition();
                    BallContainerController ballContainerControl = PoolManager.Instance.GetBallContainerControl();
                    ballContainerControl.transform.position = ballContainerPos;
                    ballContainerControl.gameObject.SetActive(true);
                    ballContainerControl.SetMaterial(ballContainerMat);
                    listBallContainerControl.Add(ballContainerControl);

                    //Set the position for complete level effects
                    if (i == o.ListStageControlPrefab.Count - 1)
                    {
                        completedLevelEffectsTrans.position = ballContainerPos;
                    }

                    //Update stage position
                    currentstagePos = stageControl.GetBottomPosition() + Vector3.down * (ballContainerSizeY + stageControl.GetSize().y / 2f);

                    stageControl.SetBlockerMaterial(blockerMat);
                    stageControl.SetDuplicatorMaterial(duplicatorMat);
                    stageControl.SetLockerMaterial(lockerMat);
                }

                break;
            }
        }

        listStageControl[0].CreateBall();
        previousStageControl = listStageControl[0];
        listStageControl.RemoveAt(0);
        previousStageControl.OnSetup();
        cRCheckingGameOver = StartCoroutine(CRCheckingGameOver());
        PlayingGame();
    }



    /// <summary>
    /// Actual start the game (call Ingame_Playing event).
    /// </summary>
    public void PlayingGame()
    {
        //Fire event
        IngameState = IngameState.Ingame_Playing;
        ingameState = IngameState.Ingame_Playing;

        //Other actions

        if (IsRevived)
        {
            ResumeBackgroundMusic(0.5f);
            previousStageControl.CreateBall();
            cameraRootControl.ResetRotationToDefault();
            cRCheckingGameOver = StartCoroutine(CRCheckingGameOver());
        }
        else
        {
            PlayBackgroundMusic(0.5f);
        }
    }


    /// <summary>
    /// Call Ingame_Revive event.
    /// </summary>
    public void Revive()
    {
        //Fire event
        IngameState = IngameState.Ingame_Revive;
        ingameState = IngameState.Ingame_Revive;

        //Add another actions here
        PauseBackgroundMusic(0.5f);
    }


    /// <summary>
    /// Call Ingame_GameOver event.
    /// </summary>
    public void GameOver()
    {
        //Fire event
        IngameState = IngameState.Ingame_GameOver;
        ingameState = IngameState.Ingame_GameOver;

        //Add another actions here
        StopBackgroundMusic(0f);
    }


    /// <summary>
    /// Call Ingame_CompletedLevel event.
    /// </summary>
    public void CompletedLevel()
    {
        //Fire event
        IngameState = IngameState.Ingame_CompletedLevel;
        ingameState = IngameState.Ingame_CompletedLevel;

        //Other actions
        StopBackgroundMusic(0f);

        //Save level
        if (testingLevel == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.SAVED_LEVEL_PPK, PlayerPrefs.GetInt(PlayerPrefsKey.SAVED_LEVEL_PPK) + 1);
        }
    }

    private void PlayBackgroundMusic(float delay)
    {
        StartCoroutine(CRPlayBGMusic(delay));
    }

    private IEnumerator CRPlayBGMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        ServicesManager.Instance.SoundManager.PlayMusic(background, 0.5f);
    }

    private void StopBackgroundMusic(float delay)
    {
        StartCoroutine(CRStopBGMusic(delay));
    }

    private IEnumerator CRStopBGMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        ServicesManager.Instance.SoundManager.StopMusic(0.5f);
    }

    private void PauseBackgroundMusic(float delay)
    {
        StartCoroutine(CRPauseBGMusic(delay));
    }

    private IEnumerator CRPauseBGMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        ServicesManager.Instance.SoundManager.PauseMusic();
    }

    private void ResumeBackgroundMusic(float delay)
    {
        StartCoroutine(CRResumeBGMusic(delay));
    }

    private IEnumerator CRResumeBGMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        ServicesManager.Instance.SoundManager.ResumeMusic();
    }


    /// <summary>
    /// Play the given particle and disable it.
    /// </summary>
    /// <param name="par"></param>
    /// <returns></returns>
    private IEnumerator CRPlayParticle(ParticleSystem par)
    {
        par.Play();
        yield return new WaitForSeconds(par.main.startLifetimeMultiplier);
        par.gameObject.SetActive(false);
    }



    /// <summary>
    /// Wait for amount of time then move the Ball_Container object dow and create a ball.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CRWaitAndCreateBall()
    {
        listStageControl[0].OnSetup();
        yield return new WaitForSeconds(1f);
        listBallContainerControl[0].MoveDownAndUp();
        previousStageControl.DisableObject();
        yield return new WaitForSeconds(0.55f);
        listStageControl[0].CreateBall();
        previousStageControl = listStageControl[0];
        listStageControl.RemoveAt(0);
        listBallContainerControl.RemoveAt(0);
        ForceDir = 0;
        ForceTurn *= -1;
        cRCheckingGameOver = StartCoroutine(CRCheckingGameOver());
    }



    /// <summary>
    /// Checking and handle GameOver state or Revive state. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator CRCheckingGameOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            BallController[] ballControls = FindObjectsOfType<BallController>();
            
            if (ballControls.Length == 0)
            {
                if (IsRevived)
                {
                    GameOver();
                }
                else
                {
                    Revive();
                }

                yield break;
            }
        }
    }



    /// <summary>
    /// Checking complete stage.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CRCheckingCompleteStage()
    {
        while (true)
        {
            yield return null;
            BallController[] ballControllers = FindObjectsOfType<BallController>();
            if (ballControllers.Length == 0)
            {
                if (listStageControl.Count > 0)
                {
                    ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.finishedOneStage);
                    cameraRootControl.ResetRotationandMoveDown();
                    StartCoroutine(CRWaitAndCreateBall());
                    ViewManager.Instance.IngameViewController.PlayingViewControl.UpdateLevelProgressUI();
                }
                else
                {
                    ViewManager.Instance.IngameViewController.PlayingViewControl.UpdateLevelProgressUI();

                    //Play effects
                    completedLevelEffectsTrans.gameObject.SetActive(true);
                    foreach (ParticleSystem o in completedLevelEffects)
                    {
                        StartCoroutine(CRPlayParticle(o));
                    }

                    StartCoroutine(CRWaitAndCallCompleteLevel());
                }

                yield break;
            }
        }
    }



    /// <summary>
    /// Wait for amount of time then call the CompletedLevel state.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CRWaitAndCallCompleteLevel()
    {
        ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.completedLevel);
        yield return new WaitForSeconds(1f);
        CompletedLevel();
    }


    //////////////////////////////////////Publish functions


    /// <summary>
    /// Continue the game
    /// </summary>
    public void SetContinueGame()
    {
        IsRevived = true;
        PlayingGame();
    }



    /// <summary>
    /// Set the SelectedStageType to the given stageType.
    /// </summary>
    /// <param name="stageType"></param>
    public static void SetStageType(StageType stageType)
    {
        SelectedStageType = stageType;
    }

    /// <summary>
    /// Check and handle actions when there's no ball left on the scene.
    /// </summary>
    public void CheckOutOfBall()
    {
        if (cRCheckingGameOver != null)
        {
            StopCoroutine(cRCheckingGameOver);
            cRCheckingGameOver = null;

            StartCoroutine(CRCheckingCompleteStage());
        }
    }


    /// <summary>
    /// Set the force direction.
    /// </summary>
    /// <param name="dir"></param>
    public void SetForceDir(int dir)
    {
        ForceDir = dir;
    }
}
