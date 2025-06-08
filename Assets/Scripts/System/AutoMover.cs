using UnityEngine;

public class AutoMover : MonoBehaviour
{
    private PlayerController player; // ����
    private float baseMultiplier = 1f; // �ʿ� �� �ӵ� ������ �� ���� ����

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

    // Optional: ���� ������ �ʿ��� ���
    public void SetMultiplier(float multiplier)
    {
        baseMultiplier = multiplier;
    }
}