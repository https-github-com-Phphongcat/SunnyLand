using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject ControlButtons;
    [SerializeField] private Text text;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "player" && winGame != null && !winGame.gameObject.activeSelf)
        {
            StartCoroutine(WinGameSunny());
        }
    }

    IEnumerator WinGameSunny()
    {
        yield return new WaitForSeconds(0.35f);
        winGame.gameObject.SetActive(true);
        ControlButtons.gameObject.SetActive(false);
        text.text = "Score: " + FindObjectOfType<PlayerControl>().Score.ToString();
        Time.timeScale = 0;
    }
}
