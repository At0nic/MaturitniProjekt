using UnityEngine;

public class legRotation : MonoBehaviour
{
    public float rotationSpeed;
    private PlayerMovement plMovement;
    [SerializeField] private Animator animator;

    void Start()
    {
        plMovement = GetComponentInParent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = plMovement.playerMovement;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRoation = Quaternion.Euler(0, 0, angle);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRoation, rotationSpeed * Time.deltaTime);
        
        if (direction != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
