using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerCam : MonoBehaviour
{
    public float _mouseSensitivity = 0.8f;
    public float _ZoomedSensitivity = 0.3f;
    PlayerInput _playerInput;
    Vector2 _look;
    public Camera _mainCamera;
    public Camera _weaponCamera;

    public Transform _playerBody;
    public GameObject _headGroup;
    [Header("x = min/up, y = max/down")]
    public Vector2 _clampValues = new Vector2(80f, 80f);
    public float _zoomValue;
    public float _oldZoom;

    float _xRotation = 0f;
    bool _isZooming;
    float _currentSensitivity;
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

        _playerInput.Movement.Look.performed += ctx => _look = ctx.ReadValue<Vector2>();
        _playerInput.Movement.Look.canceled += ctx => _look = Vector2.zero;

        _playerInput.Movement.Zooming.performed += ctx => _isZooming = true;
        _playerInput.Movement.Zooming.canceled += ctx => _isZooming = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Mouse.current.delta.x.ReadValue();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();
        RotateY();
        RotateX();
        Zoom();
    }
    void GetMouseInput(){

        _xRotation -= _look.y * (_currentSensitivity / 10f);
        _xRotation = Mathf.Clamp(_xRotation, _clampValues.x, _clampValues.y);
    }
    void RotateY(){
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
    void RotateX(){
        _playerBody.Rotate(Vector3.up * _look.x * (_currentSensitivity / 10f));
    }
    void Zoom(){
        if(_isZooming){
            _mainCamera.fieldOfView = _zoomValue;
            _weaponCamera.fieldOfView = _zoomValue;
            _currentSensitivity = _ZoomedSensitivity;
        }
        else{
            _mainCamera.fieldOfView = _oldZoom;
            _weaponCamera.fieldOfView = _oldZoom;
            _currentSensitivity = _mouseSensitivity;
        } 

    }
}
