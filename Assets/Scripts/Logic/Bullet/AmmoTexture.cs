using UnityEngine;
using UnityEngine.UI;

public class AmmoTexture : MonoBehaviour
{
    [SerializeField] private Shooting shooting;
    [SerializeField] private Sprite[] sprites;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        switch (shooting.currentWeapon)
        {
            case WeaponType.Shotgun:
                image.sprite = sprites[0];
                image.enabled = true;
                break;

            case WeaponType.Pistol:
                image.sprite = sprites[1];
                image.enabled = true;
                break;

            default:
                image.enabled = false;
                break;
        }
    }
}