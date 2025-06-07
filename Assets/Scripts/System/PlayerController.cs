using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도 관련")]
    public float moveSpeed = 5f;           // 점수/배경/장애물용 속도
    public float maxSpeed = 20f;
    public float speedIncreaseRate = 0.01f;

    [Header("Y축 이동")]
    public float verticalMoveSpeed = 5f;   // 실제 입력 반영 속도 (고정값)

    [Header("체력 관련")]
    public int maxHP = 3;
    private int currentHP;

    [Header("UI & 시각 효과")]
    public Image healthBarFill;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("애니메이션")]
    public Animator animator;

    private void Start()
    {
        currentHP = maxHP;
        originalColor = spriteRenderer.color;

        if (animator != null)
            animator.SetBool("isRunning", true);

        UpdateHealthUI();
    }

    private void Update()
    {
        // 위아래 이동
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = Vector2.up * vertical * verticalMoveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Y 위치 제한
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -2.7f, 3.0f);
        transform.position = pos;

        // 이동 속도 증가 (배경, 장애물, 점수용)
        moveSpeed += speedIncreaseRate * Time.deltaTime;
        moveSpeed = Mathf.Min(moveSpeed, maxSpeed);

        // 속도 전달
        FindObjectOfType<ScoreManager>()?.SetSpeed(moveSpeed);
        FindObjectOfType<ObstacleSpawner>()?.SetPlayerSpeed(moveSpeed);
        BackgroundLooper[] backgroundLoopers = FindObjectsOfType<BackgroundLooper>();
        foreach (var looper in backgroundLoopers)
        {
            looper.SetScrollSpeed(moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            currentHP--;
            UpdateHealthUI();
            StartCoroutine(FlashRed());

            if (currentHP <= 0)
            {
                FindObjectOfType<GameManager>()?.GameOver();
            }
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = originalColor;
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHP / maxHP;
        }
    }
}