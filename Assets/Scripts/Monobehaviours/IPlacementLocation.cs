using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlacementLocation 
{
    //This is called when a PlaceableGameObject has just been dropped onto the IPlacementLocation.
    //It's responsible for assigning the PGO to the correct location, incorporating it into the logic, etc.
    void Place(PlaceableGameObject placeableGO);

    //This is called when a PlaceableGameObject needs to be 'put back' into this IPlacementLocation
    void PlaceBack(PlaceableGameObject placeableGO);

    //This is called when a PlaceableGameObject is picked up from an IPlacementLocation.
    //It's responsible for removing the PGO from the logic.
    bool Remove(PlaceableGameObject placeableGO);

    bool CanBePlaced(PlaceableGameObject placeableGO);
}
