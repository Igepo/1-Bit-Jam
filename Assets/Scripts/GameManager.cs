using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public SoundManager soundManager;

    private int collisionCount = 0;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        ChessPiece.OnEnemyDied += OnEnemyDied;
        Player.OnPlayerCollision += OnPlayerCollision;
        ChessPiece.OnCollisionWithPlayer += OnCollisionWithPlayer;
    }

    private void OnDisable()
    {
        ChessPiece.OnEnemyDied -= OnEnemyDied;
        Player.OnPlayerCollision -= OnPlayerCollision;
        ChessPiece.OnCollisionWithPlayer -= OnCollisionWithPlayer;
    }

    void Start()
    {
        soundManager.PlayMusic();
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

        collisionCount++;
        soundManager.PlayCollisionSound(impactForceMagnitudeClamp, collisionCount); // Appel au SoundManager
        CameraManager.Instance.TriggerShake(impactForceMagnitudeClamp);
    }

    private void OnCollisionWithPlayer(float impactForceMagnitude)
    {
        if (tag == "Wall") return;
        var impactForceMagnitudeClamp = Mathf.Clamp(impactForceMagnitude, 0f, 5000f);

        collisionCount++;
        soundManager.PlayCollisionSound(impactForceMagnitudeClamp, collisionCount); // Appel au SoundManager
        CameraManager.Instance.TriggerShake(impactForceMagnitudeClamp);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount = Mathf.Max(0, collisionCount - 1);
    }

}
