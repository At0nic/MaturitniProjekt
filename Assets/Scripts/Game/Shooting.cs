using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public string GunType;      // AR/SMG, Pistol, Shotgun
    public bool isFiring;
    
    //Shooting Intervals
    private float timeOfLastShot;
    private double timeBetweenShots;
    
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (GunType == "shotgun")
        {
            timeBetweenShots = 1;
            if (Input.GetButtonDown("Fire1") && Time.time - timeOfLastShot > timeBetweenShots)
            {
                ShootShotgun();
                timeOfLastShot=Time.time;
                isFiring = false;
            }
        }
        else if (GunType == "pistol")
        {
            timeBetweenShots = 0.5;
            if (Input.GetButtonDown("Fire1") && Time.time - timeOfLastShot > timeBetweenShots)
            {
                Shoot();
                timeOfLastShot=Time.time;
                isFiring = false;
            }
        }
        else if (GunType == "ar")
        {
            timeBetweenShots = 0.1;
            if (Input.GetButton("Fire1") && Time.time - timeOfLastShot > timeBetweenShots)
            {
                Shoot();
                timeOfLastShot=Time.time;
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