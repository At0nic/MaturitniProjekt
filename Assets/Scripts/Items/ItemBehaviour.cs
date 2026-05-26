using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [Header("Information")]
    public WeaponType itemType;
    public ItemSubType itemSubType;
    public int ammoCount;
    public float timeFromLastPickup;

    [Header("Conditions")]
    public bool isFlying = false;
    public bool onGround = true;

    private bool playerInRange = false;
    private Rigidbody2D rb;
    private Shooting shooting;

    private const float PICKUP_COOLDOWN = 0.5f;
    private const float LANDING_VELOCITY = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckLanding();
        CheckPickup();
    }

    private void CheckLanding()
    {
        if (isFlying && rb.linearVelocity.sqrMagnitude < LANDING_VELOCITY * LANDING_VELOCITY)
        {
            isFlying = false;
            onGround = true;
        }
    }

    private void CheckPickup()
    {
        if (!onGround || !playerInRange || isFlying || shooting == null) return;
        if (!Input.GetButtonDown("Fire2")) return;
        if (Time.time - timeFromLastPickup < PICKUP_COOLDOWN) return;

        TransferData();
        Destroy(gameObject);
    }

    private void TransferData()
    {
        shooting.currentWeapon = itemType;
        shooting.ammoCount = ammoCount;
        shooting.timeFromLastPickup = Time.time;
        shooting.itemSubType = itemSubType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || isFlying) return;

        playerInRange = true;

        if (other.TryGetComponent<Shooting>(out Shooting s))
            shooting = s;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}