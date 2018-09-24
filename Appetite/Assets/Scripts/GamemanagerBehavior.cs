using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemanagerBehavior : MonoBehaviour {
    public float levelDuration = 0.5f; // in Minutes
    private float timeElapsed = .0f;
    private float monney = 0;

    private bool isLevelRunning = true;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        timeElapsed += Time.deltaTime;
        if (MinutedToSeconds (levelDuration) <= timeElapsed) {
            if(isLevelRunning){
                finsihLeve ();
            }
        }
    }

    float MinutedToSeconds (float minutes) {
        return minutes * 60;
    }
    public void finsihLeve () {
        print ("Game finishend");
        isLevelRunning= false;
    }
    public void addMonney (float monneyToAdd) {
        monney += monneyToAdd;
    }
    public void subtractMonney (float monneyTosubtract) {
        monney -= monneyTosubtract;
    }
    public float getMonney () {
        return monney;
    }
}