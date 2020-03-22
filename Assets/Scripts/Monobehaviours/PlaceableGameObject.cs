using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableGameObject : MonoBehaviour
{
    public Placeable placeable;

    public bool isLocked;

    public static float dragHeight = 0.5f;     //The y-coordinate the object goes to when being dragged
    private Plane dragPlane;

    private bool isBeingDragged = false;

    private Vector3 lastPosition;

    public List<IPlacementLocation> placementLocationQueue;
        //A list of all the IPlacementLocations that this object is in, inner first.

    private IPlacementLocation currentPlacementLocation;
        //Where this object was/is placed before it was picked up

    public static PlaceableGameObject dragged;

    public delegate void onPickedUpHandler(PlaceableGameObject placeableGO);
    public static event onPickedUpHandler OnPickedUp;
    public static event onPickedUpHandler OnDropped;

    private void Awake()
    {
        dragPlane = new Plane(Vector3.down, dragHeight);
        lastPosition = transform.position;
        placementLocationQueue = new List<IPlacementLocation>();
    }

    private void Start()
    {
        placeable = new Placeable("demo");

        placeable.AddTag(PlaceableTag.tagBlueSkillToken);
        //placeable.AddTag(PlaceableTag.tagGreenSkillToken);
        //Debug.Log("done");
    }

    private void OnMouseDown()
    {
        if (isLocked) return;
        isBeingDragged = true;
        
        if(dragged == null)
        {
            dragged = this;
        }
        else
        {
            throw new System.Exception("Error: Multiple PlaceableGameObjects are being dragged at once: '" + name + "' and '" + dragged.name + "'");
        }

        currentPlacementLocation?.Remove(this);
        transform.parent = null;
        //if (currentPlacementLocation != null) placementLocationQueue.Add(currentPlacementLocation);
        OnPickedUp?.Invoke(this);
    }

    private void OnMouseUp()
    {
        isBeingDragged = false;
        dragged = null;
        OnDropped?.Invoke(this);
        if (placementLocationQueue.Count == 0 || !placementLocationQueue[0].CanBePlaced(this))  //If it can't be placed where it is, either due to being off-screen or over an unacceptable location
        {
            currentPlacementLocation.PlaceBack(this);   //Place it back where it was, even if it's an unacceptable place
        }
        else
        {
            placementLocationQueue[0].Place(this);
            currentPlacementLocation = placementLocationQueue[0];
        }

        placementLocationQueue = new List<IPlacementLocation>();    //Resets the queue
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBeingDragged) return;

        //Ignore collisions between this and any children
        if (other.transform.IsChildOf(transform)) return;
        
        //Check if the collider has an IPlacementLocation attached. If it does, add it to the queue and return
        foreach (MonoBehaviour script in other.GetComponents<MonoBehaviour>())
        {
            if(script is IPlacementLocation placementLocation)
            {
                placementLocationQueue.Remove(placementLocation);
                placementLocationQueue.Insert(0, placementLocation);
                Debug.Log("Entered collider of '" + other.name + "'");
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isBeingDragged) return;
        
        //Check if the collider has an IPlacementLocation attached. If it does, remove it from the queue. Do this for all/any attached.
        foreach (MonoBehaviour script in other.GetComponents<MonoBehaviour>())
        {
            if (script is IPlacementLocation placementLocation)
            {
                placementLocationQueue.Remove(placementLocation);
            }
        }
    }

    private void Update()
    {
        if (isBeingDragged)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(mouseRay, out float distance))
            {
                transform.position = mouseRay.GetPoint(distance);
            }
        }
    }
}