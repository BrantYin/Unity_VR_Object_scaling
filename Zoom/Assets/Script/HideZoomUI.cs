///<summary>
/// Hide the zoom UI certain amount of time after user moves the pointer away from canvas
/// This script need to be attached to the zoomCanvas
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;

public class HideZoomUI : MonoBehaviour 
	, IPointerEnterHandler
	, IPointerExitHandler
{
	private HashSet<PointerEventData> hovers = new HashSet<PointerEventData>();

	public float hideDelay;

	///<summary>
	/// Cancel the Invoked "SetZoomUIInactive" if the pointer
	/// is moved back to the canvas
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (hovers.Add(eventData) && hovers.Count == 1)
		{
			CancelInvoke();
		}
	}

	///<summary>
	/// Hide the zoom UI "hideDelay" seconds after the pointer is moved away from the canvas
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		if (hovers.Remove(eventData) && hovers.Count == 0)
		{
			InvokeHideZoomUI();
		}
	}

	///<summary>
	/// Call "SetZoomUIInactive" in "hideDelay" seconds
	/// </summary>
	public void InvokeHideZoomUI()
	{
		Invoke("SetZoomUIInactive", hideDelay);
	}

	///<summary>
	/// Disable the zoom UI
	/// </summary>
	public void SetZoomUIInactive()
	{
		gameObject.SetActive(false);
	}
}