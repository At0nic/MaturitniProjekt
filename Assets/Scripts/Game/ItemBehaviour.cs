using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public string ItemType;
    public bool isFlying = false;
    public bool onGround = true;
    private bool playerInRange = false;
    public bool playerPickedUp = false;
    
    // Update is called once per frame
    void Update()
    {
        /*
        //throw
        if (onGround == false && isFlying == false && Input.GetButtonDown("Fire2") && shooting.isFiring == false)
        {
            //Throw {might delete and put into player Logic}
        }
*/
        //Item Pickup
        if (onGround && playerInRange && Input.GetButtonDown("Fire2"))
        {
            onGround = false;
            Debug.Log("Item Picked Up!");

            Destroy(gameObject); //Destroys GO and stores data in other GO
        }
    }
    
    //System for Player In Range Detection with Item's Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
