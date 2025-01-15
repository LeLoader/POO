using NaughtyAttributes;
using System;
using System.Security.Cryptography;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset IAA_Player;
    [SerializeField] InputActionReference IA_moveAction;
    [SerializeField] InputActionReference IA_attackAction;
    [SerializeField] InputActionReference IA_jumpAction;
    [SerializeField] CinemachineCamera _camera;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject _playerObject;
    [SerializeField] Animator _animator;
    [SerializeField] Health _health;
    [SerializeField] Weapon _weapon;
    bool inverseGravity;

    [SerializeField, Tooltip("In m/s")] int _movementSpeed;
    public int MovementSpeed
    {
        get
        {
            return _movementSpeed;
        }
        set
        {
            if (value > 0)
            {
                _movementSpeed = value;
            }
        }
    }

    [SerializeField, Tooltip("In deg/s")] int _rotationSpeed;
    float _rotationTimer;
    Vector3 _moveDirection = new();
    Vector3 _wantedDirection = new();
    bool _canMove;

    private void Awake()
    {
        IA_moveAction.action.started += StartMove;
        IA_moveAction.action.performed += PerformMove;
        IA_moveAction.action.canceled += StopMove;
        IA_jumpAction.action.started += ChangeGravity;
        if (_weapon != null)
        {
            IA_attackAction.action.started += AttackStarted;
        }

        _health.OnDeath.AddListener(Death);
    }

    private void Death()
    {
        _canMove = false;
        _animator.SetTrigger("OnDeath");
        DisableAllBindings();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        _animator.SetFloat("velocity", _moveDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if (inverseGravity) _playerObject.transform.rotation = Quaternion.RotateTowards(_playerObject.transform.rotation, Quaternion.LookRotation(_wantedDirection, Vector3.up) * Quaternion.Euler(0, 0, 180), _rotationSpeed * Time.fixedDeltaTime);
        else _playerObject.transform.rotation = Quaternion.RotateTowards(_playerObject.transform.rotation, Quaternion.LookRotation(_wantedDirection, Vector3.up), _rotationSpeed * Time.fixedDeltaTime);

        if (_canMove)
        {
            _wantedDirection = _moveDirection.x * _camera.transform.right + _moveDirection.z * _camera.transform.forward;
            _wantedDirection.Normalize();
            _wantedDirection.y = 0;
            _rigidbody.AddForce(_movementSpeed * _playerObject.transform.forward, ForceMode.VelocityChange);
            //var rot = Quaternion.FromToRotation(_playerObject.transform.forward, _wantedDirection);

        }
    }

    void AttackStarted(InputAction.CallbackContext callback)
    {
        _animator.SetInteger("randomAttack", (int)Mathf.Floor(Random.Range(1, 5)));
        _animator.SetTrigger("OnAttack");
        _weapon.ActivateFor(0.5f);
    }

    void StartMove(InputAction.CallbackContext callback)
    {
        _canMove = true;
    }

    void PerformMove(InputAction.CallbackContext callback)
    {
        var rawMoveInput = IA_moveAction.action.ReadValue<Vector2>();
        _moveDirection.Set(rawMoveInput.x, 0, rawMoveInput.y);
    }

    void StopMove(InputAction.CallbackContext callback)
    {
        _canMove = false;
        _moveDirection = Vector3.zero;
    }

    void ChangeGravity(InputAction.CallbackContext callback)
    {
        Physics.gravity = -Physics.gravity;
        inverseGravity = !inverseGravity;
    }

    void DisableAllBindings()
    {
        IA_moveAction.action.started -= StartMove;
        IA_moveAction.action.performed -= PerformMove;
        IA_moveAction.action.canceled -= StopMove;
        IA_jumpAction.action.started -= ChangeGravity;
        if (_weapon != null)
        {
            IA_attackAction.action.started -= AttackStarted;
        }
        _health.OnDeath.RemoveListener(Death);
    }

    private void OnDisable()
    {
        DisableAllBindings();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_playerObject.transform.position, _playerObject.transform.position + _playerObject.transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_playerObject.transform.position, _playerObject.transform.position + _wantedDirection);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.Label("Actual rotation quat:" + _playerObject.transform.rotation.ToString());
        GUILayout.Label("Actual rotation euler:" + _playerObject.transform.rotation.eulerAngles.ToString());
        GUILayout.Label("Wanted rotation quat:" + Quaternion.LookRotation(_wantedDirection, Vector3.up).ToString());
        GUILayout.Label("Wanted rotation euler(maybe not):" + _wantedDirection);

        GUILayout.EndVertical();
    }
}
