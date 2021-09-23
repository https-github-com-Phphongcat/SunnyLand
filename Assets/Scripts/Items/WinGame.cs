using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject ControlButtons;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "player" && winGame != null && !winGame.gameObject.activeSelf)
        {
            StartCoroutine(WinGameSunny());
        }
    }

    IEnumerator WinGameSunny()
    {
        yield return new WaitForSeconds(0.5f);
        winGame.gameObject.SetActive(true);
        ControlButtons.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
