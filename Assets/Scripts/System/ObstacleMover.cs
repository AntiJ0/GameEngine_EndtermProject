using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private float moveSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // 화면 왼쪽 바깥으로 나가면 제거
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}