using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleControl : MonoBehaviour
{
    [SerializeField] private Vector2 topLeftPoint;
    [SerializeField] private Vector2 bottomLeftPoint;
    [SerializeField] private Vector2 topRightPoint;
    [SerializeField] private Vector2 bottomRightPoint;
    [SerializeField] private Transform target;
    [SerializeField] private Transform thisTranform;
    [SerializeField] private AIPath aIPath;
    [SerializeField] private AIDestinationSetter aIDestinationSetter;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone()){
            if(target == null){
                target = GameObject.FindGameObjectWithTag("player").transform;
            }

            if(target.position.x >= topLeftPoint.x && target.position.y <= topLeftPoint.y &&
                target.position.x >= bottomLeftPoint.x && target.position.y >= bottomLeftPoint.y &&
                target.position.x <= topRightPoint.x && target.position.y <= topRightPoint.y &&
                target.position.x <= bottomRightPoint.x && target.position.x >= bottomRightPoint.y){
                    aIDestinationSetter.target = GameObject.FindGameObjectWithTag("player").transform;
            }else{
                aIDestinationSetter.target = thisTranform;
            }  
        }
    }

    void Update()
    {
        if(aIPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
        }else if(aIPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
