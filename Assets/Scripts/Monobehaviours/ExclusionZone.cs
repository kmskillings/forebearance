using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclusionZone : MonoBehaviour, IPlacementLocation
{
    public Collider zoneCollider;

    public bool CanBePlaced(PlaceableGameObject placeableGO)
    {
        return false;
    }

    public void Place(PlaceableGameObject placeableGO)
    {
        throw new System.Exception("Tried to place object '" + placeableGO.name + "' into ExclusionZone '" + name + "'");
    }

    public void PlaceBack(PlaceableGameObject placeableGO)
    {
        throw new System.Exception("Tried to place back object '" + placeableGO.name + "' into ExclusionZone '" + name + "'");
    }

    public bool Remove(PlaceableGameObject placeableGO)
    {
        return false;
    }

    private void Awake()
    {
        //zoneCollider = GetComponent<Collider>();
        zoneCollider.enabled = false;

        PlaceableGameObject.OnPickedUp += OnPlaceableGOPickedUp;
        PlaceableGameObject.OnDropped += OnPlaceableGODropped;
    }

    public void OnPlaceableGOPickedUp(PlaceableGameObject placeableGO)
    {
        zoneCollider.enabled = true;
    }

    public void OnPlaceableGODropped(PlaceableGameObject placeableGO)
    {
        zoneCollider.enabled = false;
    }
}
