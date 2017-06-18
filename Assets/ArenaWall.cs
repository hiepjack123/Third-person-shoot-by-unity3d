using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour {
    public Animator arenaAnimator;
	// Use this for initialization
	void Start () {
        GameObject arena = transform.parent.gameObject;
        arenaAnimator = arena.GetComponent<Animator>();
	}
	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag =="Player")
        arenaAnimator.SetBool("IsLowered", true);
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        arenaAnimator.SetBool("IsLowered", false);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
