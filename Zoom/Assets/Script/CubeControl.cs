using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
	private Transform tf;
	public float speed;



	// Start is called before the first frame update
    void Start()
    {
	    tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
	    float moveHorizontal = Input.GetAxis ("Horizontal");
	    float moveVertical = Input.GetAxis ("Vertical");

	    Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

	    tf.position += movement;
    }
}
