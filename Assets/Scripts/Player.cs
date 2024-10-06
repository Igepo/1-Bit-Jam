using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float _baseForce = 200f;
    [SerializeField] private float _growthRate = 0.5f;

    private Rigidbody _playerRigidbody;
    private Vector3 _currentDirection = Vector3.zero;
    private float _currentSpeed = 0f;
    private float _timeElapsed = 0f;
    private TrailRenderer _trailRenderer;
    private PlayerInputActions playerInputActions;

    public static event Action<Collision> OnPlayerCollision;

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();

        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += Movement_performed;
    }
    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Movement.performed -= Movement_performed;
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

        _timeElapsed = 0f;
        _currentSpeed = _baseForce;
        _playerRigidbody.velocity = Vector3.zero;
        _playerRigidbody.AddForce(_currentDirection * _currentSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnPlayerCollision?.Invoke(collision);
    }

    void FixedUpdate()
    {
        float currentSpeedMagnitude = _playerRigidbody.velocity.magnitude;

        if (_currentDirection != Vector3.zero && _playerRigidbody.velocity.magnitude > 0.1f)
        {
            animator.Play("Angry");

            _timeElapsed += Time.fixedDeltaTime;
            //_currentSpeed = _baseForce * Mathf.Exp(_growthRate * _timeElapsed);
            _currentSpeed = _baseForce + (_growthRate * _timeElapsed);
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, float.MaxValue);
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
        //_currentDirection = Vector3.zero;
        _currentSpeed = 0f;
        _timeElapsed = 0f;
    }

    public void IncreasedStats()
    {
        _growthRate += 20000f;
        _baseForce += 200f;

        var multiplier = 1.25f;
        transform.localScale *= multiplier;
        _trailRenderer.startWidth *= multiplier;
        CameraManager.Instance.StartDezoom(multiplier, 0.25f);
    }
}
