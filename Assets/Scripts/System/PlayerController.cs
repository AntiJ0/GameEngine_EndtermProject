using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedIncreaseRate = 0.01f;
    public float maxSpeed = 20f;

    public int maxHP = 100;
    private int currentHP;

    public Image healthBarFill;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    public Animator animator;

    private enum ColorState { Normal, Red, Green, Yellow }
    private ColorState currentColor = ColorState.Normal;

    private bool hasShield = false;
    public GameObject shieldEffectPrefab;
    private GameObject shieldVisual;

    private bool isYellowBuffActive = false;
    private float yellowBuffEndTime = 0f;

    private void Start()
    {
        currentHP = maxHP;
        originalColor = spriteRenderer.color;
        animator.SetBool("isRunning", true);
        UpdateHealthUI();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 pos = transform.position;
        pos.y += vertical * 5 * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -2.7f, 3.0f);
        transform.position = pos;

        moveSpeed += speedIncreaseRate * Time.deltaTime;
        moveSpeed = Mathf.Min(moveSpeed, maxSpeed);

        FindObjectOfType<ScoreManager>()?.SetSpeed(moveSpeed);
        FindObjectOfType<ObstacleSpawner>()?.SetPlayerSpeed(moveSpeed);
        FindObjectOfType<ItemSpawner>()?.SetPlayerSpeed(moveSpeed);

        foreach (var looper in FindObjectsOfType<BackgroundLooper>())
        {
            looper.SetScrollSpeed(moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Obstacle obs = other.GetComponent<Obstacle>();
            int damage = obs != null ? obs.GetDamage() : 20;

            if (hasShield)
            {
                hasShield = false;
                if (shieldVisual != null) Destroy(shieldVisual);
                Destroy(other.gameObject);
                return;
            }

            currentHP -= damage;
            UpdateHealthUI();

            StartCoroutine(ChangeColorTemporarily(Color.red, 0.5f, ColorState.Red));
            Destroy(other.gameObject);

            if (currentHP <= 0)
                FindObjectOfType<GameManager>().GameOver();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHP / maxHP;
        }
    }

    IEnumerator ChangeColorTemporarily(Color tempColor, float duration, ColorState tempState)
    {
        currentColor = tempState;
        spriteRenderer.color = tempColor;
        yield return new WaitForSeconds(duration);

        if (isYellowBuffActive && Time.time < yellowBuffEndTime)
        {
            spriteRenderer.color = Color.yellow;
            currentColor = ColorState.Yellow;
        }
        else
        {
            spriteRenderer.color = originalColor;
            currentColor = ColorState.Normal;
        }
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        UpdateHealthUI();
        StartCoroutine(ChangeColorTemporarily(new Color(0.5f, 1f, 0.5f), 0.5f, ColorState.Green));
    }

    public void GainShield()
    {
        if (hasShield) return;

        hasShield = true;

        if (shieldEffectPrefab != null)
        {
            shieldVisual = Instantiate(shieldEffectPrefab, transform);
            shieldVisual.transform.localPosition = new Vector3(0.1f, 0.35f, 0f);
        }
    }

    public void ActivateScoreBoost(float duration)
    {
        yellowBuffEndTime = Time.time + duration;
        isYellowBuffActive = true;
        currentColor = ColorState.Yellow;
        spriteRenderer.color = Color.yellow;

        FindObjectOfType<ScoreManager>().SetScoreMultiplier(1.5f);
        StartCoroutine(EndScoreBoostAfter(duration));
    }

    IEnumerator EndScoreBoostAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        isYellowBuffActive = false;

        if (currentColor == ColorState.Yellow)
        {
            spriteRenderer.color = originalColor;
            currentColor = ColorState.Normal;
        }

        FindObjectOfType<ScoreManager>().SetScoreMultiplier(1f);
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}