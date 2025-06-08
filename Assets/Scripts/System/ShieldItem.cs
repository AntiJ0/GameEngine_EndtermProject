using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GainShield(); // ���� ���� ȣ��
                Debug.Log(">> �÷��̾ ��ȣ�� �������� ȹ���߽��ϴ�.");
            }

            Destroy(gameObject);
        }
    }
}