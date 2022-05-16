using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _speed;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * _speed;

        _characterController.Move(movement);  
        //_rigidbody.AddForce(movement);
    }
}
