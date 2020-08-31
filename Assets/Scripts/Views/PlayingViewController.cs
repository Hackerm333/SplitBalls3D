using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using MirkoZambito;

public class PlayingViewController : MonoBehaviour {

    [SerializeField] private RectTransform leftBarTrans = null;
    [SerializeField] private GameObject endImg = null;
    [SerializeField] private Text levelTxt = null;
    [SerializeField] private ProcessDotController processDotControlPrefab = null;


    private List<ProcessDotController> listProcessDotControl = new List<ProcessDotController>();
    private List<ProcessDotController> listProcessDotControlTemp = new List<ProcessDotController>();

    [SerializeField] private TimeManager timeManager;

    public void OnShow()
    {
        ViewManager.Instance.MoveRect(leftBarTrans, leftBarTrans.anchoredPosition, new Vector2(leftBarTrans.anchoredPosition.x, -5f), 0.5f);

        levelTxt.text = "LEVEL: " + PlayerPrefs.GetInt(PlayerPrefsKey.SAVED_LEVEL_PPK).ToString();

        if (!IngameManager.Instance.IsRevived)
        {
            listProcessDotControlTemp.Clear();

            //Create process dots
            int number = FindObjectsOfType<StageController>().Length;
            for (int i = 0; i < number; i++)
            {
                ProcessDotController processDotControl = GetProcessDotControl();
                processDotControl.transform.SetParent(leftBarTrans);
                processDotControl.transform.localScale = Vector3.one;
                processDotControl.gameObject.SetActive(true);
                processDotControl.ResetFilters();
                processDotControl.SetIndex(i + 1);

                listProcessDotControlTemp.Add(processDotControl);
            }

            endImg.transform.SetAsLastSibling();
        }
        else
        {
            foreach(ProcessDotController o in listProcessDotControlTemp)
            {
                o.gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        leftBarTrans.anchoredPosition = new Vector2(leftBarTrans.anchoredPosition.x, 50f);

        if (IngameManager.Instance != null)
        {
            foreach (ProcessDotController o in listProcessDotControl)
            {
                o.gameObject.SetActive(false);
            }
        }
    }




    /// <summary>
    /// Update the level progress UI.
    /// </summary>
    public void UpdateLevelProgressUI()
    {
        listProcessDotControlTemp[0].StartFilter();
        listProcessDotControlTemp.RemoveAt(0);
    }



    private ProcessDotController GetProcessDotControl()
    {
        //Find in the list
        ProcessDotController processDotControl = listProcessDotControl.Where(a => !a.gameObject.activeInHierarchy).FirstOrDefault();


        //Did not find one -> Create new one
        if (processDotControl == null)
        {
            processDotControl = Instantiate(processDotControlPrefab, Vector3.zero, Quaternion.identity);
            listProcessDotControl.Add(processDotControl);
            processDotControl.gameObject.SetActive(false);
        }

        return processDotControl;
    }
}
