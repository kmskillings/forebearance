using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IPlacementLocation
{
    public ITagCondition condition;

    private bool isAcceptablePlaceablePickedUp = false;

    public float tableHeight;

    public List<PlaceableGameObject> contents;

    private Collider hoverZoneCollider;

    private Vector3 justRemovedPosition;

    private void Awake()
    {
        PlaceableGameObject.OnPickedUp += OnPlaceableGOPickedUp;
        PlaceableGameObject.OnDropped += OnPlaceableGODropped;
        hoverZoneCollider = GetComponent<Collider>();

        contents = new List<PlaceableGameObject>();

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

    public void OnPlaceableGOPickedUp(PlaceableGameObject placeableGO)
    {
        bool conditionSatisfied = condition.Evaluate(placeableGO.placeable);
        if (conditionSatisfied)
        {
            hoverZoneCollider.enabled = true;
            isAcceptablePlaceablePickedUp = true;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        //Don't do anything if the other wasn't the thing being dragged
        PlaceableGameObject otherPlaceableGO = other.GetComponent<PlaceableGameObject>();
        if(otherPlaceableGO == null || otherPlaceableGO != PlaceableGameObject.dragged)
        {
            return;
        }

        if (isAcceptablePlaceablePickedUp)
        {
            otherPlaceableGO.Accept(this);
        }
        isDraggedHovering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Don't do anything if the other wasn't the thing being dragged
        PlaceableGameObject otherPlaceableGO = other.GetComponent<PlaceableGameObject>();
        if (otherPlaceableGO == null || otherPlaceableGO != PlaceableGameObject.dragged)
        {
            return;
        }

        otherPlaceableGO.Unaccept(this);
        isDraggedHovering = false;
    }*/

    public void OnPlaceableGODropped(PlaceableGameObject placeableGO)
    {
        isAcceptablePlaceablePickedUp = false;
        hoverZoneCollider.enabled = false;
    }

    public void Place(PlaceableGameObject placed)
    {
        contents.Add(placed);
        placed.transform.parent = transform;
        placed.transform.position = new Vector3(placed.transform.position.x, tableHeight, placed.transform.position.z);
    }

    public void PlaceBack(PlaceableGameObject placed)
    {
        contents.Add(placed);
        placed.transform.position = justRemovedPosition;
    }

    public bool Remove(PlaceableGameObject removed)
    {
        justRemovedPosition = removed.transform.position;
        //removed.transform.parent = null;
        return contents.Remove(removed);
    }

    public bool CanBePlaced(PlaceableGameObject placeableGO)
    {
        return condition.Evaluate(placeableGO.placeable);
    }
}