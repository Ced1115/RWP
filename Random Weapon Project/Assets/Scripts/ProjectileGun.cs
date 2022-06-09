using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public Gun_Projectile _gun;
    PlayerInput _playerInput;
    public GameObject _barrel;
    void OnEnable()
    {
        _playerInput.Actions.Enable();
    }
    void OnDisable()
    {
        _playerInput.Actions.Disable();
    }
    void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Actions.Shoot.performed += ctx => Shoot();
    }
    void Shoot(){
        GameObject projectile = Instantiate(_gun._Projectile, _barrel.transform.position, _barrel.transform.rotation);
    }
}
