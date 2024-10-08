using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScriptPawn : MonoBehaviour
{
    private Transform targetTransform;
    private GameObject targetGameObject;
    private NavMeshAgent agent;
    private Rigidbody _rigidbody;

    private bool isKnockedBack = false;
    private float knockbackDuration = 1f;

    public AudioClip[] moveSounds;
    private AudioSource audioSource;

    public float pauseDuration = 1f;
    public float moveDuration = 1f;

    private void Awake()
    {
        targetGameObject = GameObject.FindWithTag("Ally");
        targetTransform = targetGameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //var allyMaxHealth = collision.gameObject.GetComponent<King>().chessPieceData.maxHealth;

    }

    void Start()
    {
        agent.updateRotation = false;
        agent.updatePosition = false;

        StartCoroutine(MoveTowardsTarget());
    }
    IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            if (!isKnockedBack && agent.isActiveAndEnabled)
            {
                agent.SetDestination(targetTransform.position);

                agent.isStopped = false;

                yield return new WaitForSeconds(moveDuration);

                PlayRandomMoveSound();

                if (agent.isActiveAndEnabled)
                {
                    agent.isStopped = true;
                }
            }

            yield return new WaitForSeconds(pauseDuration);
        }
    }

    void PlayRandomMoveSound()
    {
        if (moveSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, moveSounds.Length);
            audioSource.clip = moveSounds[randomIndex];
            audioSource.Play();
        }
    }

    void Update()
    {
        if (!isKnockedBack)
        {
            agent.SetDestination(targetTransform.position);

            if (agent.isOnNavMesh)
            {
                _rigidbody.MovePosition(agent.nextPosition);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ally"))
        {
            Bounce(collision);
        }
    }

    void Bounce(Collision collision)
    {
        Rigidbody ennemyRigidbody = transform.gameObject.GetComponent<Rigidbody>();

        if (ennemyRigidbody != null)
        {
            Vector3 impactDirection = (collision.transform.position - transform.position).normalized;
            impactDirection.y = 0;

            Vector3 knockbackForce = impactDirection * ennemyRigidbody.mass * 50f;
            ennemyRigidbody.AddForce(-knockbackForce, ForceMode.Impulse);

            StartCoroutine(HandleKnockback());
        }
    }
    private IEnumerator HandleKnockback()
    {
        isKnockedBack = true;
        //agent.isStopped = true;
        agent.enabled = false;

        yield return new WaitForSeconds(knockbackDuration);

        agent.enabled = true;
        if (agent.isOnNavMesh)
        {
            agent.Warp(transform.position);
        }

        isKnockedBack = false;
    }
}
