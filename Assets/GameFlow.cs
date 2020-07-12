using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public GameObject policeCar;
    ComplaintManager complaintManager;

    public int ComplaintsTillGameOver = 10;

    bool Gameover = false;
    // Start is called before the first frame update
    void Start()
    {
        
        complaintManager = GameObject.Find("Master").GetComponent<ComplaintManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (complaintManager.complaints.Count >= ComplaintsTillGameOver && !Gameover)
        {
            Gameover = true;
            CallPolice();
        }
    }

    /// <summary>
    /// Shit is about to go down
    /// </summary>
    void CallPolice()
    {
        Instantiate(policeCar);
    }
}
