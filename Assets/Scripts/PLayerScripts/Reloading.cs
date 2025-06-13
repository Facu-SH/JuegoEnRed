using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

using UnityEngine;

public class Reloading : MonoBehaviourPun
{

    [SerializeField] private float reloadSpeed = 0.35f;
    [SerializeField] private float ammoLeft;
    [SerializeField] private int reloadMultiplier = 15;
    
    private RaycastHit hit;


    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, 1f)) // 100f es el largo del rayo
            {
                if (hit.transform.gameObject.TryGetComponent<ReloadBox>(out var ammoBox))
                {
                    ammoBox.ConsumingAmmo(reloadSpeed);
                    ammoLeft += reloadSpeed * Time.deltaTime * reloadMultiplier;
                    ammoLeft = Mathf.RoundToInt(ammoLeft);
                }
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 1f, Color.red);
    }
}
