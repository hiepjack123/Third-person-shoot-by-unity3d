using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterAndWin : MonoBehaviour {
    public SphereCollider enterWinSphere;
    public Animator playerIdle;
    public Rigidbody head;

    public Gun gun;

	// Use this for initialization
	void Start () {
        enterWinSphere = GetComponent<SphereCollider>();
        playerIdle.GetComponent<Animator>();

        head.GetComponent<Rigidbody>();
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter()
    {
        playerIdle.SetBool("IsMoving",false);//he will not moving
        gun.enabled = false;//he cannot shoot

    
        
        
        

    }
}
