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


	private bool isDrageing = false;
	private Vector3 touchPosWorld;
	private RaycastHit[] hits;

	void Update () {
		UpdateUI();
		// also using mouse down for testing

		// use for phone
		if (Input.touchCount > 0 ) {			
			print("Tougcht");
			//just to be sure
			costMonney = 0;

			
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		}
		// use for PC
		if(Input.GetMouseButtonDown(0)){
			
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			print(touchPosWorld);
			hits= Physics.RaycastAll(touchPosWorld, Camera.main.transform.forward);
			for(int i = 0; i < hits.Length; i++){

				if(hits[i].transform == transform) {
					print("Hello");
					isDrageing = true;				
				}
			}
			
		}
		if(Input.GetMouseButtonUp(0)) {
			isDrageing = false;
			print("I stopped");
		}
		if(isDrageing){
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 newPos = new Vector3(touchPosWorld.x, touchPosWorld.y, 0.0f);
			transform.position = newPos;
			print("I am dragging");
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
