using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public abstract void ApplyEffect(PlayerController player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.GetComponent<PlayerController>());
            Destroy(gameObject);
        }
    }
}