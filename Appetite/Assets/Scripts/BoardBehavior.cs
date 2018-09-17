using System.Collections;
using System.Collections.Generic;
//using UnityEngine.PhysicsModule;
using UnityEngine;

//TODO make board a singleton
public class BoardBehavior : MonoBehaviour {

    public BoxCollider2D hand2DCollider;
    public BoxCollider2D field2DCollider;
    //TODO Remove just for Debugg
    public GameObject DummyCard;

    private List<CardBehaviourBase> handCards;
    private List<CardBehaviourBase> onBoardCars;
    private CardBehaviourBase holdingCard;
    private int cooks = 0;
    private int cooksMax = 3;
    private float timeSinceCookRep = 0;
    private float cookRespawnTime = 3; // in seconds
    public int MaxCardPerRow = 4;
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
        if (cooks < cooksMax) {
            timeSinceCookRep += Time.deltaTime;
            if (timeSinceCookRep >= cookRespawnTime) {
                cooks += 1;
                timeSinceCookRep -= cookRespawnTime;
                if (cooks >= cooksMax) {
                    cooks = cooksMax;
                    timeSinceCookRep = 0;
                }
            }
        }

    }

    public void stopHolding (CardBehaviourBase card) {
        holdingCard = null;

        //check if card was played or moved back to hand
        Vector3 cardCenter = card.gameObject.transform.position;

        // Card is placed on field:
        if (field2DCollider.bounds.Contains (cardCenter)) {
            if (card.cookCost <= cooks) {
                playCard (card);
                cooks -= card.cookCost;
                Mathf.Clamp (cooks, 0, cooksMax);
            } else {
                addToHand (card);
            }
        }
        // Card is put back to hand
        if (hand2DCollider.bounds.Contains (cardCenter)) {
            addToHand (card);
        }
        //if(topHeaderBoxCollider.bounds.Intersects(currentHeader.boxCollider.bounds))

    }
    private void playCard (CardBehaviourBase card) {
        card.OnPlayed ();
        addToField (card);
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
        // card.gameObject.transform.position = hand2DCollider.center;
        card.gameObject.transform.position = hand2DCollider.offset;

        rearangeHandCards ();
        //print ("Amounts of cards hodlung: " + handCards.Count);
    }
    public void addToField (CardBehaviourBase card) {
        onBoardCars.Add (card);
        Vector2 fieldPosition = new Vector2 (field2DCollider.gameObject.transform.position.x,
            field2DCollider.gameObject.transform.position.y) + field2DCollider.offset;
        card.gameObject.transform.position = new Vector3 (fieldPosition.x, fieldPosition.y, 0);
        //TODO JUST FOR DEBUGG!!
        removeCard (card);
        rearangeHandCards ();
    }
    private void removeCard (CardBehaviourBase card) {
        handCards.Remove (card);
        onBoardCars.Remove (card);

    }
    private void rearangeHandCards () {
        float length = hand2DCollider.size.x;
        float height = hand2DCollider.size.y;
        Vector3 newPosition;
        // do we have to much cards in one handrow
        //if (handCards.Count > MaxCardPerRow) {
        int rowAmount = Mathf.CeilToInt ((float)handCards.Count / MaxCardPerRow);
        float handCenterX = hand2DCollider.gameObject.transform.position.x + hand2DCollider.offset.x;
        float handCenterY = hand2DCollider.gameObject.transform.position.y + hand2DCollider.offset.y;
        

        int cardPerRow = Mathf.CeilToInt ((float)handCards.Count /(float) rowAmount); //round up

        //float offsetX = length / (2 + handCards.Count);
        float offsetX = length / (1 + cardPerRow);
        float offsetY = height / (1 + rowAmount);
       // print( handCards.Count +" / " + rowAmount + " = Cards Per row: " + cardPerRow);
        
        float offsetCenter = offsetX * handCards.Count / 2;
        offsetCenter = length / 2;
        offsetCenter = offsetX * cardPerRow / 2;
        CardBehaviourBase card;
        for (int i = 0; i < rowAmount; i++) {
            

            for (int j = cardPerRow * i; j < cardPerRow * i + cardPerRow; j++) {
               
                if (j < handCards.Count) {// check if elements exist. Important because we have to round up cardPerRow
                    card = handCards[j];
                    int numInHand = j - i * cardPerRow;
                    float newXPos = (handCenterX) + ( (numInHand+1) * offsetX);
                    float newYPos = handCenterY + (i * offsetY);
                    //newYPos = handCenterY ;//debugg

                    newPosition = new Vector3 (newXPos, newYPos, 0);
                    //Move to center                    
                    newPosition.x -= length/2;
                    //Apply new Position
                    card.gameObject.transform.position = new Vector3 (newPosition.x, newPosition.y, newPosition.z); //I could not find the copy constructor
                    // card.gameObject.transform.position = hand2DCollider.gameObject.transform.position;

                    //print("Move Card with index:  " + j + "to position " + card.gameObject.transform.position);
                }
                // else{
                //    // print("Did overflow at: " + j);
                // }

            }

        }

    }

    public int getCooks () {
        return cooks;
    }
}