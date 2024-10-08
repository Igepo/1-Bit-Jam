using UnityEngine;

public class DualParticleSoundSync : MonoBehaviour
{
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    public AudioSource audioSource;
    public AudioClip particleSound;

    void Start()
    {
        var lifetime1 = particleSystem1.main.startLifetime;
        var lifetime2 = particleSystem2.main.startLifetime;

        float randomLifetime1 = Random.Range(lifetime1.constantMin, lifetime1.constantMax);
        float randomLifetime2 = Random.Range(lifetime2.constantMin, lifetime2.constantMax);

        particleSystem1.Play();
        particleSystem2.Play();

        Invoke("PlaySoundForSystem1", randomLifetime1);

        Invoke("PlaySoundForSystem2", randomLifetime2);
    }

    private void PlaySoundForSystem1()
    {
        audioSource.PlayOneShot(particleSound);
    }

    private void PlaySoundForSystem2()
    {
        audioSource.PlayOneShot(particleSound);
    }
}
