using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCard : CardBehaviourBase {

    // Use this for initialization
    void Start () {
        base.Start ();
    }

    public override void OnPlayed () {
        base.OnPlayed ();
        //print ("I am played DUMMY");

    }
}