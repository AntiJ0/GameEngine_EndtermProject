using UnityEngine;

public class ScoreBoostItem : ItemBase
{
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateScoreBoost(10f);
            }
            Destroy(gameObject);
        }
    }

    public override void ApplyEffect(PlayerController player)
    {
        player.ActivateScoreBoost(duration);
    }
}