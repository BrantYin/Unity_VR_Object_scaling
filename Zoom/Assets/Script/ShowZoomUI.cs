///<summary>
/// Show the zoom UI when pointer is moved to the target game object
/// This script need to be attached to the target of zooming
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;

public class ShowZoomUI : MonoBehaviour 
	, IPointerEnterHandler
	, IPointerExitHandler
{
	private HashSet<PointerEventData> hovers = new HashSet<PointerEventData>();

	public GameObject zoomUI;
	Camera m_MainCamera;
	public float proximityDistance;

	///<summary>
	/// Get the main camera
	/// </summary>
	void Start()
	{
		m_MainCamera = Camera.main;
	}

	///<summary>
	/// Show the zoom UI when the main camera is in proximityDistance with the target
	/// and the pointer is moved on the target
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (hovers.Add(eventData) && hovers.Count == 1)
		{
			Debug.Log(eventData);
			
			float dist = Vector3.Distance(m_MainCamera.transform.position, transform.position);

			if (dist <= proximityDistance)
			{
				zoomUI.SetActive(true);
			}
		}
	}

	///<summary>
	/// Hide the zoom UI "hideDelay" seconds after the pointer is moved away from the target
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		if (hovers.Remove(eventData) && hovers.Count == 0)
		{
			zoomUI.GetComponent<HideZoomUI>().InvokeHideZoomUI();
		}
	}
}
