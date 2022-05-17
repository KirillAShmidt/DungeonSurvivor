using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private Text _text;
    private PlayerShooting _shooting;
    private GunData _data;

    private void Start()
    {
        _shooting = FindObjectOfType<PlayerShooting>();
        _shooting.OnGunTaken += Initialize;

        _text = GetComponent<Text>();
    }

    private void Update()
    {
        if (!_data.isReloading)
        {
            _text.text = String.Format("R: {0} / {1}", _data.currentRounds, _data.rounds);
        }
        else
        {
            _text.text = "R: ! RELOADING !";
        }
    }

    private void Initialize(GunData data)
    {
        _data = data;
    }
}
