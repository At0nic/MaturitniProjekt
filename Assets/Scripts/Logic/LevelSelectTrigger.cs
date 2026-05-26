using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(1); // 1 = LevelSelect scene index
        }
    }
}