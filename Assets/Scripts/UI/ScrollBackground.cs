using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private Renderer scrollRend;

    void Update()
    {
        scrollRend.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0f);
    }
}
