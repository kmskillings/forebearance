using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class represents a tray that board cards can be placed into.
//It handles the card glow effects, arrangement of cards in the tray, etc.
//It also presents the card abstractions to the actual game component of the program.
//It should be attached to the card tray object.
public class CardTray : MonoBehaviour
{
    /*public string parameterCardHovoringName;

    public GameObject[] boardCardGOs = new GameObject[4];   //Lists all the gameobjects of the board cards currently in the tray.
    public GameObject[] cardSlots = new GameObject[4];
    public int numberOfCards;   //The number of cards currently in the tray
    public GameObject[] cardTrayGlows;  //Lists all the card tray glow gameobjects in this cardtray. Order matters, the one with 4 cards should go in index 0, etc.
                                        //Basically, the glow that represents that the tray has n cards should go in index n

    private bool cardHovoring = false;
    
    // Start is called before the first frame update
    void Start()
    {
        PickUpable.OnPickedUp += CheckPickedUpCard;
        PickUpable.OnDropped += DeactivateAllGlows;

        foreach(GameObject glow in cardTrayGlows) {
            XZABB xzabb = glow.GetComponent<XZABB>();
            if(xzabb == null)
            {
                Debug.Log("An object named " + glow.name + " is in the cardTrayGlows array of " + name + " and does not have an XZABB attached.");
                return;
            }

            xzabb.OnXZABBEnter += SetGlowHovoringTrue;
            xzabb.OnXZABBLeave += SetGlowHovoringFalse;
        }
    }

    public void PickUpCard(GameObject go)
    {
        CheckPickedUpCard(go);
    }

    public void DropCard(GameObject go)
    {
        DeactivateAllGlows(go);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Responds to a card being picked up by checking whether this tray can accept it, then turning on the appropriate glow if it can.
    public void CheckPickedUpCard(GameObject pickedUpCard)
    {
        //For now, everything that can be picked up can be put in any tray
        if (CanCardBePlaced(pickedUpCard))
        {
            ActivateGlow();

            //Adds the card to the appropriate tray's checkAgainst.
            AddToGlowCheckAgainst(pickedUpCard);
        }
    }

    //Responds to a card being dropped by deactivating all glows
    public void DeactivateAllGlows(GameObject droppedCard)
    {
        foreach(GameObject glow in cardTrayGlows)
        {
            //Also clears their checkAgainsts (and that of the droppedCard.
            glow.GetComponent<XZABB>().ClearCheckAgainst();
            droppedCard.GetComponent<XZABB>().ClearCheckAgainst();

            glow.SetActive(false);
        }
        
    }

    private void ActivateGlow()
    {
        //Sets the active self of the approptiate tray
        cardTrayGlows[numberOfCards].SetActive(true);
    }

    private void AddToGlowCheckAgainst(GameObject go)
    {
        XZABB glowXzabb = cardTrayGlows[numberOfCards].GetComponent<XZABB>();
        if(glowXzabb == null)
        {
            Debug.Log("In the function AddToGlowCheckAgainst, the appropriate card tray glow gameobject does not have an attached XZABB.");
            return;
        }

        //go is guaranteed to have an xzabb attached, but check it here anyway.
        XZABB goXzabb = go.GetComponent<XZABB>();
        if(goXzabb == null)
        {
            Debug.Log("In the function AddToGlowCheckAgainst, the GameObject go has no xzabb attached.");
            return;
        }

        //Add each other to both checkAgainst's 
        glowXzabb.AddToCheckAgainst(goXzabb);
        goXzabb.AddToCheckAgainst(glowXzabb);
    }

    public void SetGlowBool(GameObject glow, GameObject card, bool value, string name)
    {
        Animator animator = glow.GetComponent<Animator>();
        if(animator == null)
        {
            Debug.Log("In function SetGlowFlag of CardTray Script attached to " + gameObject.name + ", no animator was found on GameObject glow.");
            return;
        }

        animator.SetBool(name, value);
    }

    public bool CanCardBePlaced(GameObject card)
    {
        if(numberOfCards >= 4)
        {
            return false;
        }

        return true;
    }

    public void SetGlowHovoringTrue(GameObject card, GameObject glow)
    {
        SetGlowBool(card, glow, true, parameterCardHovoringName);
        cardHovoring = true;
    }

    public void SetGlowHovoringFalse(GameObject card, GameObject glow)
    {
        SetGlowBool(card, glow, false, parameterCardHovoringName);
        cardHovoring = false;
    }
    */
}
