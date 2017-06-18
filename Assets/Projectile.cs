using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
