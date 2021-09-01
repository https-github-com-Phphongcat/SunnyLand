using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private Renderer scrollRend;

    // Update is called once per frame
    void Update()
    {
        scrollRend.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0f);
    }
}
