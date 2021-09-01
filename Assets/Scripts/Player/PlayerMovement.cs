using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int flip;
    private bool isRight, isLeft, isJump, jumpEnabled;

    [SerializeField] private float speed;

    private Rigidbody2D rb;

    public void SetIsRight(bool enable) { isRight = enable; }
    public bool GetIsRight() { return isRight; }

    public void SetIsLeft(bool enable) { isLeft = enable; }
    public bool GetIsLeft() { return isLeft; }

    public void SetIsJump(bool enable) { isJump = enable; }
    public bool GetIsJump() { return isJump; }

    public void SetJumpEnabled(bool enable) { jumpEnabled = enable; }
    public bool GetJumpEnabled() { return jumpEnabled; }

    public float GetSpeed() { return speed; }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Awake()
    {   
        isRight = false;
        isLeft = false;
        isJump = false;
        jumpEnabled = true;
        flip = 1;
    }

    private void Movement()
    {
        Vector2 tempPosition = rb.position;

        if(isRight){
            flip = 1;
            tempPosition.x += speed * Time.deltaTime;
        }else if(isLeft){
            flip = -1;
            tempPosition.x -= speed * Time.deltaTime;
        }if(isJump && jumpEnabled){
            jumpEnabled = false;
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }

        rb.position = tempPosition;

        FlipCharacter(flip);
    }

    private void FlipCharacter(int state)
    {
        Vector3 trans = transform.localScale;

        if(trans.x == state){
            return;
        }

        trans.x = state;
        transform.localScale = trans;
    }

    void BlockMovement()
    {
        float dist = (transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        Vector3 playerSize = GetComponent<Renderer>().bounds.size / 2;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder + playerSize.x / 2, rightBorder - playerSize.x / 2),
            transform.position.y, transform.position.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        BlockMovement();
    }
}
