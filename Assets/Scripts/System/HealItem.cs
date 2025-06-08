using UnityEngine;

public class HealItem : ItemBase
{
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(20);
            }
            Destroy(gameObject);
        }
    }

    public override void ApplyEffect(PlayerController player)
    {
        player.Heal(healAmount);
    }
}