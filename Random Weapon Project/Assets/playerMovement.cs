using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    PlayerInput _playerInput;
    public CharacterController _controller;
    public GameObject _playerTransform;

    public float _graviy;
    public float _speed;
    public float _runMultiplier;
    public float _crouchMultiplier;
    public Vector3 _crouchSize;
    public Vector3 _standingSize;
    public float _jumpHeight = 3f;
    [Range(0, 1)]
    public float _airControl;

    public Transform _groundCheck;
    public float _groundRadius;
    public LayerMask _groundLayer;
    public float _headDist;

    Vector2 _dir = new Vector2();
    Vector3 _velocity;
    bool _isGrounded;
    float _currentAirControl;
    bool _isJumping;
    bool _isSprinting;
    float _currentRunMultiplier;
    bool _isCrouching;
    float _currentCrouchMultiplier;
    bool _canStandUp = true;
    

    void OnEnable()
    {
        _playerInput.Movement.Enable();
    }
    void OnDisable()
    {
        _playerInput.Movement.Disable();
    }
    void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Movement.Move.performed += ctx => _dir = ctx.ReadValue<Vector2>();
        _playerInput.Movement.Move.canceled += ctx => _dir = Vector2.zero;

        _playerInput.Movement.Jump.performed += ctx => _isJumping = true;
        _playerInput.Movement.Jump.canceled += ctx => _isJumping = false;

        _playerInput.Movement.Sprint.performed += ctx => _isSprinting = true;
        _playerInput.Movement.Sprint.canceled += ctx => _isSprinting = false;

        _playerInput.Movement.Crouching.performed += ctx => _isCrouching = true;
        _playerInput.Movement.Crouching.canceled += ctx => _isCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Walk();
        Crouch();
        Running();
        Jump();
        ApplyGravity();
    }
    void GroundCheck(){
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundRadius, _groundLayer);

        if(_isGrounded && _velocity.y < 0){
            _velocity.y = 0f;
            _currentAirControl = 1;
        }else{
            _currentAirControl = _airControl;
        }
    }
    void Walk(){
        Vector3 move = transform.right * _dir.x + transform.forward * _dir.y;
        _controller.Move(move * (_speed * _currentAirControl * _currentRunMultiplier * _currentCrouchMultiplier) * Time.deltaTime);
    }
    void Crouch(){
        RaycastHit hit;
        if(Physics.Raycast(_groundCheck.position, _groundCheck.up,out hit, _headDist, _groundLayer)){
            _canStandUp = false;
        }else{
            _canStandUp = true;
        }
        if(_isCrouching){
            _playerTransform.transform.localScale = _crouchSize;
            _currentCrouchMultiplier = _crouchMultiplier;
        }else if(_canStandUp){
            _currentCrouchMultiplier = 1;
            _playerTransform.transform.localScale = _standingSize;
        }
    }
    void Running(){
        if(_isSprinting && !_isCrouching){
            _currentRunMultiplier = _runMultiplier;
        }else{
            _currentRunMultiplier = 1f;
        }
    }
    void Jump(){
        if(_isJumping && _isGrounded){
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _graviy);
        }
    }
    void ApplyGravity(){
        _velocity.y += _graviy * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}
