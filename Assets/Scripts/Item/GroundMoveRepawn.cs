using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoveRepawn : MonoBehaviour
{
    [SerializeField] private Vector2 LeftPoint;
    [SerializeField] private Vector2 RightPoint;
    [SerializeField] private float Speed;
    [SerializeField] private bool MoveToRightFirst;
    
    [SerializeField] private bool MoveVertical_LeftIsTop_RightIsBottom;

    void AutoMoveRepawn()
    {
        if(MoveVertical_LeftIsTop_RightIsBottom){
            if(MoveToRightFirst){
                if(transform.position.y <= RightPoint.y){
                    MoveToRightFirst = false;
                }else{
                    transform.position = Vector2.MoveTowards(transform.position, RightPoint, Speed * Time.deltaTime);
                }
            }else{
                if(transform.position.y >= LeftPoint.y){
                    MoveToRightFirst = true;
                }else{
                    transform.position = Vector2.MoveTowards(transform.position, LeftPoint, Speed * Time.deltaTime);
                }
            }
        }else{
            if(MoveToRightFirst){
                if(transform.position.x >= RightPoint.x){
                    MoveToRightFirst = false;
                }else{
                    transform.position = Vector2.MoveTowards(transform.position, RightPoint, Speed * Time.deltaTime);
                }
            }else{
                if(transform.position.x <= LeftPoint.x){
                    MoveToRightFirst = true;
                }else{
                    transform.position = Vector2.MoveTowards(transform.position, LeftPoint, Speed * Time.deltaTime);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player"){
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player"){
            collision.gameObject.transform.parent = null;
        }
    }

    void Update()
    {
        AutoMoveRepawn();
    }
}
