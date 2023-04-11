using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5; 

    private void Start() {
        Destroy(gameObject , 5);
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    // trigger
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(); 
            Destroy(gameObject);
        }

    }
    
}
