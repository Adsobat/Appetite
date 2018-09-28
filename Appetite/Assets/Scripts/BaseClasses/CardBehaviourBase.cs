using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBehaviourBase : MonoBehaviour {
    // costs for gameplay
    public int cookCost = 1;
    public float calories = 10;
    public float thirst = 0;
    public float costMonney = 13;
    public cardTypes CardType;
    private int cardID; // this is a unique id to identify the cardprefab. ID is set from database.
   
    public TextMesh costText;
    public TextMesh cookCostText;

    private bool isDrageing = false;
    private Vector3 touchPosWorld;
    private RaycastHit[] hits;
    //ref of board
    private BoardBehavior board;
    private GamemanagerBehavior gameManager;
    private CustomerBehavior customer;
    
    //

    

    public void Start () {
        // // find the board		
        board = (BoardBehavior) FindObjectOfType (typeof (BoardBehavior));
        if (board == null) {
            Debug.Log ("Board ref == null");
            print ("NO BOARD FOUND");
            
        }
        gameManager = (GamemanagerBehavior) FindObjectOfType (typeof (GamemanagerBehavior));
        if (gameManager == null) {
            Debug.Log ("gameManager ref == null");
            print ("NO gameManager FOUND");
        }
        customer = (CustomerBehavior) FindObjectOfType (typeof (CustomerBehavior));
        if (customer == null) {
            Debug.Log ("customer ref == null");
            print ("NO customer FOUND");
        }
        //  Debug.Log("Start called.");
    }
    void Update () {
        UpdateUI ();
        // also using mouse down for testing

        // use for phone
        if (Input.touchCount > 0) {

            //just to be sure
            costMonney = 0;

            touchPosWorld = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
        }
        // use for PC
        if (Input.GetMouseButtonDown (0)) {

            touchPosWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);

            hits = Physics.RaycastAll (touchPosWorld, Camera.main.transform.forward);
            for (int i = 0; i < hits.Length; i++) {

                if (hits[i].transform == transform) {
                    isDrageing = true;
                    board.startHolding (this);
                }
            }

        }
        if (Input.GetMouseButtonUp (0)) {
            if (isDrageing) {
                board.stopHolding (this);
            }

            isDrageing = false;

        }
        if (isDrageing) {
            touchPosWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector3 newPos = new Vector3 (touchPosWorld.x, touchPosWorld.y, 0.0f);
            transform.position = newPos;

        }

    }

    public virtual void OnEaten () {
        gameManager.addMonney (costMonney);
        customer.Eat (this);

        Destroy (gameObject); // DIRTY TODO
    }
    public virtual void OnPlayed () {
        //OnEaten ();
    }
    public virtual void OnRemoved () {

    }
    public virtual void OnEnhanced () {

    }
    public virtual void OnDrawn () {

    }
    private void UpdateUI () {
        if (costText != null) {
            costText.text = costMonney + " €";
        }
        if (cookCostText != null) {
            cookCostText.text = cookCost.ToString ();
        }

    }
    public bool isPlayable () {
        return board.getCooks () >= cookCost;
    }

}