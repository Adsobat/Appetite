using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBehaviourBase : MonoBehaviour {
	// costs for gameplay
	public int cookCost;
	public float calories;
	public float thirst;
	public float costMonney = 13;
	public TextMesh costText;

	void Update () {
		UpdateUI();
		// also using mouse down for testing
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {			
			print("Tougcht");
			//just to be sure
			costMonney = 0;
			
		}

	}
	public void OnEaten(){

	}
	public void OnPlayed(){

	}
	public void OnRemoved(){

	}
	public void OnEnhanced(){

	}
	public void OnDrawn(){

	}
	private void UpdateUI(){
		if(costText !=null){
			costText.text = costMonney + " €";
		}

	}

}
