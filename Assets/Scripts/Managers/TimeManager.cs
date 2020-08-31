using UnityEngine;
using UnityEngine.UI;

namespace MirkoZambito
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float timeRemaining;
        private float timeAvailable;
        [SerializeField] private Text timeText;
        private static bool _timerIsRunning;

        private void Start()
        {
            timeText.gameObject.SetActive(false);
            timeAvailable = timeRemaining;
            IngameManager.GameStateChanged += GameManager_GameStateChanged;
        }

        private void GameManager_GameStateChanged(IngameState obj)
        {
            if (IngameManager.Instance.IngameState == IngameState.Ingame_Playing)
            {
                timeText.gameObject.SetActive(true);
                StartTimer();
            }
            
            else if (obj == IngameState.Ingame_GameOver || obj == IngameState.Ingame_CompletedLevel)
            {
                timeText.gameObject.SetActive(false);
                _timerIsRunning = false;
                timeRemaining = timeAvailable;
            }
        }

        public static void StartTimer()
        {
            _timerIsRunning = true;
        }

        private void Update()
        {
            if (!_timerIsRunning) return;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                IngameManager.Instance.GameOver();
                timeRemaining = 0;
                _timerIsRunning = false;
            }
        }

        private void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}