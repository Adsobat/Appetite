using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//wrapper class for cards
public class CardContainer : ScriptableObject {

	//private CardBehaviourBase[] cards;
	private List<CardBehaviourBase> cards;
	private int MaxAmounts;

	public void Init (int _MaxAmounts) {
		cards = new List<CardBehaviourBase> ();
		for (int i = 0; i < _MaxAmounts; i++) {
			cards.Add (null);
		}
		MaxAmounts = _MaxAmounts;
	}

	public CardBehaviourBase getLast () {
		return cards[MaxAmounts - 1];
	}
	public bool add (CardBehaviourBase _card) {
		if (MaxAmounts > 0) {
			for (int i = (MaxAmounts - 1); i >= 0; --i) {
				//for (int i = 0; i < MaxAmounts; i++) {

				if (cards[i] == null) {
					cards[i] = _card;
					return true;
				}
			}
		}
		return false;
	}
	public void remove (CardBehaviourBase _card) {
		cards.Remove (_card);
		cards.Add (null);
	}

	void Awake () {

	}
	public void rotateElements () {
		CardBehaviourBase first = cards[0];
		cards.Remove (cards[0]);
		cards.Add (first);
	}

	public CardBehaviourBase get (int key) {
		return cards[key];
	}

	public int getCount () {
		return MaxAmounts;
	}
	/*
	int first = list[0];
	list.RemoveAt(0);
	list.Add(first);
	 */
}