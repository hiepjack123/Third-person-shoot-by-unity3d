using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region declare Marine Control,position,..
    public GameObject headStop;
    public Animator bodyAnimator;
    public Vector3 moveDirection;
    public float moveSpeed = 50.0f;//Add speed to the Marines using f when it is float type
    public Rigidbody head;//declare head of the Marine also use when he dies
    public LayerMask layerMask;//indicate what layer the ray will hit
    private Vector3 currentLookTarget = Vector3.zero;//Start position when the game start
    // Use this for initialization
    private CharacterController characterController;// add ChacterController
    #endregion
    #region Shake Camera
    public float[] hitForce;
    public float timeBetweenHits = 2.5f;
    private bool isHit = false;
    private float timeSinceHit = 0;
    private int hitNumber = -1;
    #endregion
    #region Declare Marine's Death
    public Rigidbody marineBody;
    public Rigidbody bullet;

    private bool isDead = false;
    #endregion
    #region HeathBar Of Marine
    public Slider heathBar;//declare heathBar

    #endregion



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        bodyAnimator.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {//This method we set when we interact
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//Vector 3 have 3 axes x,y,z
        characterController.SimpleMove(moveDirection * moveSpeed);//SimpleMove method have vector3 and speed
        if (moveDirection == Vector3.zero)
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
            bodyAnimator.SetBool("IsMoving", true);//If Marine move active animation "Walk"
        head.AddForce(transform.right * 150, ForceMode.Acceleration);//Code help the head shake
    }
    void FixedUpdate()
    {
       
      
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//use a ray with mouse
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);//Draw a green ray
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore)) { }//tell the Physic not activate trigger
        if (hit.point != currentLookTarget)
        {
            currentLookTarget = hit.point;
        }
        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);//Locate vector3 using raycasting, we ignore y axis
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);//make Marine look following the point
        //Quaternion which is used to determine rotation( make a rotation when use ray to look)
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);//Make marine look smooth with Lerp() method( following previous rotation)
        #region Player gets hit
        if (isHit)
        { timeSinceHit += Time.deltaTime; }
        if (timeSinceHit > timeBetweenHits)
        {
            isHit = false;
            timeSinceHit = 0;
        #endregion

        }
    }
    void OnTriggerEnter(Collider col)//when aliens come to Marine
    {
        if(col.gameObject.tag=="Alien")
        { 
            heathBar.value -= 15f;
        }
        if (heathBar.value <= 0)
        {
            Die();
        }
        Alien alien = col.gameObject.GetComponent<Alien>();
        if (alien != null)//If It has Aliens
        {
            if (!isHit)
            {
                hitNumber += 1;

                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();//get component to main camera

                //make camerashake
                cameraShake.Shake();
               

            }
            
            isHit = true;
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);//Sound when get hurt
        }
    }




   

  public void Die(){


        bodyAnimator.SetBool("IsMoving", false); //He cannot move anymore
        marineBody.transform.parent = null;//make him cannot move arround
        marineBody.isKinematic = false;
        marineBody.useGravity = true;//Fall down the floor
        bullet.transform.parent = null;

        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false;//Can't use his gun when he dies
        
        Destroy(head.gameObject.GetComponent<HingeJoint>());
        head.transform.parent = null;
        head.useGravity = true;
            
        SoundManager.Instance
        .PlayOneShot(SoundManager.Instance.marineDeath);
        Destroy(gameObject);
     
      }


   }


  


