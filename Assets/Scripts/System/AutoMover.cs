using UnityEngine;

public class AutoMover : MonoBehaviour
{
    private PlayerController player; // 참조
    private float baseMultiplier = 1f; // 필요 시 속도 배율을 줄 수도 있음

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (player != null)
        {
            float speed = player.GetMoveSpeed() * baseMultiplier;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    // Optional: 배율 조절이 필요한 경우
    public void SetMultiplier(float multiplier)
    {
        baseMultiplier = multiplier;
    }
}