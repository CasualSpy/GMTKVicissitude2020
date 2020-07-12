using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public GameObject policeCar;
    ComplaintManager complaintManager;
    public float GameLength;
    float GameTimer = 0f;
    TextMeshProUGUI clock;

    SceneAudioManager sceneAudio;

    List<MonoBehaviour> objsToEnable = new List<MonoBehaviour>();

    public int ComplaintsTillGameOver = 10;

    GameState currentState = GameState.Stopped;

    enum GameState
    {
        Stopped,
        Running,
        GoodEnding,
        BadEnding
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneAudio = GetComponent<SceneAudioManager>();

        objsToEnable.Add(GameObject.Find("Master").GetComponent<GuestSpawner>());
        objsToEnable.Add(GameObject.Find("Player").GetComponent<ControlsManager>());


        foreach (var item in objsToEnable)
        {
            item.enabled = false;
        }


        clock = GameObject.Find("TimeDisplay").GetComponent<TextMeshProUGUI>();
        complaintManager = GameObject.Find("Master").GetComponent<ComplaintManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Running)
            GameTimer += Time.deltaTime;

        float percentDone = GameTimer / GameLength;
        float nbOfMinutesElapsed = Mathf.Lerp(0, 360, percentDone);
        int minutes = Mathf.FloorToInt(Mathf.Repeat(nbOfMinutesElapsed, 60));
        int hours = (int)Mathf.Repeat(Mathf.FloorToInt(nbOfMinutesElapsed / 60) + 21, 24);

        //music
        if (percentDone > 0.5f && currentState == GameState.Running && sceneAudio.currentlyPlaying == SceneAudioManager.Track.Start)
            sceneAudio.PlayMiddleNight();

        clock.text = $"{hours:00}" + ":" + $"{minutes:00}";

        if (complaintManager.complaints.Count >= ComplaintsTillGameOver && currentState == GameState.Running)
        {
            currentState = GameState.BadEnding;
            CallPolice();
        } else if (percentDone >= 1 && currentState == GameState.Running)
        {
            currentState = GameState.GoodEnding;
            sceneAudio.PlayGoodTrack();
            ShowGoodEnding();
        }
    }

    public void StartParty()
    {
        GameObject.Find("StartMenu").SetActive(false);
        currentState = GameState.Running;


        foreach (var item in objsToEnable)
        {
            item.enabled = true;
        }
    }

    /// <summary>
    /// Shit is about to go down
    /// </summary>
    void CallPolice()
    {
        sceneAudio.PlayBadTrack();
        Instantiate(policeCar);
        currentState = GameState.Stopped;
        Invoke("ShowBadEnding", 5f);

        foreach (var item in objsToEnable)
        {
            item.enabled = false;
        }
    }

    void ShowBadEnding()
    {
        (Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(o => o.name == "BadScreen") as GameObject).SetActive(true);
    }

    void ShowGoodEnding()
    {
        (Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(o => o.name == "GoodScreen") as GameObject).SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
