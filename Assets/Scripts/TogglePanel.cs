using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject victoryScreenUI;
    public GameObject defeatScreenUI;
    public GameObject objectToDisable;

    private bool isPaused = false;

    private void Start()
    {
        if(pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !victoryScreenUI.activeSelf && !defeatScreenUI.activeSelf)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        objectToDisable.SetActive(true);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        objectToDisable.SetActive(false);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
