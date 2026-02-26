using System;
using UnityEngine;

public class BodyAnim : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private Animator animator;
    private PlayerMovement plMovement;
    Vector2 mousePos;

    private void Start()
    {
        plMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    

    private void FixedUpdate()
    {

        Vector2 lookDir = mousePos - plMovement.rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //Z position look, from Radians to Degrees
        plMovement.rb.rotation = angle;
    }   
}
