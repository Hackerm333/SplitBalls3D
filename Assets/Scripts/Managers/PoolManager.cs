using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MirkoZambito;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { private set; get; }

    [FormerlySerializedAs("duplicatorTextControlPrefab")] [SerializeField] private SplitterTextController splitterTextControlPrefab = null;
    [SerializeField] private LockerTextController lockerTextControlPrefab = null;
    [SerializeField] private BallContainerController ballContainerControlPrefab = null;
    [SerializeField] private BallController[] ballControlPrefabs = null;
    [SerializeField] private List<StageSelectionData> listStageSelectionData = new List<StageSelectionData>();

    private List<SplitterTextController> listDuplicatorTextControl = new List<SplitterTextController>();
    private List<LockerTextController> listLockerTextControl = new List<LockerTextController>();
    private List<BallContainerController> listBallContainerControl = new List<BallContainerController>();
    private List<BallController> listBallControl = new List<BallController>();

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


    /// <summary>
    /// Get the mesh of the stage object base on IngameManager.SelectedStageType.
    /// </summary>
    /// <returns></returns>
    public Mesh GetStageMesh()
    {
        foreach (StageSelectionData o in listStageSelectionData)
        {
            if (o.StageType == IngameManager.SelectedStageType)
            {
                return o.StageMesh;
            }
        }

        return listStageSelectionData[0].StageMesh;
    }


    /// <summary>
    /// Get the material of the stage object base on IngameManager.SelectedStageType.
    /// </summary>
    /// <returns></returns>
    public Material GetStageMaterial()
    {
        foreach (StageSelectionData o in listStageSelectionData)
        {
            if (o.StageType == IngameManager.SelectedStageType)
            {
                return o.StageMaterial;
            }
        }

        return listStageSelectionData[0].StageMaterial;
    }


    /// <summary>
    /// Get an inactive BallController object with given BallSize.
    /// </summary>
    /// <returns></returns>
    public BallController GetBallControl(BallSize ballSize)
    {
        //Find in the list
        BallController bigBallControl = listBallControl.Where((a => !a.gameObject.activeInHierarchy && a.BallSize.Equals(ballSize))).FirstOrDefault();

        if (bigBallControl == null)
        {
            //Did not find one -> create new one
            BallController ballControlPrefab = ballControlPrefabs.Where(a => a.BallSize.Equals(ballSize)).FirstOrDefault();
            bigBallControl = Instantiate(ballControlPrefab, Vector3.zero, Quaternion.identity);
            bigBallControl.gameObject.SetActive(false);
            listBallControl.Add(bigBallControl);
        }

        return bigBallControl;
    }


    /// <summary>
    /// Get an inactive DuplicatorTextController object.
    /// </summary>
    /// <returns></returns>
    public SplitterTextController GetDuplicatorTextControl()
    {
        //Find in the list
        SplitterTextController textObjext = listDuplicatorTextControl.Where(a => !a.gameObject.activeInHierarchy).FirstOrDefault();

        if (textObjext == null)
        {
            //Did not find one -> create new one
            textObjext = Instantiate(splitterTextControlPrefab, Vector3.zero, Quaternion.identity);
            textObjext.gameObject.SetActive(false);
            listDuplicatorTextControl.Add(textObjext);
        }

        return textObjext;
    }


    /// <summary>
    /// Get an inactive LockerTextController object.
    /// </summary>
    /// <returns></returns>
    public LockerTextController GetLockerTextControl()
    {
        //Find in the list
        LockerTextController textObjext = listLockerTextControl.Where(a => !a.gameObject.activeInHierarchy).FirstOrDefault();

        if (textObjext == null)
        {
            //Did not find one -> create new one
            textObjext = Instantiate(lockerTextControlPrefab, Vector3.zero, Quaternion.identity);
            textObjext.gameObject.SetActive(false);
            listLockerTextControl.Add(textObjext);
        }

        return textObjext;
    }


    /// <summary>
    /// Get an inactive BallContainerController object.
    /// </summary>
    /// <returns></returns>
    public BallContainerController GetBallContainerControl()
    {
        //Find in the list
        BallContainerController ballContainerControl = listBallContainerControl.Where(a => !a.gameObject.activeInHierarchy).FirstOrDefault();

        if (ballContainerControl == null)
        {
            //Did not find one -> create new one
            ballContainerControl = Instantiate(ballContainerControlPrefab, Vector3.zero, Quaternion.identity);
            ballContainerControl.gameObject.SetActive(false);
            listBallContainerControl.Add(ballContainerControl);
        }

        return ballContainerControl;
    }

}
