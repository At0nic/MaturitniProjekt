using UnityEngine;

public class EnemyLegs : MonoBehaviour
{
    private EnemyAI enemyMovement;
    [SerializeField] private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMovement = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Leg animation
        if (enemyMovement.Directon != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}

