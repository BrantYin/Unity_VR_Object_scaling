/// <summary>
/// When grabbing an object, change its scale with the joystick
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Scalable_Joystick : MonoBehaviour {

    enum joystick {leftHand, rightHand, none}
	
    private bool scalingActivated = false;
    private joystick handGrabbing;
	
    public float scalingFactor;
    
    /// <summary>
    /// when scaling is actived (The object is grabbed),
    /// change its scale based on the Y axis value of the joystick
    /// </summary>
    void Update () {

        if (scalingActivated) 
        {
            float joystickY = 0.0f;
			
            switch (handGrabbing) 
            {
                case joystick.leftHand:
                    joystickY = ViveInput.GetAxisEx(HandRole.LeftHand, ControllerAxis.JoystickY);
                    break;
                case joystick.rightHand:
                    joystickY = ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.JoystickY);
                    break;
                case joystick.none:
                    Debug.Log("Scaling error: no trigger press, grabbed with another button?");
                    break;
            }

            gameObject.transform.localScale *= (1 + scalingFactor * joystickY);
        }
    }

    /// <summary>
    /// Enable scaling, check which hand is grabbing,
    /// called After Grabbed by Basic Grabbable(Script)
    /// </summary>
    public void ActiveScaling()
    {
        scalingActivated = true;
        GetHandGrabbing();
    }

    /// <summary>
    /// Disable scaling, called On Drop by Basic Grabbable(Script)
    /// </summary>
    public void DisableScaling()
    {
        scalingActivated = false;
    }

    /// <summary>
    /// Check which hand is grabbing the object by checking trigger pressing
    /// Note: May need to be changed if grabbing with other buttons
    /// </summary>
    private void GetHandGrabbing()
    {
        if (ViveInput.GetPressEx(HandRole.LeftHand, ControllerButton.Trigger)) 
        {
            handGrabbing = joystick.leftHand;
        } 
        else if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
        {
            handGrabbing = joystick.rightHand;
        }
        else 
        {
            handGrabbing = joystick.none;
        }
    }
}
