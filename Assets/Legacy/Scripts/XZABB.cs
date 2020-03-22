using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XZABB : MonoBehaviour
{
    public float left;
    public float right;
    public float down;
    public float up;

    public List<XZABB> checkAgainst;    //A list of other XZABBs to check collision against.

    private List<XZABB> colliding;

    public delegate void collisionEventHandler(GameObject go1, GameObject go2);  //Handles a collision (or similar event) between two XZABB-equipped objects.
    public event collisionEventHandler OnXZABBEnter;    //Called when two XZABBs enter each other
    public event collisionEventHandler OnXZABBLeave;    //Called when two XZABBs stop colliding or stop being inside each other
    public event collisionEventHandler OnXZABBInside;   //Called every update cycle when two XZABBs are inside each other

    private void Awake()
    {
        checkAgainst = new List<XZABB>();
        colliding = new List<XZABB>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if all the ones in CheckAgainst are colliding
        //This isn't limited to just the ones that are known to not already be in 'colliding'
        //This is because that would require Contains checks, which are a lot more expensive than just doing this.
        //TODO: Possibly change this to use the dedicated function AddTo... and RemoveFrom... ?
        foreach (XZABB xzabb in checkAgainst)
        {
            bool isColliding = IsColliding(xzabb);
            if(isColliding && !colliding.Contains(xzabb))
            {
                colliding.Add(xzabb);
                OnXZABBEnter?.Invoke(gameObject, xzabb.gameObject);
            }

            if(!isColliding && colliding.Contains(xzabb))
            {
                colliding.Remove(xzabb);
                OnXZABBLeave?.Invoke(gameObject, xzabb.gameObject);
            }

            if(isColliding)
            {
                OnXZABBInside?.Invoke(gameObject, xzabb.gameObject);
            }
        }
    }

    private bool IsColliding(XZABB xzabb)
    {
        return
            xzabb.transform.position.x - xzabb.left < transform.position.x + right &&
            xzabb.transform.position.x + xzabb.right > transform.position.x - left &&
            xzabb.transform.position.z - xzabb.down < transform.position.z + up &&
            xzabb.transform.position.z + xzabb.up > transform.position.z - down
        ;
    }

    public void AddToCheckAgainst(XZABB xzabb)
    {
        if (!checkAgainst.Contains(xzabb))
        {
            checkAgainst.Add(xzabb);
        }
    }

    public void ClearCheckAgainst()
    {
        checkAgainst = new List<XZABB>();
    }
}
