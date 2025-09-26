using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize(); //syncs movements x + y

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //Z position look, from Radians to Degrees
        rb.rotation = angle;
    }   
} 
