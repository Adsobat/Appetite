using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
public class UIBehavior : MonoBehaviour {

	public Slider calorieSlider;
	public Slider thirstSlider;
	public Slider appetiteSlider;
	public Text cooksText;
	public Text MonneyText;

	private CustomerBehavior customer;

	private BoardBehavior board;
	private GamemanagerBehavior gameManager;
	// Use this for initialization
	void Start () {
		customer = (GameObject.FindGameObjectsWithTag ("Customer") [0]).GetComponent (typeof (CustomerBehavior)) as CustomerBehavior;
		board = (GameObject.FindGameObjectsWithTag ("Board") [0]).GetComponent (typeof (BoardBehavior)) as BoardBehavior;
		gameManager = (GameObject.FindGameObjectsWithTag ("Gamemanager") [0]).GetComponent (typeof (GamemanagerBehavior)) as GamemanagerBehavior;

	}

	// Update is called once per frame
	void Update () {
		if (customer != null) {
			if (calorieSlider != null) {
				calorieSlider.value = customer.eatenCalories / customer.caloriesMax;
			}
			if (thirstSlider != null) {
				thirstSlider.value = customer.thirst / customer.thirstMax;
			}
			if (appetiteSlider != null) {
				appetiteSlider.value = customer.appetite / customer.appetiteMax;
			}
		}
		if (board != null) {
			cooksText.text = board.getCooks ().ToString () ;

		} else { Debug.LogError ("Could not find Board check tags"); }
		if(gameManager != null){
			MonneyText.text = gameManager.getMonney().ToString() + " €";
		}else{			Debug.LogError("Could not find gameManger check tags");
}
	}
}