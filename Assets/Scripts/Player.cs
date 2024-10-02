using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float _baseForce = 200f; // Vitesse initiale
    [SerializeField] private float _growthRate = 0.5f; // Taux de croissance exponentiel

    private Rigidbody _playerRigidbody;
    private Vector3 _currentDirection = Vector3.zero;
    private float _currentSpeed = 0f;
    private float _timeElapsed = 0f; // Temps écoulé

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += Movement_performed;
    }

    private void Start()
    {
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
        {
            _currentDirection = new Vector3(inputVector.x, 0, 0).normalized;
        }
        else
        {
            _currentDirection = new Vector3(0, 0, inputVector.y).normalized;
        }

        _timeElapsed = 0f; // Réinitialiser le temps lorsque le mouvement est déclenché
        _currentSpeed = _baseForce;
        _playerRigidbody.velocity = Vector3.zero;
        _playerRigidbody.AddForce(_currentDirection * _currentSpeed, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        float currentSpeedMagnitude = _playerRigidbody.velocity.magnitude;

        if (_currentDirection != Vector3.zero && currentSpeedMagnitude > 0.1f)
        {
            animator.Play("Angry");

            _timeElapsed += Time.fixedDeltaTime;
            _currentSpeed = _baseForce * Mathf.Exp(_growthRate * _timeElapsed);

            _playerRigidbody.AddForce(_currentDirection * _currentSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }
        else
        {
            animator.Play("Happy");
            _timeElapsed = 0f;
        }
    }

    public void StopPlayer()
    {
        _currentDirection = Vector3.zero;
        _currentSpeed = 0f;
        _timeElapsed = 0f;
    }

    public void IncreasedStats()
    {
        _growthRate += 0.5f;
        _baseForce += 200f;
    }
}
