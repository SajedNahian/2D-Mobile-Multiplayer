using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerButtons : MonoBehaviour {
    [HideInInspector]
    public playerControls pControl;

    public void LeftUp ()
    {
        pControl.left = false;
    }

    public void LeftDown ()
    {
        pControl.left = true;
    }

    public void RightUp()
    {
        pControl.right = false;
    }

    public void RightDown()
    {
        pControl.right = true;
    }

    public void Shoot ()
    {
        pControl.Shoot();
    }

    public void Jump ()
    {
        pControl.Jump();
    }
}
