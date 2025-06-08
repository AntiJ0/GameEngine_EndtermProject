using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int baseDamage = 20;
    public Vector3 baseScale = Vector3.one;

    private bool initialized = false;

    void Awake()
    {
        if (!initialized)
        {
            initialized = true;

            // 초기 스케일 저장
            baseScale = transform.localScale;

            // 배율 랜덤 설정
            float scaleMultiplier = Random.Range(0.8f, 2.0f);

            // 조정
            transform.localScale = baseScale * scaleMultiplier;
            baseDamage = Mathf.RoundToInt(20 * scaleMultiplier);
        }
    }

    public int GetDamage()
    {
        return baseDamage;
    }
}