/// <summary>
/// When an object is grabbed, change its scale by pulling away/moving closer two controllers
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.VRModuleManagement;

public class Scalable_Drag : MonoBehaviour {

    private bool scalingActivated = false;
    public float scalingFactor;

    private float prevDist;

    /// <summary>
    /// Initialize previous distance as 0
    /// </summary>
    void Start () {
        prevDist = 0.0f;
    }
	
    /// <summary>
    /// When scalingActivated is true and both triggers are pressed, 
    /// scale the game object base on the change of distance between two controllers
    /// </summary>
    void Update () {
        if (scalingActivated
            && ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger)
            && ViveInput.GetPressEx(HandRole.LeftHand, ControllerButton.Trigger))
            // Conditions for active scaling
        {
            var rightHandIndex = ViveRole.GetDeviceIndexEx(HandRole.RightHand);
            var leftHandIndex = ViveRole.GetDeviceIndexEx(HandRole.LeftHand);

            if (VRModule.IsValidDeviceIndex(rightHandIndex) && VRModule.IsValidDeviceIndex(leftHandIndex))
            {
                var rightHandState = VRModule.GetDeviceState(rightHandIndex); 
                var leftHandState = VRModule.GetDeviceState(leftHandIndex); 
                // Get device state for both hands

                float dist = Vector3.Distance(rightHandState.position, leftHandState.position);

                if (prevDist == 0)
                {
                    prevDist = dist;
                }
                else
                {
                    // Scaling the object based on distance change
                    gameObject.transform.localScale *= (1 + scalingFactor * (dist - prevDist));
                    prevDist = dist;
                }
            }
            else
            {
                Debug.Log("Scaling: controller state error");
            }
        }

        if (ViveInput.GetPressUpEx(HandRole.RightHand, ControllerButton.Trigger)
            || ViveInput.GetPressUpEx(HandRole.LeftHand, ControllerButton.Trigger))
        {
            prevDist = 0.0f;
        }
    }

    /// <summary>
    /// Enable scaling, called After Grabbed by Basic Grabbable(Script)
    /// </summary>
    public void ActiveScaling()
    {
        scalingActivated = true;
    }

    /// <summary>
    /// Disable scaling, called On Drop by Basic Grabbable(Script)
    /// </summary>
    public void DisableScaling()
    {
        scalingActivated = false;
    }
}
