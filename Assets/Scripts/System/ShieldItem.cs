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
                player.GainShield(); // 인자 없이 호출
                Debug.Log(">> 플레이어가 보호막 아이템을 획득했습니다.");
            }

            Destroy(gameObject);
        }
    }
}