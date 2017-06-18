using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//Using Artifical Intelligence
using UnityEngine.UI;
using UnityEngine.Events;


public class Alien : MonoBehaviour
{
    public int enemyHeaths = 100;
    public float destructTime = 1f;
    
    
    public Transform target;
    private NavMeshAgent agent;
    public float navigationUpdate;
    public float navigationTime = 0;
    public UnityEvent OnDestroy;
    public Rigidbody alienHead;
    private bool isAlive=true;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();//Get compent for alien

    }

    // Update is called once per frame
    void Update()
    {
        navigationTime += Time.deltaTime;

        if (target != null)
            if (navigationTime > navigationUpdate)
            {
                agent.destination = target.position;
                navigationTime = 0;
            }
    }
    void OnTriggerEnter(Collider col)
    {
       if(col.gameObject.tag=="Bullet")
       { enemyHeaths -= 5; }
       {if (enemyHeaths <= 0) 
       {
           Die();//Hit by bullet will die
       SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);}
        //call the method PlayOneShot when aliens get hit
    }



    }
    public void Die()
    {
        isAlive = false;
        alienHead.GetComponent<Animator>().enabled = false;
        alienHead.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        alienHead.gameObject.GetComponent<Rigidbody>().useGravity = true;// It will enbale gravity
        alienHead.gameObject.GetComponent<SphereCollider>().enabled = true;//activate invisible capsule collider
        alienHead.gameObject.transform.parent = null;//return null use for stopping all alien's action
        alienHead.velocity = new Vector3(0, 26f, 3f); //head will get away when aliens die
        alienHead.GetComponent<SelfDestruct>().Initiate();//call the self destruct to destroy alien when it dies

            
        OnDestroy.Invoke();//call event onDestroy
        OnDestroy.RemoveAllListeners();
        Destroy(gameObject);//Delete aliens
    }
  

    }

        
	

