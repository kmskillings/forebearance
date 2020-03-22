using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotChain : MonoBehaviour
{
    public Slot[] slots;

    private void Awake()
    {
        PlaceableGameObject.OnPickedUp += OnPlaceableGOPickedUp;
        PlaceableGameObject.OnDropped += OnPlaceableGODropped;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlaceableGOPickedUp(PlaceableGameObject placeableGO)
    {
        //Forwards the event to the next slot, if any
        foreach(Slot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.Ready(placeableGO);
                return;
            }
        }
    }

    void OnPlaceableGODropped(PlaceableGameObject placeableGO)
    {
        foreach(Slot slot in slots)
        {
            slot.UnReady(placeableGO);
        }
    }
}
