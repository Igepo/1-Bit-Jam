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

    [SerializeField] float damageDealt = 10; // Dommage infligé par les pions
    private void Awake()
    {
        targetGameObject = GameObject.FindWithTag("Ally");
        targetTransform = targetGameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();

        //var allyMaxHealth = collision.gameObject.GetComponent<King>().chessPieceData.maxHealth;

    }

    void Start()
    {
        agent.updateRotation = false;
        agent.updatePosition = false;
    }

    bool isWaiting = false;

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
        agent.enabled = false;

        yield return new WaitForSeconds(knockbackDuration);

        if (agent.isOnNavMesh)
        {
            agent.Warp(transform.position);
        }

        agent.enabled = true;
        isKnockedBack = false;
    }
}
