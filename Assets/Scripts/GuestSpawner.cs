﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject GuestGameObject;
    public GameObject MinorGameObject;
    private TMPro.TextMeshProUGUI guestDisplay;
    public int TargetQuantity = 10;
    public float Rate = 10;
    public float minorRate = 0.1f;

    private int guestCount = 0;

    public int GuestCount
    {
        get
        {
            return guestCount;
        }
        set
        {
            guestCount = value;
            guestDisplay.text = $"guests / {value:00}";
        }
    }


    private float spawnTimer;

    void Start()
    {
        guestDisplay = GameObject.Find("GuestDisplay").GetComponent<TMPro.TextMeshProUGUI>();
        //SpawnLogic();
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
        int available = TargetQuantity - GameObject.FindGameObjectsWithTag("Guest").Length;
        if (available > 1)
        {
            SpawnDriver();

            //Spawn passengers
            int amount = Math.Min(available - 1, UnityEngine.Random.Range(0, 5));
            for (int i = 0; i < amount; i++)
            {
                bool isMinor = UnityEngine.Random.value < minorRate;
                if (isMinor)
                    SpawnMinor(i);
                else
                    SpawnGuest(i);
            }

        }
    }

    private void SpawnDriver()
    {
        GameObject go = Instantiate(GuestGameObject, new Vector3(-10, 0, 0), Quaternion.identity);
        GuestMain driver = go.GetComponent<GuestMain>();
        driver.MakeDriver();
        SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
        sr.color = Helpers.GenerateColor();
    }

    private void SpawnGuest(int num)
    {
        GameObject go = Instantiate(GuestGameObject, new Vector3(-10, (num + 1) * 0.3f, 0), Quaternion.identity);
        SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
        sr.color = Helpers.GenerateColor();
    }

    private void SpawnMinor(int num)
    {
        GameObject go = Instantiate(MinorGameObject, new Vector3(-10, (num + 1) * 0.3f, 0), Quaternion.identity);
        GuestMain minor = go.GetComponent<GuestMain>();
        minor.MakeMinor();
        SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
        sr.color = Helpers.GenerateColor();
    }
}
