using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankDownControl : MonoBehaviour
{
    [SerializeField] private GameObject[] listObject;
    [SerializeField] private Sprite changeSprite;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "player")
        {
            if(listObject.Length > 0 && gameObject.GetComponent<SpriteRenderer>().sprite != changeSprite){
                foreach(GameObject item in listObject){
                    if(item != null){
                        item.SetActive(!item.activeSelf);
                    }
                }
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite;
        }
    }
}
