using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {

    public float lerpRate = 15;
    public float heightFromTarget = 1;
    public float offsetX = 1;
    public float offsetZ = 1;
    public GameObject followTarget;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(followTarget.transform.position.x + offsetX, followTarget.transform.position.y + heightFromTarget, followTarget.transform.position.z + offsetZ),lerpRate);
	}
}
