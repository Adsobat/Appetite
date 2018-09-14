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
    private CardBehaviourBase       holdingCard;
    private int cooks = 0;
    private int cooksMax = 3;
    private float timeSinceCookRep = 0;
    private float cookRespawnTime = 3; // in seconds
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

        rearangeHandCards();
        print ("Amounts of cards hodlung: " + handCards.Count);
    }
    public void addToField (CardBehaviourBase card) {
        onBoardCars.Add (card);
        Vector2 fieldPosition = new Vector2 (field2DCollider.gameObject.transform.position.x, 
                                 field2DCollider.gameObject.transform.position.y) + field2DCollider.offset;
        card.gameObject.transform.position = new Vector3 (fieldPosition.x, fieldPosition.y, 0 );
        //TODO JUST FOR DEBUGG!!
        removeCard (card);
        rearangeHandCards();
    }
    private void removeCard (CardBehaviourBase card) {
        handCards.Remove (card);
        onBoardCars.Remove (card);
        

    }
    private void rearangeHandCards(){
       float length = hand2DCollider.size.x;
       print("SIZE!!!: " + hand2DCollider.size);
       int n = 0;
       float handCenterX = hand2DCollider.gameObject.transform.position.x + hand2DCollider.offset.x;
       float handCenterY = hand2DCollider.gameObject.transform.position.y + hand2DCollider.offset.y;
       float offset = length / (2+ handCards.Count );
       float offsetCenter = offset * handCards.Count / 2;
       Vector3 newPosition;
       foreach (CardBehaviourBase card in handCards)
       {
          // float newXPos = (handCollider.center.x - length/2) + length/(n+1);
            //float newXPos = (hand2DCollider.center.x - length/2) ;
            // distribute cards evenly
            
            float newXPos = (handCenterX ) + (n*offset) ;
            newPosition = new Vector3(newXPos,handCenterY, 0 );
            //Move to center
            newPosition.x -=offsetCenter;

            //Apply new Position
            card.gameObject.transform.position = new Vector3 (newPosition.x,newPosition.y,newPosition.z); //I could not find the copy constructor
           n++;
          
       }
    }

    public int getCooks () {
        return cooks;
    }
}