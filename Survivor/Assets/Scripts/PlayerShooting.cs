using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectile;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _angleSpeed = 50;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out var raycastHit))
        {
            var lookPos = raycastHit.point - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _angleSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var projectileClone = Instantiate(_projectile);
            projectileClone.transform.position = _firePoint.position;
            projectileClone.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        }
    }
}
