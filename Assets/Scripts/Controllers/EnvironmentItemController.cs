using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MirkoZambito;

public class EnvironmentItemController : MonoBehaviour {

    [Header("Config")]
    [SerializeField] private StageType stageType = StageType.GREEN;

    //[Header("References")]
    //[SerializeField] private Sprite background = null;


    public void SelectBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
        IngameManager.SetStageType(stageType);
        ViewManager.Instance.LoadScene("Ingame", 0.15f);
    }
}
