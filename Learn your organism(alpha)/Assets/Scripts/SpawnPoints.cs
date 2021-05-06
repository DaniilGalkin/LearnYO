﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public GameObject pointsPrefab;
    public float timeBetweenSpawn = 1;
    public int amount = 100;
    private float fieldWidth;
    private float fieldHeight;
    public int range;

    public GameObject field;

    void Start()
    {
        StartCoroutine(SpawnPoint());
        fieldWidth = field.transform.localScale.x;
        fieldHeight = field.transform.localScale.y;
    }

    private IEnumerator SpawnPoint()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int countSpawn = 0;
        while (countSpawn < amount)
        {
            var points = GameObject.FindGameObjectsWithTag("Point");
            countSpawn++;
            float xPos = Mathf.Clamp(Random.Range(player.transform.position.x - range, player.transform.position.x + range), fieldWidth / 2 * -1, fieldWidth / 2);
            float yPos = Mathf.Clamp(Random.Range(player.transform.position.y - range, player.transform.position.y + range), fieldHeight / 2 * -1, fieldHeight / 2);
            Instantiate(pointsPrefab, new Vector3(xPos, yPos, -1), Quaternion.identity);
            foreach (var p in points)
            {
                var distance = (p.transform.position - player.transform.position).magnitude;
                if (distance > 20)
                {
                    Destroy(p);
                    countSpawn--;
                }
            }
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
