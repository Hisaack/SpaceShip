﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public ShootProfile shootProfile;
    public GameObject bulletPrefab;
    public Transform firePoint;


    private float totalSpread;
    private WaitForSeconds rate, interval;
    void Start()
    {
    }

    private void OnEnable()
    {
        interval = new WaitForSeconds(shootProfile.interval);

        rate = new WaitForSeconds(shootProfile.fireRate);

        if (firePoint == null)
            firePoint = transform;

        totalSpread = shootProfile.spread * shootProfile.amount;

        StartCoroutine(ShootingSequence());

    }

    private void OnDisable()
    {
        StopAllCoroutines();     
    }
    IEnumerator ShootingSequence()
    {
        yield return rate;

        while (true)
        {
            float angle = 0f;

            if (shootProfile.amount > 1)
            {
                for (int i = 0; i < shootProfile.amount; i++)
                {
                    angle = totalSpread * (i / (float)shootProfile.amount);

                    angle -= (totalSpread / 2f) - (shootProfile.spread / shootProfile.amount);

                    Shoot(angle);

                    if(shootProfile.fireRate>0f)
                        yield return rate;

                }
            }
            yield return interval;

        }
    }

  void Shoot(float angle)
    {

        GameObject temp =PoolingManager.Instance.UseObject(bulletPrefab, firePoint.position, firePoint.rotation);
        temp.transform.Rotate(Vector3.up, angle);
    }

    public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        return Instantiate(obj, pos, rot);
    }
}
