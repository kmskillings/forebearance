using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTraySlotProvider : MonoBehaviour, ISlotProvider
{
    private PlacementLocation location;

    void Awake()
    {
        location = GetComponent<PlacementLocation>();
    }

    public GameObject GetActiveSlot()
    {
        return location.slots[location.contentsCount];
    }

    public Animator GetGlowAnimator()
    {
        return GetActiveSlot().GetComponentInChildren<Animator>();
    }

    public GameObject GetActiveGlow()
    {
        return GetActiveSlot().transform.GetChild(0).gameObject;
    }
}