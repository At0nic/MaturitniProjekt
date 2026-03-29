using Unity.VisualScripting;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [Header("Information")]
    public string itemType;
    public string itemSubType;
    public int ammoCount;
    public float timeFromLastPickup;
    private Rigidbody2D rb;
    
    [Header("Conditions")]
    public bool isFlying = false;
    public bool onGround = true;
    private bool playerInRange = false;
    public bool playerPickedUp = false;
    
    //Script Reference
    private Shooting shooting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //Item after flying check
        if (isFlying && rb.linearVelocity.magnitude < 0.5f)
        {
            isFlying = false;
            onGround = true;
        }
        
        //Item Pickup
        if (onGround && playerInRange && Input.GetButtonDown("Fire2") && !isFlying && Time.time - timeFromLastPickup > 0.5)
        {
            if (shooting == null) return; 
            timeFromLastPickup = Time.time;
            
            //Data transfer
            shooting.currentItem = itemType;
            shooting.ammoCount = ammoCount;
            shooting.timeFromLastPickup = timeFromLastPickup;
            shooting.itemSubType = itemSubType;
            
            onGround = false;
            Destroy(gameObject);
        }
    }
    
    //System for Player In Range Detection with Item's Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFlying)
            playerInRange = true;
        shooting = other.GetComponent<Shooting>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
