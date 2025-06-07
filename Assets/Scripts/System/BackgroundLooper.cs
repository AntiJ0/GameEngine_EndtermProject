using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundLooper : MonoBehaviour
{
    private float scrollSpeed = 2f;
    private float width;

    private void Start()
    {
        width = GetComponent<TilemapRenderer>().bounds.size.x;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }

    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }
}