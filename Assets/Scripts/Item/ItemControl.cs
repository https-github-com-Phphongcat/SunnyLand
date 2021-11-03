using System.Collections;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    private Animator amin;
    [SerializeField] private float timeTodead;
    [SerializeField] private ItemType itemType;

    public ItemType ItemType(){return itemType;}
    // public void ItemType(ItemType inItemType) {itemType = inItemType;}

    void Start()
    {
        amin = gameObject.GetComponent<Animator>();
    }

    IEnumerator PlayAnimationDead()
    {
        amin.SetBool("dead", true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(timeTodead);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "player")
        {
            StartCoroutine(PlayAnimationDead());
        }
    }
}