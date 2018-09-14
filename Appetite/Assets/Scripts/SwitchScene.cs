using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    //switches sceene to scene with the name sceneName
    public void switchSceneNow (string sceneName) {
        SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
    }
    public void helloWorld () {
        print ("Hallo Welt");
    }

}