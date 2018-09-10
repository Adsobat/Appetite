﻿using System.Collections;
using System.Collections.Generic;
//using UnityEngine.PhysicsModule;
using UnityEngine;

//TODO make board a singleton
public class BoardBehavior : MonoBehaviour {

	public BoxCollider handCollider;
	public BoxCollider fieldCollider;


	private List<CardBehaviourBase> 	handCards;
	private List<CardBehaviourBase> 	onBoardCars;
	private CardBehaviourBase			holdingCard;
	
	// Use this for initialization
	void Start () {
	handCards 	=	new List<CardBehaviourBase>();
	onBoardCars =	new List<CardBehaviourBase>();	
	holdingCard = 	null;

	//TODO
	//REMOVE JUST FOR DEBUGG
	addToHand((CardBehaviourBase)FindObjectOfType(typeof(CardBehaviourBase)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void stopHolding(CardBehaviourBase card){		
		holdingCard = null;

		//check if card was played or moved back to hand
		Vector3 cardCenter = card.gameObject.transform.position;
		
		// Card is played:
		if(fieldCollider.bounds.Contains(cardCenter)){
			card.OnPlayed();
			addToField(card);
			
		}
		// Card is put back to hand
		if(handCollider.bounds.Contains(cardCenter)){			
			addToHand(card);
		}
		//if(topHeaderBoxCollider.bounds.Intersects(currentHeader.boxCollider.bounds))

		
		
		
		
		
	}
	public void startHolding(CardBehaviourBase card){
		
		holdingCard = card;	
		handCards.Remove(card);	
		
	}
	public void addToHand(CardBehaviourBase card){
		handCards.Add(card);
		card.gameObject.transform.position = handCollider.center;
		print("Amounts of cards hodlung: " + handCards.Count );
	}
	public void addToField(CardBehaviourBase card){
		onBoardCars.Add(card);
		card.gameObject.transform.position = fieldCollider.center;
	}
}
