using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private int point;
    private Rigidbody2D rb;
    private Animator amin;

    [SerializeField] private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        amin = gameObject.GetComponent<Animator>();
    }

    private void ChangeStateAnimation()
    {
        if(playerMovement.GetIsRight() || playerMovement.GetIsLeft()){
            amin.SetFloat("run", 1);
        }else{
            amin.SetFloat("run", 0);
        }if (rb.velocity.y < 0){
            amin.SetBool("isJump", false);
            amin.SetBool("isFall", true);
        }else if(rb.velocity.y > 0.01){
            amin.SetBool("isJump", true);
            amin.SetBool("isFall", false);
        }else{
            amin.SetBool("isJump", false);
            amin.SetBool("isFall", false);

            playerMovement.SetJumpEnabled(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            if(rb.velocity.y > 0){
                playerMovement.SetJumpEnabled(false);
            }else{
                playerMovement.SetJumpEnabled(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "gem")
        {
            point += 1;
            Debug.Log(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeStateAnimation();
    }
}
