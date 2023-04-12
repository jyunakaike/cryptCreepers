using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for using async await
// using System.Threading.Tasks;
// using System;

public class Player : MonoBehaviour
{
    float h;
    float v;
    Vector3 moveDirection;

    [SerializeField] float speed = 3;
    float walk = 3;
    float run = 6;

    // water reduction
    public bool waterReduction;
    [SerializeField] float speedReductionRatio = 0.5f;

    float stamina = 1000;
    float minStamina = -500;
    float maxStamina = 1000;

    [SerializeField] Transform aim;
    [SerializeField] Camera cameraPlayer;

    Vector2 facingDirection;

    [SerializeField] Transform bulletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] bool powerShotEnabled = false;

    // player health
    [SerializeField] int health = 5;

    public int Health{
        get => health;
        set{
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        // stamina movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                // print("running");
                speed = run;
                stamina = Mathf.Floor(stamina -= 1 * Time.deltaTime);
            }
            else
            {
                speed = walk;
                if (stamina == 0)
                {
                    // print("set tired");
                    stamina = minStamina;
                }
                else
                {
                    // print("tired");
                    stamina += Mathf.Ceil(Mathf.Abs(1 * Time.deltaTime));
                }
            }
        }
        else
        {
            speed = walk;
            // print("walking");
            if (stamina < maxStamina)
            {
                // print("recovery stamina");
                stamina += Mathf.Ceil(Mathf.Abs(1 * Time.deltaTime));
            }
        }
        if (waterReduction)
        {
            speed *= speedReductionRatio;
        }
        // print(waterReduction);
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        moveDirection.x = h;
        moveDirection.y = v;
        // print(speed);
        transform.position += moveDirection * Time.deltaTime * speed;

        // Aim Movement
        facingDirection = cameraPlayer.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;

        // getMouseButton(0) leftClick 
        // getMouseButton(0) rightClick 

        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
            if (powerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powershot = true;
            }
            StartCoroutine(ReloadGun());
            // ReloadGun();
        }
    }
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }
    // usin async await 
    // private async void ReloadGun(){
    //     await Task.Delay(TimeSpan.FromSeconds(1/fireRate));
    //     gunLoaded= true;
    // }

    // player health

    // public void TakeDamage(int enemyAtackPower){
    //     health--;
    //     if(health <= 0 ){
    //         // GameOver
    //         gameObject.SetActive(false);
    //     }
    // }
    // from player point of view
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.CompareTag("Enemy")){
    //         PlayerHealth--;
    //         if(PlayerHealth <=0){
    //             gameObject.SetActive(false);
    //         }
    //     }
    // }
    // inmunity and constantDamage
    public void TakeDamage(int enemyAtackPower)
    {
        Health = Health - (1 * enemyAtackPower);
        if (Health <= 0)
        {
            // GameOver
            GameManager.Instance.GameOver = true;
            UIManager.Instance.showGameOverScreen();
            // gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            switch (other.GetComponent<PowerUp>().powerupType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    //incrementar cardencia de disparo
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    //activar el powershot
                    powerShotEnabled = true;
                    break;
            }
            Destroy(other.gameObject, 0.1f);
        }
    }
}
