using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
public class UIBehavior : MonoBehaviour {

	public Slider calorieSlider;
	public Slider thirstSlider;
	public Slider appetiteSlider;
	private CustomerBehavior customer;
	// Use this for initialization
	void Start () {
		customer = (GameObject.FindGameObjectsWithTag ("Customer") [0]).GetComponent(typeof(CustomerBehavior)) as CustomerBehavior;
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
				appetiteSlider.value = customer.appetite/ customer.appetiteMax;
			}
		}
	}
}