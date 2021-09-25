using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [SerializeField] private float speed;

    public bool SavePoint;
    public int MaxHealth{get; private set;}

    private bool isRight;
    private bool isLeft;
    private bool isJump;

    private bool isJumpOrFall;
    private bool isGround;
    private bool isLadder;
    private bool isDead;

    public LayerMask GroundLayer;
    public LayerMask whatIsLadder;
    public AnimationStates animationStates;

    public void SetDefaultAllVariables()
    {
        SavePoint = false;

        IsDead(false);
        IsJump(false);
        IsLeft(false);
        IsRight(false);
        IsLadder(false);
        IsGround(false);
        
        IsJumpOrFall(true);
    }

    public void Speed(float inSpeed) {speed = inSpeed; }
    public float Speed(){ return speed; }

    public void IsRight(bool enable) { isRight = enable; }
    public bool IsRight() { return isRight; }

    public void IsLeft(bool enable) { isLeft = enable; }
    public bool IsLeft() { return isLeft; }

    public void IsJump(bool enable) { isJump = enable; }
    public bool IsJump() { return isJump; }

    public void IsGround(bool enable) { isGround = enable; }
    public bool IsGround() { return isGround; }

    public void IsJumpOrFall(bool enable){isJumpOrFall = enable;}
    public bool IsJumpOrFall() { return isJumpOrFall; }

    public void IsLadder(bool enable) { isLadder = enable; }
    public bool IsLadder() { return isLadder; }

    public void IsDead(bool isDeaded) { isDead = isDeaded; }
    public bool IsDead() { return isDead; }
}

[System.Serializable]
public class AnimationStates
{
    public string PLAYER_IDLE;
    public string PLAYER_RUN;
    public string PLAYER_JUMP;
    public string PLAYER_FALL;
    public string PLAYER_CLIMB;
    public string PLAYER_DEAD;
}
