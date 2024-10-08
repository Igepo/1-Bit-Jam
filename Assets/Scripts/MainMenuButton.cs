using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public RectTransform transitionPanel;
    public float transitionDuration = 1f;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transitionPanel.anchoredPosition;

        transitionPanel.anchoredPosition = new Vector3(-transitionPanel.rect.width, 0, 0);
    }

    public void OnButtonPressed()
    {
        StartCoroutine(TransitionAndLoadScene("1"));
    }

    private IEnumerator TransitionAndLoadScene(string sceneName)
    {
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
