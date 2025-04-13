using System.Collections;
using _Project.Scripts.Fight;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")] 
        [SerializeField] private float _moveSpeed = 5f;

        [Header("Jump Settings")] 
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _jumpHeight = 1f;
        [SerializeField] private float _jumpDuration = 0.5f;
        
        [Header("Ground Check")] 
        [SerializeField] private float _groundCheckRadius = 0.5f;
        [SerializeField] private float _groundCheckDistance = 0.2f;
        [SerializeField] private Vector3 _groundCheckOffset = new(0, -0.1f, 0);
        [SerializeField] private LayerMask _groundLayer;

        [Header("Camera Settings")] 
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private float _cameraPitchLimit = 85f;

        [Header("Dash Settings")] 
        [SerializeField] private float _dashDistance = 5f;
        [SerializeField] private float _dashDuration = 0.2f;
        [SerializeField] private float _dashCooldown = 1f;

        [Header("Other")] 
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Animator _handsAnimator;
        [SerializeField] private AudioClip _dashAndDoubleJumpSound;

        private CharacterController _characterController;
        private AudioSource _audioSource;
        private Vector3 _moveDirection;
        private Vector2 _mouseDelta;
        private Vector3 _groundNormal = Vector3.up;
        private bool _isGrounded;
        private float _cameraPitch;
        private float _dashTimer;
        private float _rollTimer;
        private bool _isDashing;
        private float _verticalVelocity;
        private float _jumpTimer;
        private bool _isJumping;
        private bool _doubleJumpUsed;

        private const float _gravity = -9f;
        private const string _isRunningAnimator = "IsRunning";

        public bool CanDoubleJump { get; set; } = true;
        public bool CanDash { get; set; } = true;
        public bool IsEnabled { get; set; } = true;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _audioSource = GetComponent<AudioSource>();
            Cursor.lockState = CursorLockMode.Locked;
            _playerHealth.Died += () => IsEnabled = false;
        }

        private void Update()
        {
            if (!IsEnabled)
                return;

            HandleCameraRotation();
            HandleMovementInput();
            HandleDashInput();
            HandleJumpInput();
            
        }

        private void FixedUpdate()
        {
            if (!IsEnabled)
                return;

            CheckGround();
            ApplyMovement();
            ApplyGravity();
        }

        private void HandleCameraRotation()
        {
            _mouseDelta = new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            ) * _mouseSensitivity;
            
            _cameraPitch = Mathf.Clamp(_cameraPitch - _mouseDelta.y, -_cameraPitchLimit, _cameraPitchLimit);
            _cameraTransform.localEulerAngles = Vector3.right * _cameraPitch;
            transform.Rotate(Vector3.up * _mouseDelta.x);
        }

        private void HandleMovementInput()
        {
            if (_isDashing) 
                return;

            var input = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );
            _moveDirection = (_cameraTransform.forward * input.y + _cameraTransform.right * input.x);
    
            if (_moveDirection.magnitude > 0.1f)
                _moveDirection.Normalize();
        }

        private void HandleDashInput()
        {
            if (!CanDash || _isDashing || _dashTimer > 0 || !Input.GetKeyDown(KeyCode.LeftShift)) 
                return;

            _isDashing = true;
            _dashTimer = _dashCooldown;
            _audioSource.PlayOneShot(_dashAndDoubleJumpSound);
            StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            float elapsed = 0f;
            var dashDirection = _moveDirection.sqrMagnitude > 0.1f 
                ? Vector3.ProjectOnPlane(_moveDirection, _groundNormal).normalized 
                : Vector3.ProjectOnPlane(transform.forward, _groundNormal).normalized;

            while (elapsed < _dashDuration)
            {
                var dashSpeed = _dashDistance / _dashDuration;
                var dashMovement = dashDirection * (dashSpeed * Time.deltaTime);
                _characterController.Move(dashMovement);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _isDashing = false;
            yield return new WaitForSeconds(_dashTimer);
            _dashTimer = 0;
        }

        private void CheckGround()
        {
            bool wasGrounded = _isGrounded;
            RaycastHit hit;
            _isGrounded = Physics.SphereCast(
                transform.position + _groundCheckOffset, _groundCheckRadius, Vector3.down,
                out hit,  _groundCheckDistance, _groundLayer);

            if (!_isGrounded)
            {
                _groundNormal = Vector3.up;
                return;
            }
        
            _groundNormal = hit.normal;

            if (wasGrounded) 
                return;
        
            _doubleJumpUsed = false;
            _isJumping = false;
        }
    
        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || (CanDoubleJump && !_doubleJumpUsed)))
            {
                if (!_doubleJumpUsed && !_isGrounded)
                {
                    _doubleJumpUsed = true;
                    _audioSource.PlayOneShot(_dashAndDoubleJumpSound);
                }
            
                StartJump();
            }

            if (_isJumping)
                UpdateJump();
        }
    
        private void StartJump()
        {
            _isJumping = true;
            _jumpTimer = 0f;
            _verticalVelocity = 0f;
        }
    
        private void UpdateJump()
        {
            _jumpTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(_jumpTimer / _jumpDuration);
            _verticalVelocity = _jumpCurve.Evaluate(progress) * _jumpHeight;

            if (progress >= 1)
                _isJumping = false;
        }

        private void ApplyMovement()
        {
            if (_isDashing) 
                return;

            var movement = Vector3.zero;
            movement.y = _verticalVelocity * Time.deltaTime;

            if (_moveDirection.sqrMagnitude > 0.1f)
            {
                _handsAnimator.SetBool(_isRunningAnimator, true);
        
                var targetVelocity = Vector3.ProjectOnPlane(_moveDirection, _groundNormal).normalized * _moveSpeed;
                movement.x = targetVelocity.x * Time.deltaTime;
                movement.z = targetVelocity.z * Time.deltaTime;
            }
            
            else
                _handsAnimator.SetBool(_isRunningAnimator, false);

            _characterController.Move(movement);
        }

        private void ApplyGravity()
        {
            if (!_isJumping && !_isGrounded)
            {
                _verticalVelocity = _gravity;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            var sphereCenter = transform.position + _groundCheckOffset + Vector3.down * _groundCheckDistance;
            Gizmos.DrawWireSphere(sphereCenter, _groundCheckRadius);
        }
    }
}