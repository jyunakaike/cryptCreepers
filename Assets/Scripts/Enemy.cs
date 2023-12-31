using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] int health = 1;
    [SerializeField] float speed = 1;
    [SerializeField] int enemyAtackPower = 1;
    [SerializeField] bool playerInmune = false;

    [SerializeField] int scorePoints = 100;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            GameManager.Instance.Score += scorePoints;
            Destroy(gameObject);
        }
    }

    // enemy damage from enemy point of view 
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.CompareTag("Player") && !playerInmune){
    //         other.GetComponent<Player>().TakeDamage(enemyAtackPower);
    //         playerInmune = true; 
    //         Invoke("PlayerInmuinity", 3f);
    //     }
    // }

    // enemy damage when stay 
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player") && !playerInmune){
            other.GetComponent<Player>().TakeDamage(enemyAtackPower);
            playerInmune = true; 
            Invoke("PlayerInmuinity", 3f);
        }
    }

    void PlayerInmuinity()
    {
        playerInmune = false;
    }

}
