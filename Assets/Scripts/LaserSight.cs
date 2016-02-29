using UnityEngine;
using System.Collections;

public class LaserSight : MonoBehaviour {

    private LineRenderer lr;
    private RaycastHit hit;
    public Transform firePosition;

    // Use this for initialization
    void Start ()
    {
        lr = GetComponent<LineRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);

        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            lr.SetPosition(1, hit.point);
        }
    }
}
