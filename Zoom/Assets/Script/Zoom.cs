/// <summary>
/// Upscale/downscale a game object using a plus and minus button
/// Make sure the "ShowZoomUI" script is attached to the target
/// and the "HideZoomUI" script is attached to the zoomCanvas
/// </summary>



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zoom : MonoBehaviour
{
	public Canvas zoomUICanvas;
	public GameObject target;
	public float scalingStep;

	Camera m_MainCamera;

	private Vector3 fellowOffset;

	///<summary>
	/// Get the fellowing offset for zoom UI based on its initial position
	/// Get the main camera
	/// </summary>
    void Start()
    {
	    fellowOffset = zoomUICanvas.transform.position - target.transform.position;
	    m_MainCamera = Camera.main;
    }

	///<summary>
	/// The zoom UI fellows the target when its move,
	/// and keep facing the main camera
	/// </summary>
    void Update()
    {
	    zoomUICanvas.transform.position = target.transform.position + fellowOffset;
	    zoomUICanvas.transform.LookAt(m_MainCamera.transform);
	    zoomUICanvas.transform.Rotate(0, 180, 0);
    }

	///<summary>
	/// Upscale the target by scalingStep, change the fellowOffset at the same timne
	/// Called by the plus bottun
	/// </summary>
    public void plus()
    {
	    target.transform.localScale *= scalingStep + 1;
		fellowOffset *= scalingStep + 1;
    }

	///<summary>
	/// Dowbscale the target by scalingStep, change the fellowOffset at the same timne
	/// Called by the minus bottun
	/// </summary>
    public void minus()
    {
	    target.transform.localScale *= 1 - scalingStep;
	    fellowOffset *= 1 - scalingStep;
    }
}
