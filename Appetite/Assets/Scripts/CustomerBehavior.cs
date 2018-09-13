using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour {

	public float thirst = 15;
	public float thirstMax = 100;
	public float appetite = 20;
	public float appetiteMax = 100;
	public float eatenCalories = 0;
	public float caloriesMax = 100;
	
	public void Eat(CardBehaviourBase food){
		thirst += food.thirst;
		thirst = Mathf.Clamp(thirst,0,thirstMax);
		eatenCalories += food.calories;
		eatenCalories = Mathf.Clamp(eatenCalories,0,caloriesMax);
		print("EAT");
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
