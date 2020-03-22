using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class represents anything that can be picked up.
//It handles the mouse clicking and dragging to move stuff.
//It also handles knowing where objects can be placed or not (with the help of another script attached to the same object)
public class PickUpable : MonoBehaviour
{

    public bool isPickedUp;         //Whether or not the object is currently picked up
    public float dragPlaneHeight;   //The Y coordinate the object should stay at when it's being dragged around
    public float normalHeight;      //The Y coordinate the object should stay at when it's not being dragged around
    private Plane dragPlane;

    public delegate void PickUpHandler(PickUpable go);

    public static event PickUpHandler OnPickedUp;   //Static events for picking up and dropping stuff. They're static so that other scripts can subscribe to only one.
    public static event PickUpHandler OnDropped;

    private XZABB xzabb;

    public PlacementLocation prospectivePL;
    public PlacementLocation currentPL;

    //When the object is clicked, start dragging it
    void OnMouseDown()
    {
        isPickedUp = true;
        OnPickedUp?.Invoke(this);
    }

    //When the object is let go, stop dragging it
    private void OnMouseUp()
    {
        OnDropped?.Invoke(this);
        xzabb.ClearCheckAgainst();

        isPickedUp = false;

        if (prospectivePL != currentPL)
        {
            prospectivePL.PlacePickUpable(this);
            currentPL.RemovePickUpable(this);
            currentPL = prospectivePL;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    private void Awake()
    {
        dragPlane = new Plane(Vector3.down, dragPlaneHeight);
        xzabb = GetComponent<XZABB>();
        prospectivePL = currentPL;
    }

    private void Xzabb_OnXZABBLeave(GameObject go1, GameObject go2)
    {
        prospectivePL = currentPL;
    }

    private void Xzabb_OnXZABBEnter(GameObject go1, GameObject other)
    {
        //Checks if the other XZABB has any placement locations
        PlacementLocation pl = other.GetComponentInParent<PlacementLocation>();
        if (pl == null)
        {
            return;
        }

        if (pl.GetIsValid(this))
        {
            //Sets the currentPlacementLocation to pl. This means that, if it's dropped, it will go there
            prospectivePL = pl;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(
            transform.position.x,
            normalHeight,
            transform.position.z
        );

        xzabb.OnXZABBEnter += Xzabb_OnXZABBEnter;
        xzabb.OnXZABBLeave += Xzabb_OnXZABBLeave;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickedUp)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(mouseRay, out float distance))
            {
                transform.position = mouseRay.GetPoint(distance);
            }
        }
    }
}
