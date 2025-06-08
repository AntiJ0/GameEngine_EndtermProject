using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // 아이템 프리팹 3개
    public float minY = -2.5f;
    public float maxY = 2.5f;

    private float timer = 0f;

    private float baseInterval = 20f;
    private float playerSpeed = 5f;

    private void Start()
    {
        StartCoroutine(SpawnItemRoutine());
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        timer += Time.deltaTime;
    }

    public void SetPlayerSpeed(float speed)
    {
        playerSpeed = speed;
    }

    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            float interval = baseInterval * (5f / playerSpeed);
            yield return new WaitForSeconds(interval);

            if (itemPrefabs.Length == 0) continue;

            int index = Random.Range(0, itemPrefabs.Length);

            Vector3 spawnPos = transform.position;
            spawnPos.y = Random.Range(minY, maxY); 

            GameObject item = Instantiate(itemPrefabs[index], spawnPos, Quaternion.identity);
        }
    }
}