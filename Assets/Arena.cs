using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {
    public GameObject player;
    public Transform elevator;//Position of the elevator
    public Animator arenaAnimator;
    public Animator playerStepInto;
    public Gun gunEnable;
    public Rigidbody head;
    public Rigidbody bullet;
  
    private SphereCollider enterSphere;//When player enter will activ




	// Use this for initialization
	void Start () {

        arenaAnimator = GetComponent<Animator>();
        enterSphere = GetComponent<SphereCollider>();
        playerStepInto.GetComponent<Animator>();
        head.GetComponent<Rigidbody>();
         gunEnable.GetComponent<Gun>();
         bullet.GetComponent<Rigidbody>();
	}
    void OnTriggerEnter(Collider other)
    {
      
        player.transform.parent = elevator.transform;//player will in the elevator object parent
        player.GetComponent<PlayerController>().enabled = false;//Make player cannot move anymore
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);//sound when elevator arrives;
        arenaAnimator.SetBool("OnElevator", true);//anination'll active
        playerStepInto.SetBool("IsMoving", false);//If player steps into he not moving
       
        gunEnable.enabled = false;//player cannot shoot or do anything
        gunEnable.CancelInvoke();//
        head.isKinematic = true;//this make head isn't controlled by force we add
        head.angularVelocity = Vector3.zero;//
        head.velocity = Vector3.zero;// no more velocity

       
      

    }
    public void ActiveFlatform()//Attached to the Animation
    {
        enterSphere.enabled = true;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
