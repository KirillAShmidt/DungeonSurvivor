using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectile;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private int _roundsAmount;

    [SerializeField]
    private float _reloadTime = 5f;

    private GunData _gunData;

    public Action<GunData> OnGunTaken;
    public Action OnReloadStart;

    private void Start()
    {
        _gunData = new GunData(_roundsAmount);
        _gunData.currentRounds = _roundsAmount;
        OnGunTaken?.Invoke(_gunData);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_gunData.isReloading)
        {
            var projectileClone = Instantiate(_projectile);
            projectileClone.transform.position = _firePoint.position;
            projectileClone.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
            _gunData.currentRounds--;
        }

        if (Input.GetKey(KeyCode.R) || _gunData.currentRounds <= 0 && !_gunData.isReloading)
        {
            StartCoroutine(Realoading());
        }
    }

    public IEnumerator Realoading()
    {
        OnReloadStart?.Invoke();

        _gunData.isReloading = true;

        var timer = _reloadTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime * 2;
            yield return new WaitForFixedUpdate();
        }
        _gunData.isReloading = false;
        _gunData.currentRounds = _roundsAmount;
    }
}
