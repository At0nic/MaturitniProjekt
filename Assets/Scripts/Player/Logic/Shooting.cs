using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Unity
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    //Info
    public string currentItem;      // AR/SMG, Pistol, Shotgun
    public bool isFiring;
    public bool pickingUp;
    public float bulletForce = 20f;
    public ItemBehaviour itemBehaviour;
    
    //Shooting Intervals
    private float timeOfLastShot;
    private double timeBetweenShots;
    
    void Start()
    {
        itemBehaviour = GetComponent<ItemBehaviour>();
    }
    
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Fire2") && currentItem != "" && pickingUp == false)
        {
            Debug.Log("Item thrown!");
            currentItem = "";
        }
        else if (Input.GetButtonDown("Fire2") && currentItem == "")
        {
            Debug.Log("Item not in hand!");
        }*/

        if (Input.GetButtonDown("Fire2") && Time.time - itemBehaviour.timeFromLastPickup > 0.05)
        {
            if (currentItem != "" && pickingUp == false)
            {
                Debug.Log("Item thrown!");
                currentItem = "";
            }
            else if (currentItem == "")
            {
                Debug.Log("Item not in hand!");
            }
        }
        
        if (currentItem == "shotgun")
        {
            timeBetweenShots = 1;
            if (Input.GetButtonDown("Fire1") && Time.time - timeOfLastShot > timeBetweenShots &&  itemBehaviour.ammoCount != 0)
            {
                ShootShotgun();
                timeOfLastShot=Time.time;
                itemBehaviour.ammoCount--;
                isFiring = false;
            }
        }
        else if (currentItem == "pistol")
        {
            timeBetweenShots = 0.5;
            if (Input.GetButtonDown("Fire1") && Time.time - timeOfLastShot > timeBetweenShots &&  itemBehaviour.ammoCount > 0)
            {
                Shoot();
                timeOfLastShot=Time.time;
                itemBehaviour.ammoCount--;
                isFiring = false;
            }
        }
        else if (currentItem == "ar")
        {
            timeBetweenShots = 0.1;
            if (Input.GetButton("Fire1") && Time.time - timeOfLastShot > timeBetweenShots &&  itemBehaviour.ammoCount > 0)
            {
                Shoot();
                timeOfLastShot=Time.time;
                itemBehaviour.ammoCount--;
                isFiring = false;
            }
        }
        
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

            Quaternion spreadRotation =
                firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);

            GameObject bullet =
                Instantiate(bulletPrefab, firePoint.position, spreadRotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bullet.transform.up * bulletForce;
        }
        
    }
}