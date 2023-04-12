using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    bool waterReduccion;
    Player player;
    // [SerializeField] float speedReductionRatio = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        // originalSpeed = player.speed;
        waterReduccion = player.waterReduction;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // if(other.CompareTag("Player")){
        //     player.speed *= speedReductionRatio;
        // }
        if(other.CompareTag("Player")){
            player.waterReduction = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        // if(other.CompareTag("Player")){
        //     player.speed = originalSpeed;
        // }
        if(other.CompareTag("Player")){
            player.waterReduction = false;
        }
    }
    
}
