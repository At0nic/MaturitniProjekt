using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Unity
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject heldItemPrefab;
    public float timeFromLastPickup;
    public int ammoCount;
    public GameObject[] itemPrefabs;
    
    //Info
    public string currentItem;      // AR/SMG, Pistol, Shotgun
    public string itemSubType;      // for animations and prefab matching
    public bool isFiring;
    public bool pickingUp;
    public float bulletForce = 20f;
    public BoxCollider2D attackCollider;
    
    //Shooting Intervals
    private float timeOfLastShot;
    private double timeBetweenShots;

    
    void Start()
    {
        attackCollider.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Add if item in hand and player picks up another weapon, the previous weapon is tossed
        if (Input.GetButtonDown("Fire2") && Time.time - timeFromLastPickup > 0.05)
        {
            if (currentItem != "" && pickingUp == false)
            {
                Throw();
                currentItem = "";
                itemSubType = "";

            }
            else if (currentItem == "")
            {
                Debug.Log("Item not in hand!");
            }
        }
        
        if (currentItem == "shotgun")
        {
            timeBetweenShots = 1;
            if (Input.GetButtonDown("Fire1") && Time.time - timeOfLastShot > timeBetweenShots &&  ammoCount != 0)
            {
                ShootShotgun();
                timeOfLastShot=Time.time;
                ammoCount--;
                isFiring = false;
            }
        }
        else if (currentItem == "pistol")
        {
            timeBetweenShots = 0.5;
            if (Input.GetButtonDown("Fire1") && Time.time - timeOfLastShot > timeBetweenShots &&  ammoCount != 0)
            {
                Shoot();
                timeOfLastShot=Time.time;
                ammoCount--;
                isFiring = false;
            }
        }
        else if (currentItem == "ar")
        {
            timeBetweenShots = 0.1;
            if (Input.GetButton("Fire1") && Time.time - timeOfLastShot > timeBetweenShots &&  ammoCount != 0)
            {
                Shoot();
                timeOfLastShot=Time.time;
                ammoCount--;
                isFiring = false;
            }
        }
        else if (currentItem == "melee" ||  currentItem == "")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isFiring = true;
                attackCollider.enabled = true;
                Debug.Log(isFiring);
                Invoke(nameof(DisableAttack), 0.2f);
                Debug.Log("attack executed");
            }
        }
        
    }

    void DisableAttack() // Disabling attack detection
    {
        attackCollider.enabled = false;
    }

    void Throw()
    {
        //Prefab checking + matching
        GameObject prefabToThrow = System.Array.Find(itemPrefabs, p => p.name == itemSubType);
        GameObject thrownWeapon = Instantiate(prefabToThrow, firePoint.position, firePoint.rotation);
        Rigidbody2D thrownRb = thrownWeapon.GetComponent<Rigidbody2D>();
        
        //Setting up physics
        thrownRb.linearVelocity = firePoint.up * 12f;
        thrownWeapon.GetComponent<Rigidbody2D>().angularVelocity = 500f;
        ItemBehaviour ib = thrownWeapon.GetComponent<ItemBehaviour>();
        
        //Info Transfer
        ib.ammoCount = ammoCount;
        ib.isFlying = true;
        ib.onGround = false;
    }
    void Shoot()
    {
        isFiring = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        
        bulletRb.linearVelocity = firePoint.up * bulletForce;
    }
    void ShootShotgun()
    {
        isFiring = true;
        int pelletCount = 3;                // Number of pellets
        float totalSpreadAngle = 7.5f;      // Total spread angle
        
        float angleStep = totalSpreadAngle / (pelletCount - 1);
        float startAngle = -totalSpreadAngle / 2;
        
        for (int i = 0; i < pelletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);

            Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, spreadRotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bullet.transform.up * bulletForce;
        }
    }
}