using System.Collections;
using System.Collections.Generic;
//using UnityEngine.PhysicsModule;
using UnityEngine;

//TODO make board a singleton
public class BoardBehavior : MonoBehaviour {

	public BoxCollider handCollider;
	public BoxCollider fieldCollider;
	//TODO Remove just for Debugg
	public GameObject DummyCard;

	private List<CardBehaviourBase> handCards;
	private List<CardBehaviourBase> onBoardCars;
	private CardBehaviourBase holdingCard;
	private int cooks = 0;
	private int cooksMax = 3;
	private float timeSinceCookRep = 0;
	private float cookRespawnTime = 10; // in seconds
	// Use this for initialization
	void Start () {
		handCards = new List<CardBehaviourBase> ();
		onBoardCars = new List<CardBehaviourBase> ();
		holdingCard = null;

		//TODO
		//REMOVE JUST FOR DEBUGG
		addToHand ((CardBehaviourBase) FindObjectOfType (typeof (CardBehaviourBase)));
	}

	// Update is called once per frame
	void Update () {
		//TODO remove and add auto draw 
		if (Input.GetKeyDown ("space")) {
			drawCard ();
		}
		if(cooks < cooksMax){
			timeSinceCookRep +=Time.deltaTime; 
			if(timeSinceCookRep >= cookRespawnTime){
				cooks += 1;
				timeSinceCookRep -=cookRespawnTime;
				if(cooks >= cooksMax){
					cooks = cooksMax;
					timeSinceCookRep= 0;
				}
			}
		}

	}

	public void stopHolding (CardBehaviourBase card) {
		holdingCard = null;

		//check if card was played or moved back to hand
		Vector3 cardCenter = card.gameObject.transform.position;

		// Card is played:
		if (fieldCollider.bounds.Contains (cardCenter)) {
			card.OnPlayed ();
			addToField (card);

		}
		// Card is put back to hand
		if (handCollider.bounds.Contains (cardCenter)) {
			addToHand (card);
		}
		//if(topHeaderBoxCollider.bounds.Intersects(currentHeader.boxCollider.bounds))

	}
	public void drawCard () {

		addToHand (((GameObject) Instantiate (DummyCard, transform.position, transform.rotation)).GetComponent (typeof (CardBehaviourBase)) as CardBehaviourBase);
	}
	public void startHolding (CardBehaviourBase card) {

		holdingCard = card;
		handCards.Remove (card);

	}
	public void addToHand (CardBehaviourBase card) {
		handCards.Add (card);
		card.gameObject.transform.position = handCollider.center;
		print ("Amounts of cards hodlung: " + handCards.Count);
	}
	public void addToField (CardBehaviourBase card) {
		onBoardCars.Add (card);
		card.gameObject.transform.position = fieldCollider.center;
		//TODO JUST FOR DEBUGG!!
		removeCard (card);
	}
	private void removeCard (CardBehaviourBase card) {
		handCards.Remove (card);
		onBoardCars.Remove (card);
		Destroy (card.gameObject); // DIRTY TODO
	}
	public int getCooks(){
		return cooks;
	}
}