using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject GuestGameObject;

    public int TargetQuantity = 10;
    public float Rate = 1;

    private float spawnTimer;

    void Start()
    {
        spawnTimer = Rate;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnLogic();
            spawnTimer = Rate;
        }

    }

    private void SpawnLogic()
    {
        if (GameObject.FindGameObjectsWithTag("Guest").Length < TargetQuantity)
            Instantiate(GuestGameObject, new Vector3(-10, 0, 0), Quaternion.identity);
    }
}
