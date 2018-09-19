using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public List<GameObject> vegetableDeck;
	public List<GameObject> meatDeck;
	public List<GameObject> drinkDeck;

	
	// Use this for initialization
	void Start () {
		vegetableDeck = new List<GameObject> ();
		meatDeck = new List<GameObject> ();
		drinkDeck = new List<GameObject> ();
		fillDecks ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void fillDecks () {
		GameObject[] prefabBuffer = Resources.LoadAll<GameObject> ("Prefabs");
		foreach (GameObject obj in prefabBuffer) {
			CardBehaviourBase card = obj.GetComponent (typeof (CardBehaviourBase)) as CardBehaviourBase;

			if (card != null) {
				switch (card.CardType) {
					case cardTypes.Meat:
						{
							meatDeck.Add (card.gameObject);
							break;
						}
					case cardTypes.Drink:
						{
							drinkDeck.Add (card.gameObject);
							break;
						}
					case cardTypes.Vegetable:
						{
							vegetableDeck.Add (card.gameObject);
							break;
						}
					default:
						break;
				}
			}
		}

		
	}
}