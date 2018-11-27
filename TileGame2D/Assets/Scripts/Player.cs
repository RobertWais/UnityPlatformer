using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Animations;
using UnityEditor;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    //Change later
    [SerializeField] Projectile projectile;

    //State 
    //For Fireball Strictly
    bool isFacingRight = true;
    bool isAlive = true;
    float gravityAtFirst;
    int right = 1;

    //Cached Components References
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    CapsuleCollider2D myCollider;
    BoxCollider2D myBoxCollider;
    Collider2D fullCollider;




    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        fullCollider = GetComponent<Collider2D>();
        gravityAtFirst = myRigidBody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isAlive){
            return;
        }
        Run();
        Jump();
        FlipSprite(); 
        Climb();
        Die();
        Fire();
    }

    private void Run(){
        float controlFloat = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlFloat*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void Jump(){
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        //SPACE BTN
        if(CrossPlatformInputManager.GetButtonDown("Jump")){
            Vector2 velocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += velocityToAdd;
        }

    }

    private void Climb(){
        
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myAnimator.SetBool("Climbing", false);

            //For stalling on ladder
            myRigidBody.gravityScale = gravityAtFirst;
            return;
        }
        myRigidBody.gravityScale = 0f;
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow*climbSpeed);
        myRigidBody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }



    private void FlipSprite(){
        //If there is speed
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHorizontalSpeed){
            //+1 or -1
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
            print("Velocity: " + Mathf.Sign(myRigidBody.velocity.x));
            if(Mathf.Sign(myRigidBody.velocity.x).Equals(1f))
            {
                right = 1;
            }else{
                right = -1;
            }
        }
    }

    private void Die(){
        if (fullCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            Debug.Log("Die");
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
        }
    }






    /////////////////////////////
    /// 


   
    public void Fire()
    {
        //SPACE BTN
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {


             projectile.direction = right;
            //projectile.y = transform.position.y;
            //projectile.x = transform.position.x
            
            Instantiate(projectile, transform.position, projectile.transform.rotation);


        }
    }


}
