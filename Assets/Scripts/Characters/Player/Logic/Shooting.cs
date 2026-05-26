using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject[] itemPrefabs;
    public BoxCollider2D attackCollider;

    [Header("Weapon Info")]
    public WeaponType currentWeapon = WeaponType.None;
    public ItemSubType itemSubType = ItemSubType.None;
    public int ammoCount;
    public float timeFromLastPickup;
    public bool isFiring;

    [Header("Stats")]
    [SerializeField] private float bulletForce = 20f;

    private float timeOfLastShot;
    private float timeBetweenShots;

    void Awake()
    {
        attackCollider.enabled = false;
    }

    void Update()
    {
        HandleThrow();
        HandleFire();
    }

    private void HandleThrow()
    {
        if (Input.GetButtonDown("Fire2")
            && Time.time - timeFromLastPickup > 0.05f
            && currentWeapon != WeaponType.None)
        {
            Throw();
            currentWeapon = WeaponType.None;
            itemSubType = ItemSubType.None;
        }
    }

    private void HandleFire()
    {
        switch (currentWeapon)
        {
            case WeaponType.Shotgun:
                timeBetweenShots = 1f;
                if (Input.GetButtonDown("Fire1")
                    && Time.time - timeOfLastShot > timeBetweenShots
                    && ammoCount > 0)
                {
                    ShootShotgun();
                    timeOfLastShot = Time.time;
                    ammoCount--;
                }
                break;

            case WeaponType.Pistol:
                timeBetweenShots = 0.5f;
                if (Input.GetButtonDown("Fire1")
                    && Time.time - timeOfLastShot > timeBetweenShots
                    && ammoCount > 0)
                {
                    Shoot();
                    timeOfLastShot = Time.time;
                    ammoCount--;
                }
                break;

            case WeaponType.AR:
                timeBetweenShots = 0.1f;
                if (Input.GetButton("Fire1")
                    && Time.time - timeOfLastShot > timeBetweenShots
                    && ammoCount > 0)
                {
                    Shoot();
                    timeOfLastShot = Time.time;
                    ammoCount--;
                }
                break;

            case WeaponType.Melee:
            case WeaponType.None:
                if (Input.GetButtonDown("Fire1"))
                {
                    isFiring = true;
                    attackCollider.enabled = true;
                    Invoke(nameof(DisableAttack), 0.2f);
                }
                break;
        }
    }

    private void DisableAttack()
    {
        attackCollider.enabled = false;
    }

    private void Throw()
    {
        GameObject prefabToThrow = System.Array.Find(itemPrefabs, p => p.name == itemSubType.ToString());
        GameObject thrownWeapon = Instantiate(prefabToThrow, firePoint.position, firePoint.rotation);
        Rigidbody2D thrownRb = thrownWeapon.GetComponent<Rigidbody2D>();

        thrownRb.linearVelocity = firePoint.up * 12f;
        thrownRb.angularVelocity = 500f;

        if (thrownWeapon.TryGetComponent<ItemBehaviour>(out ItemBehaviour ib))
        {
            ib.ammoCount = ammoCount;
            ib.isFlying = true;
            ib.onGround = false;
        }
    }

    private void Shoot()
    {
        isFiring = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.up * bulletForce;

        if (bullet.TryGetComponent<Bullet>(out Bullet bulletScript))
            bulletScript.ownerTag = gameObject.tag;
    }

    private void ShootShotgun()
    {
        isFiring = true;
        int pelletCount = 3;
        float totalSpreadAngle = 7.5f;
        float angleStep = totalSpreadAngle / (pelletCount - 1);
        float startAngle = -totalSpreadAngle / 2;

        for (int i = 0; i < pelletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, spreadRotation);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = bullet.transform.up * bulletForce;

            if (bullet.TryGetComponent<Bullet>(out Bullet bulletScript))
                bulletScript.ownerTag = gameObject.tag;
        }
    }
}