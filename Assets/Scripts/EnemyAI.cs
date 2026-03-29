using System;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Linking")]
    public Transform target;
    private Seeker seeker;
    private Rigidbody2D rb;
    
    [Header("Movement")]
    public float speed = 20f;
    public float nextWaypointDistance = 2f;
    public float stoppingDistance = 2f;
        
    [Header("Detection")]
    public float detectionRadius = 10f;
    public LayerMask obstacleMask;

    //Pathfinding
    private Path path;
    private int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    
    [Header("Shooting")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float shootingRange = 5f; 
    private float fireRate;
    private float fireRateTimer = 0f;
    
    [Header("Weapon")]
    public float bulletForce = 20f;
    public string gunType;
    public string gunSubType;
    public bool isFiring;
    public int ammoCount;
    
    //Detection state
    private bool playerDetected = false;
    private Vector2 lastKnownPosition;
    private bool hasLastKnownPosition = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetection();
        HandleShooting();
    }
    void HandleShooting()
    {
        if (target == null || !playerDetected) return;

        float distanceToPlayer = Vector2.Distance(rb.position, target.position);
        fireRateTimer += Time.deltaTime;

        if (distanceToPlayer <= shootingRange && fireRateTimer >= fireRate && ammoCount > 0)
        {
            fireRateTimer = 0f;
            if (gunType == "shotgun")
            {
                fireRate = 2f; 
                ShootShotgun();
                ammoCount--;
            }
            else if (gunType == "pistol")
            {
                fireRate = 1f; 
                Shoot();
                ammoCount--;
            }
            else if (gunType == "ar")
            {
                fireRate = 0.5f; 
                Shoot();
                ammoCount--;
            }
        }
    }
    void Shoot()
    {
        isFiring = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = firePoint.up * bulletForce;
        isFiring = false;
    }

    void ShootShotgun()
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
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bullet.transform.up * bulletForce;
        }
        isFiring = false;
    }
    void CheckDetection()
    {
        if (target == null) return;

        float distanceToPlayer = Vector2.Distance(rb.position, target.position);

        if (distanceToPlayer <= detectionRadius)
        {
            Vector2 directionToPlayer = (target.position - transform.position).normalized;
            float distanceToCast = Vector2.Distance(transform.position, target.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToCast, obstacleMask);

            if (hit.collider == null)
            {
                playerDetected = true;
                lastKnownPosition = target.position; // update last known position while visible
                hasLastKnownPosition = true;
            }
            else
            {
                playerDetected = false;
            }
        }
        else
        {
            playerDetected = false;
        }
        //Debug.DrawRay(transform.position, (target.position - transform.position).normalized * detectionRadius, playerDetected ? Color.green : Color.red);
    }
    
    void FixedUpdate()
    {
        if (path == null) return;

        float distanceToPlayer = Vector2.Distance(rb.position, target.position);

        // Stop if close enough to player while detected
        if (playerDetected && distanceToPlayer <= stoppingDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Stop if reached last known position with no LOS
        if (!playerDetected && hasLastKnownPosition)
        {
            float distanceToLastKnown = Vector2.Distance(rb.position, lastKnownPosition);
            if (distanceToLastKnown <= stoppingDistance)
            {
                rb.linearVelocity = Vector2.zero;
                hasLastKnownPosition = false; // reached it, give up
                path = null;
                return;
            }
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        
        // Rotate sprite to face movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        
        

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    

    void UpdatePath()
    {
        if (!seeker.IsDone()) return;

        if (playerDetected)
        {
            // Path to actual player position while visible
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        else if (hasLastKnownPosition)
        {
            // Path to last known position if LOS is lost
            seeker.StartPath(rb.position, lastKnownPosition, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
