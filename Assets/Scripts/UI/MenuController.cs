using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _health;
    [SerializeField] private TextMeshProUGUI _menuTextMeshPro;
    [SerializeField] private string _levelCompleteText = "LEVEL COMPLETE";
    [SerializeField] private string _levelFailedText = "LEVEL FAILED";

    private void Awake()
    {
        FinishZone.OnFinishEnter.AddListener(LevelCompleteShow);
        StatsController.OnPlayerDeath.AddListener(LevelFailedShow);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LevelCompleteShow()
    {
        _health.SetActive(false);
        _menu.SetActive(true);
        _menuTextMeshPro.SetText(_levelCompleteText);
        _menuTextMeshPro.color = Color.green;
    }

    private void LevelFailedShow()
    {
        _health.SetActive(false);
        _menu.SetActive(true);
        _menuTextMeshPro.SetText(_levelFailedText);
        _menuTextMeshPro.color = Color.red;
    }
}
