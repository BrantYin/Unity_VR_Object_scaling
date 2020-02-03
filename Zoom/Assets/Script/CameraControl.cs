using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public GameObject target;
	private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
	    tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
	    tf.LookAt(target.GetComponent<Transform>());
    }
}
