using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public SoundManager soundManager;
    public Canvas canvasToDisable;
    public GameObject objectToDisable;

    private int collisionCount = 0;

    public GameObject victoryScreen;

    public RectTransform victoryPanel;
    public float transitionDuration = 1f;
    private Vector3 initialPosition;

    private int pawnDeadNumber;
    public TextMeshProUGUI pawnKilledText;
    private void Awake()
    {
        //initialPosition = victoryPanel.anchoredPosition;

        //victoryPanel.anchoredPosition = new Vector3(-victoryPanel.rect.width, 0, 0);
    }

    private void OnEnable()
    {
        ChessPiece.OnEnemyDied += OnEnemyDied;
        Player.OnPlayerCollision += OnPlayerCollision;
        ChessPiece.OnCollisionWithPlayer += OnCollisionWithPlayer;
        NavigationScriptKing.OnVictory += ShowVictoryScreen;
        TutorialFinished.OnTutorialFinished += ShowVictoryScreen;
        Pawn.OnPawnDie += OnPawnDie;
    }

    private void OnDisable()
    {
        ChessPiece.OnEnemyDied -= OnEnemyDied;
        Player.OnPlayerCollision -= OnPlayerCollision;
        ChessPiece.OnCollisionWithPlayer -= OnCollisionWithPlayer;
        NavigationScriptKing.OnVictory -= ShowVictoryScreen;
        TutorialFinished.OnTutorialFinished -= ShowVictoryScreen;
        Pawn.OnPawnDie -= OnPawnDie;
    }

    void Start()
    {
        soundManager.PlayMusic();
        victoryScreen.SetActive(false);
    }

    void OnPawnDie()
    {
        pawnDeadNumber++;
        Debug.Log(pawnDeadNumber);
    }
    void OnEnemyDied()
    {
        //player.IncreasedStats();
    }

    private void OnPlayerCollision(Collision collision)
    {
        if (tag == "Ennemy") return;
        Vector3 impactForce = collision.relativeVelocity * player.GetComponent<Rigidbody>().mass;
        float impactForceMagnitude = impactForce.magnitude;
        var impactForceMagnitudeClamp = Mathf.Clamp(impactForceMagnitude, 0f, 5000f);
        Debug.Log("OnPlayerCollision");

        collisionCount++;
        soundManager.PlayCollisionSound(impactForceMagnitudeClamp, collisionCount);
        CameraManager.Instance.TriggerShake(impactForceMagnitudeClamp);
    }

    private void OnCollisionWithPlayer(float impactForceMagnitude)
    {
        if (tag == "Wall") return;
        var impactForceMagnitudeClamp = Mathf.Clamp(impactForceMagnitude, 0f, 5000f);
        Debug.Log("OnCollisionWithPlayer");
        collisionCount++;
        soundManager.PlayCollisionSound(impactForceMagnitudeClamp, collisionCount);
        CameraManager.Instance.TriggerShake(impactForceMagnitudeClamp);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount = Mathf.Max(0, collisionCount - 1);
    }

    private void ShowVictoryScreen()
    {
        //StartCoroutine(TransitionPanel(victoryPanel));
        victoryScreen.SetActive(true);

        canvasToDisable.enabled = false;
        objectToDisable.SetActive(false);

        if (pawnKilledText != null)
            pawnKilledText.text = $"You captured {pawnDeadNumber} pawns!";
        //Time.timeScale = 0f;
    }

    private IEnumerator TransitionPanel(RectTransform panel)
    {
        yield return StartCoroutine(MovePanelToRight(panel));
        victoryScreen.SetActive(true);
    }

    private IEnumerator MovePanelToRight(RectTransform panel)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = panel.anchoredPosition;
        Vector3 endPosition = initialPosition;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            panel.anchoredPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        panel.anchoredPosition = endPosition;
    }
}
