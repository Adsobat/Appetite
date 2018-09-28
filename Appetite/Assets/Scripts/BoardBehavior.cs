using System.Collections;
using System.Collections.Generic;

//using UnityEngine.PhysicsModule;
using UnityEngine;

//TODO make board a singleton
public class BoardBehavior : MonoBehaviour {

    public BoxCollider2D hand2DCollider;
    public BoxCollider2D field2DCollider;
    //TOD  O Remove just for Debugg

    private List<CardBehaviourBase> handCards;
   
    private CardContainer onBoardCars;

    private CardBehaviourBase holdingCard;
    public int cooks = 3;
    public int cooksMax = 5;
    private float timeSinceCookRep = 0;
    private float cookRespawnTime = 3; // in seconds
    public int MaxCardPerRow = 4;
    public int MaxCardPerTable = 4;
    private PlayerBehavior player;

    private float tableRoatation = 0; // rotation of the table in radiant
    public float tableRotatespeed = 5; // in seconds
    // Use this for initialization
    void Start () {
        handCards = new List<CardBehaviourBase> ();
        //onBoardCars = new List<CardBehaviourBase> ();
        onBoardCars = ScriptableObject.CreateInstance<CardContainer>();
        onBoardCars.Init(MaxCardPerTable);
        holdingCard = null;

        player = ((PlayerBehavior) FindObjectOfType (typeof (PlayerBehavior)));
        //TODO
        //REMOVE JUST FOR DEBUGG
        addToHand ((CardBehaviourBase) FindObjectOfType (typeof (CardBehaviourBase)));

        // Start rotating table;
        InvokeRepeating ("rotateTable", tableRotatespeed, tableRotatespeed);
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
        int rand = Random.Range (0, 2);
        int randCard= 0; 
        if (rand == 0) {
            randCard = Random.Range(0, player.vegetableDeck.Count);
            addToHand (((GameObject) Instantiate (player.vegetableDeck[randCard], transform.position, transform.rotation)).GetComponent (typeof (CardBehaviourBase)) as CardBehaviourBase);
            print ("vegetable Drawn");
        } else if (rand == 1) {
            randCard = Random.Range(0, player.meatDeck.Count);
            addToHand (((GameObject) Instantiate (player.meatDeck[randCard], transform.position, transform.rotation)).GetComponent (typeof (CardBehaviourBase)) as CardBehaviourBase);
            print ("meat drawn");
        }

    }
    public void startHolding (CardBehaviourBase card) {

        holdingCard = card;
        handCards.Remove (card);

    }
    public void addToHand (CardBehaviourBase card) {
        handCards.Add (card);
        // card.gameObject.transform.position = hand2DCollider.center;
        //card.gameObject.transform.position = hand2DCollider.offset;

        rearangeHandCards ();
        //print ("Amounts of cards hodlung: " + handCards.Count);
    }
    public void addToField (CardBehaviourBase card) {
        onBoardCars.add (card);
        // Vector2 fieldPosition = new Vector2 (field2DCollider.gameObject.transform.position.x,
        //     field2DCollider.gameObject.transform.position.y) + field2DCollider.offset;
        //card.gameObject.transform.position = new Vector3 (fieldPosition.x, fieldPosition.y, 0);
        //TODO JUST FOR DEBUGG!!
        //removeCard (card);
        rearangeHandCards ();
        rearangeFielCards ();
    }
    private void removeCard (CardBehaviourBase card) {
        handCards.Remove (card);
        onBoardCars.remove (card);

    }
    private void rearangeFielCards () {
        Vector3 center = new Vector3 (field2DCollider.transform.position.x + field2DCollider.offset.x,
            field2DCollider.transform.position.y + field2DCollider.offset.y, 0);
        float height = field2DCollider.size.y;
        float length = field2DCollider.size.x;
        float alpha = 0;
        // radian for the ellipse table
        float rX = length / 2;
        float rY = height / 2;
        for (int i = 0; i < onBoardCars.getCount (); i++) {
            alpha = (float) ((float) i * (2 * Mathf.PI / MaxCardPerTable));
            alpha += tableRoatation;
            Vector2 newPosition = new Vector2 (height / 2, alpha).TransPolarToKar ();
            newPosition = newPosition + new Vector2 (center.x, center.y);
            if (onBoardCars.get (i) != null) {
                onBoardCars.get (i).transform.position = new Vector3 (newPosition.x, newPosition.y, 0);
                // onBoardCars[i].transform.position = new Vector3 (newPosition.x, newPosition.y, 0);
            }
        }
    }

    private void rearangeHandCards () {
        float length = hand2DCollider.size.x;
        float height = hand2DCollider.size.y;
        Vector3 newPosition;
        // do we have to much cards in one handrow
        //if (handCards.Count > MaxCardPerRow) {
        int rowAmount = Mathf.CeilToInt ((float) handCards.Count / MaxCardPerRow);
        float handCenterX = hand2DCollider.gameObject.transform.position.x + hand2DCollider.offset.x;
        float handCenterY = hand2DCollider.gameObject.transform.position.y + hand2DCollider.offset.y;

        int cardPerRow = Mathf.CeilToInt ((float) handCards.Count / (float) rowAmount); //round up

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

                if (j < handCards.Count) { // check if elements exist. Important because we have to round up cardPerRow
                    card = handCards[j];
                    int numInHand = j - i * cardPerRow;
                    float newXPos = (handCenterX) + ((numInHand + 1) * offsetX);
                    float newYPos = handCenterY + (i * offsetY);
                    //newYPos = handCenterY ;//debugg

                    newPosition = new Vector3 (newXPos, newYPos, 0);
                    //Move to center                    
                    newPosition.x -= length / 2;
                    //Apply new Position
                    card.gameObject.transform.position = new Vector3 (newPosition.x, newPosition.y, newPosition.z); //I could not find the copy constructor

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
    public void rotateTable (int rotateAmount) {
        // Old version keep for animation
        // tableRoatation += (float) ((float) rotateAmount * (2 * Mathf.PI / MaxCardPerTable));
        // tableRoatation = tableRoatation % (2 * Mathf.PI);
        for(int i = 0; i < rotateAmount; i++){
            onBoardCars.rotateElements();
        }
        // Always check for null because if the table is empty get() returns null
        if(onBoardCars.get(0) != null){
            onBoardCars.get(0).OnEaten();
        }
        rearangeFielCards ();

    }
    public void rotateTable () {
        rotateTable (1);

    }
}