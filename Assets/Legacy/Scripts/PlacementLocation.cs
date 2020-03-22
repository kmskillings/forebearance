using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlacementLocation : MonoBehaviour
{
    //public GameObject glow;     //The gameobject representing the glow effect for when a card is pick up
    //private Animator glowAnimator;
    public string glowHoveringParameter;

    public GameObject[] slots;
    public GameObject[] contents;  //The thing that is placed in this location.
    public int contentsCount;
    public int slotsCount;
    private ISlotProvider slotProvider;

    private void Awake()
    {
        //glowAnimator = glow.GetComponent<Animator>();

        foreach(MonoBehaviour mono in GetComponents<MonoBehaviour>())
        {
            if(mono is ISlotProvider)
            {
                if(slotProvider == null)
                {
                    slotProvider = mono as ISlotProvider;
                }
                else
                {
                    Debug.Log("Multiple slot providers found on object " + name + ". Keeping first.");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        PickUpable.OnDropped += PickUpable_OnDropped;
        PickUpable.OnPickedUp += PickUpable_OnPickedUp;

        foreach (GameObject slot in slots)
        {
            XZABB xzabb = slot.GetComponent<XZABB>();
            xzabb.OnXZABBEnter += XZABB_OnXZABBEnter;
            xzabb.OnXZABBLeave += XZABB_OnXZABBLeave;
        }
    }

    private void XZABB_OnXZABBLeave(GameObject go1, GameObject other)
    {
        Animator glowAnimator = slotProvider.GetGlowAnimator();
        glowAnimator.SetBool(glowHoveringParameter, false);
    }

    private void XZABB_OnXZABBEnter(GameObject go1, GameObject other)
    {
        PickUpable pickUpable = other.GetComponent<PickUpable>();

        if (GetIsValid(pickUpable))
        {
            Animator glowAnimator = slotProvider.GetGlowAnimator();
            glowAnimator.SetBool(glowHoveringParameter, true);
        }
    }

    private void PickUpable_OnPickedUp(PickUpable pickedUp)
    {
        if (!GetIsValid(pickedUp)) return;

        slotProvider.GetActiveGlow().SetActive(true);

        XZABB pickedUpXZABB = pickedUp.GetComponent<XZABB>();
        if (pickedUpXZABB == null)
        {
            Debug.Log("An object that was picked up doesn't have a valid XZABB: " + pickedUp.gameObject.name);
        }
        XZABB slotXZABB = slotProvider.GetActiveSlot().GetComponent<XZABB>();
        slotXZABB.AddToCheckAgainst(pickedUpXZABB);
        pickedUpXZABB.AddToCheckAgainst(slotXZABB);
    }

    private void PickUpable_OnDropped(PickUpable go)
    {
        //Deactivates all glows
        foreach(GameObject slot in slots)
        {
            slot.transform.GetChild(0).gameObject.SetActive(false);
            slot.GetComponent<XZABB>().ClearCheckAgainst();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Places the pickUpable into the placement location
    public void PlacePickUpable(PickUpable pickUpable)
    {
        if (!GetIsValid(pickUpable)) return;


        pickUpable.transform.SetParent(slotProvider.GetActiveSlot().transform);
        pickUpable.transform.localPosition = Vector3.zero;

        contents[contentsCount] = pickUpable.gameObject;
        contentsCount++;
    }

    public void RemovePickUpable(PickUpable pickUpable)
    {
        int index = Array.IndexOf(contents, pickUpable.gameObject);
        if (index < 0) return;

        contents[index] = null;
        contentsCount--;

        //Goes through the lower contents and shifts them all up one
        for(int i = index + 1; i < contentsCount + 1; i++)
        {
            //Gets the first child of the slot object that has a PickUpable attached
            GameObject go = slots[i].GetComponentInChildren<PickUpable>().gameObject;

            //Moves it up one
            go.transform.SetParent(slots[i - 1].transform);
            go.transform.localPosition = Vector3.zero;
            contents[i - 1] = go;
        }
    }

    //Whether a particular PickUpable can be placed in this location
    public bool GetIsValid(PickUpable pickUpable)
    {
        return contentsCount < slotsCount;
    }
}
