using UnityEngine;
using MirkoZambito;
using System.Collections;

public class IngameViewController : MonoBehaviour {

    [SerializeField] private PlayingViewController playingViewControl = null;
    [SerializeField] private ReviveViewController reviveViewControl = null;
    [SerializeField] private EndGameViewController endGameViewControl = null;

    public PlayingViewController PlayingViewControl { get { return playingViewControl; } }
    public void OnShow()
    {
        IngameManager.GameStateChanged += GameManager_GameStateChanged;
    }

    private void OnDisable()
    {
        IngameManager.GameStateChanged -= GameManager_GameStateChanged;
    }

    private void GameManager_GameStateChanged(IngameState obj)
    { 
        if (obj == IngameState.Ingame_Revive)
        {
            StartCoroutine(CRShowReviveView());
        }
        else if (obj == IngameState.Ingame_GameOver || obj == IngameState.Ingame_CompletedLevel)
        {
            StartCoroutine(CRShowEndGameView());
        }
        else if (obj == IngameState.Ingame_Playing)
        {
            playingViewControl.gameObject.SetActive(true);
            playingViewControl.OnShow();

            reviveViewControl.gameObject.SetActive(false);
            endGameViewControl.gameObject.SetActive(false);
        }
    }

    private IEnumerator CRShowEndGameView()
    {
        yield return new WaitForSeconds(0.3f);
        endGameViewControl.gameObject.SetActive(true);
        endGameViewControl.OnShow();

        reviveViewControl.gameObject.SetActive(false);
        playingViewControl.gameObject.SetActive(false);
    }

    private IEnumerator CRShowReviveView()
    {
        yield return new WaitForSeconds(0.3f);
        reviveViewControl.gameObject.SetActive(true);
        reviveViewControl.OnShow();

        playingViewControl.gameObject.SetActive(false);
        endGameViewControl.gameObject.SetActive(false);
    }
}
