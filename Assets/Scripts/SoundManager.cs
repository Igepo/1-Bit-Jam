using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectAudioSource;
    public AudioSource musicAudioSource;
    public AudioClip collisionSound;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        musicAudioSource.volume = 0.2f;
        musicAudioSource.clip = backgroundMusic;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    private void OnEnable()
    {
        NavigationScriptKing.OnVictory += VictoryVolume;
        King.OnGamelost += DefeatVolume;
    }

    private void OnDisable()
    {
        NavigationScriptKing.OnVictory -= VictoryVolume;
        King.OnGamelost -= DefeatVolume;
    }

    void VictoryVolume()
    {
        musicAudioSource.volume -= 0.15f;
    }
    void DefeatVolume()
    {
        musicAudioSource.volume -= 0.15f;
        musicAudioSource.pitch -= 0.1f;
    }

    public void PlayCollisionSound(float impactSpeed, int collisionCount)
    {
        float normalizedImpactSpeed = Mathf.Clamp(impactSpeed, 0f, 5000f) / 5000f;

        float baseVolume = Mathf.SmoothStep(0f, 0.2f, normalizedImpactSpeed);
        float volume = Mathf.Clamp(baseVolume / collisionCount, 0.1f, 1f);

        effectAudioSource.volume = volume;

        if (normalizedImpactSpeed < 0.1f)
        {
            effectAudioSource.pitch = 0.8f;
        }
        else
        {
            effectAudioSource.pitch = Mathf.Lerp(0.8f, 1.0f, normalizedImpactSpeed);
        }

        effectAudioSource.PlayOneShot(collisionSound);
    }

    public void PlayMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }
}
