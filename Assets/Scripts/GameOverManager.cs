using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI endMessageText;
    private NavigationScriptKing navigationScriptKing;

    private void Awake()
    {
        navigationScriptKing = FindObjectOfType<NavigationScriptKing>();
    }
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    public void DisplayGameOverScreen()
    {
        int percentageCompletedInt = Mathf.RoundToInt(navigationScriptKing.GetPercentageCompleted());

        endMessageText.text = $"The king moved {percentageCompletedInt}% of the way";

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        //gameOverPanel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
