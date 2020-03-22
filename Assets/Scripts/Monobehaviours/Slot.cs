using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour, IPlacementLocation
{
    public PlaceableGameObject contents;

    public Vector3 contentsPosition;

    public ITagCondition condition;

    private Collider hoverZoneCollider;

    public bool isNextSlotInChain = false;

    private void Awake()
    {
        hoverZoneCollider = GetComponent<Collider>();

        condition = new TagConditionTrue();
    }

    // Start is called before the first frame update
    void Start()
    {
        hoverZoneCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsEmpty()
    {
        return contents == null;
    }

    public void Ready(PlaceableGameObject placeableGO)
    {
    //Sets up to accept it
    hoverZoneCollider.enabled = true;

    //Trigger some sort of cosmetic change?
    }

    public void UnReady(PlaceableGameObject placeableGO)
    {
        hoverZoneCollider.enabled = false;
    }

    public void Place(PlaceableGameObject placeableGO)
    {
        placeableGO.transform.parent = transform;
        placeableGO.transform.localPosition = contentsPosition;
        contents = placeableGO;
    }

    public void PlaceBack(PlaceableGameObject placeableGO)
    {
        Place(placeableGO);
    }

    public bool Remove(PlaceableGameObject placeableGO)
    {
        if(placeableGO == contents)
        {
            contents.transform.parent = null;
            contents = null;
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanBePlaced(PlaceableGameObject placeableGO)
    {
        return condition.Evaluate(placeableGO.placeable);
    }
}