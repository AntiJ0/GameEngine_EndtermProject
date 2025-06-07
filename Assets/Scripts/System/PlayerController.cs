using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("�̵� �ӵ� ����")]
    public float moveSpeed = 5f;           // ����/���/��ֹ��� �ӵ�
    public float maxSpeed = 20f;
    public float speedIncreaseRate = 0.01f;

    [Header("Y�� �̵�")]
    public float verticalMoveSpeed = 5f;   // ���� �Է� �ݿ� �ӵ� (������)

    [Header("ü�� ����")]
    public int maxHP = 3;
    private int currentHP;

    [Header("UI & �ð� ȿ��")]
    public Image healthBarFill;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("�ִϸ��̼�")]
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
        // ���Ʒ� �̵�
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = Vector2.up * vertical * verticalMoveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Y ��ġ ����
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -2.7f, 3.0f);
        transform.position = pos;

        // �̵� �ӵ� ���� (���, ��ֹ�, ������)
        moveSpeed += speedIncreaseRate * Time.deltaTime;
        moveSpeed = Mathf.Min(moveSpeed, maxSpeed);

        // �ӵ� ����
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