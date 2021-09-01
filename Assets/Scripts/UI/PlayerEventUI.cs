using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventUI : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public void OnRightButtonDown() { playerMovement.SetIsRight(true); }
    public void OnRightButtonUp() { playerMovement.SetIsRight(false); }

    public void OnLeftButtonDown() { playerMovement.SetIsLeft(true); }
    public void OnLeftButtonUp() { playerMovement.SetIsLeft(false); }

    public void OnJumpButtonDown() { playerMovement.SetIsJump(true); }
    public void OnJumpButtonUp() { playerMovement.SetIsJump(false); }
}
