using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI endMessageText;
    private NavigationScriptKing navigationScriptKing;

    public RectTransform transitionPanel;
    public float transitionDuration = 1f;
    private Vector3 initialPosition;

    public GameObject fxToDisable;
    public GameObject levelSelectionPanel;
    private void Awake()
    {
        navigationScriptKing = FindObjectOfType<NavigationScriptKing>();

        initialPosition = transitionPanel.anchoredPosition;

        transitionPanel.anchoredPosition = new Vector3(-transitionPanel.rect.width, 0, 0);

    }
    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        
        if (levelSelectionPanel != null)
            levelSelectionPanel.SetActive(false);
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

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            string nextSceneName = SceneManager.GetSceneByBuildIndex(currentSceneIndex + 1).name;
            StartCoroutine(TransitionAndLoadScene(nextSceneName));
        }

        //SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        string nextSceneName = SceneManager.GetSceneByBuildIndex(currentSceneIndex).name;
        StartCoroutine(TransitionAndLoadScene(nextSceneName));

        //SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(TransitionAndLoadScene("MainMenuScene"));
    }

    public void MainMenuPlayButton() 
    {
        //string firstScene = SceneManager.GetSceneByBuildIndex(1).name;
        //Debug.Log("firstScene " + firstScene);
        //StartCoroutine(TransitionAndLoadScene(firstScene));
        StartCoroutine(TransitionAndLoadScene("Level_01"));
    }

    #region LevelSelection
    public void MainMenuLevelSelectionButton()
    {
        levelSelectionPanel.SetActive(true);
    }

    public void MainMenuLevelSelectionBackButton()
    {
        levelSelectionPanel.SetActive(false);
    }

    public void MainMenuLevelSelectionFirstButton()
    {

        StartCoroutine(TransitionAndLoadScene("Level_01"));
    }
    public void MainMenuLevelSelectionSecondButton()
    {
        StartCoroutine(TransitionAndLoadScene("Level_02"));
    }
    public void MainMenuLevelSelectionThirdButton()
    {
        StartCoroutine(TransitionAndLoadScene("Level_03"));
    }

    #endregion

    private IEnumerator TransitionAndLoadScene(string sceneName)
    {
        if (fxToDisable != null)
            fxToDisable.SetActive(false);

        yield return StartCoroutine(MovePanelToRight());
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator MovePanelToRight()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transitionPanel.anchoredPosition;
        Vector3 endPosition = initialPosition;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            transitionPanel.anchoredPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        transitionPanel.anchoredPosition = endPosition;
    }
}
