using UnityEngine;

namespace FunnyBlox.GUI
{
    public class GUIService : MonoBehaviour
    {
        [SerializeField] private GUICanvasGroup menuCanvas;
        [SerializeField] private GUICanvasGroup gameCanvas;
        [SerializeField] private GUICanvasGroup gamewinCanvas;
        [SerializeField] private GUICanvasGroup gameoverCanvas;
        
        private void OnEnable()
        {
            EventsService.OnGameStart += OnGameStart;
            EventsService.OnGameWin += OnGameWin;
            EventsService.OnGameOver += OnGameOver;
            EventsService.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            EventsService.OnGameStart -= OnGameStart;
            EventsService.OnGameWin -= OnGameWin;
            EventsService.OnGameOver -= OnGameOver;
            EventsService.OnGameRestart -= OnGameRestart;
        }

        private void Start()
        {
            menuCanvas.Show();
            gameCanvas.Hide();
            gamewinCanvas.Hide();
            gameoverCanvas.Hide();
        }

        private void OnGameStart()
        {
            menuCanvas.Hide();
            gameCanvas.Show();
        }

        private void OnGameWin()
        {
            gameCanvas.Hide();
            gamewinCanvas.Show();
        }

        private void OnGameOver()
        {
            gameCanvas.Hide();
            gameoverCanvas.Show();
        }

        private void OnGameRestart()
        {
            gamewinCanvas.Hide();
            gameoverCanvas.Hide();
            menuCanvas.Show();

        }
    }
}