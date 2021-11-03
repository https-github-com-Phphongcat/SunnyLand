using UnityEngine;

public class Opossum : MonoBehaviour
{
    [SerializeField] private float rightPoint;
    [SerializeField] private float leftPoint;
    [SerializeField] private float speedToWalk = 12f;
    [SerializeField] private bool moveToRight;

    private void AutoMoveRespawn()
    {
        var scaler = transform.localScale;
        var position = transform.position;
        
        moveToRight = moveToRight switch
        {
            true when position.x >= rightPoint => false,
            false when position.x <= leftPoint => true,
            _ => moveToRight
        };
        scaler.x = moveToRight ? -1 : 1;

        transform.localScale = scaler;
        transform.position = Vector2.MoveTowards(position,
            new Vector2(moveToRight ? rightPoint : leftPoint, position.y), speedToWalk * Time.deltaTime);
    }

    void Update()
    {
        AutoMoveRespawn();
    }
}
