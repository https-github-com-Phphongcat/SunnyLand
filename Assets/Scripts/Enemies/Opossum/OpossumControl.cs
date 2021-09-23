using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumControl : MonoBehaviour
{
    [SerializeField] private Vector2 LeftPoint;
    [SerializeField] private Vector2 RightPoint;
    [SerializeField] private float SpeedToWalk;
    [SerializeField] private bool MoveToRightFirst;

    void AutoMoveRepawn()
    {
        if(MoveToRightFirst){
            if(transform.position.x >= RightPoint.x){
                MoveToRightFirst = false;
            }else{
                transform.localScale = new Vector3(-1f, 1f, 1f);
                    transform.position = Vector2.MoveTowards(transform.position, RightPoint, SpeedToWalk * Time.deltaTime);
                }
        }else{
            if(transform.position.x <= LeftPoint.x){
                MoveToRightFirst = true;
            }else{
                transform.localScale = new Vector3(1f, 1f, 1f);
                transform.position = Vector2.MoveTowards(transform.position, LeftPoint, SpeedToWalk * Time.deltaTime);
            }
        }
    }

    void Update()
    {
        AutoMoveRepawn();
    }
}
