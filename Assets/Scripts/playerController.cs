using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    // Game Components
    public CharacterController2D controller;
    public Animator anim;

    public BoxCollider2D boxCol;
    public CircleCollider2D circleCol;
    
    public Text scoreText;


    public float movementSpeed;
    float horizontalMovementSpeed;
    bool isJumping = false;
    bool isHurt = false;
    int score;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();

        isJumping = false;
        isHurt = false;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = score.ToString();

        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;

        anim.SetFloat("speed", Mathf.Abs(horizontalMovementSpeed));
        if(Input.GetButtonDown("Jump")) {
            isJumping = true;
            anim.SetBool("isJumping", true);

              isJumping = true;
                anim.SetBool("isJumping", true);
        }

      

 
    }

    public void OnLanding() {
        Debug.Log("Landing");
        isJumping = false;
        anim.SetBool("isJumping", false);
    }

    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag == "gem"){
            Destroy(col.gameObject);
            score++;
        }

        else if(col.gameObject.tag == "Enemy" ) {
            if(isJumping) {
                Destroy(col.gameObject);
                score++;
            } else {
                anim.SetBool("isHurt", true);

                boxCol.enabled = false;
                circleCol.enabled = false;
            }
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, isJumping);
        //Debug.Log(isJumping);
    }

    
}
