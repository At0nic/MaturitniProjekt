using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.5f;

    public Rigidbody2D rb;
    public Camera cam;

    public Vector2 playerMovement;
    Vector2 mousePos;
    

    void Update()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        playerMovement.Normalize(); //syncs movement x + y

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerMovement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //Z position look, from Radians to Degrees
        rb.rotation = angle;
    }   
} 

