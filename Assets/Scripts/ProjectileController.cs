using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
    
    public float moveSpeed = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);

        if (col.gameObject.tag == "Zombie")
        {
            Destroy(col.gameObject);
            Destroy(this);
            //Add Points
        }

    }
}
