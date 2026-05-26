using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Shooting shooting;
    [SerializeField] private TMP_Text ammoText;

    void Awake()
    {
        ammoText.enabled = false;
    }

    void Update()
    {
        bool hasRangedWeapon = shooting.currentWeapon != WeaponType.None
                               && shooting.currentWeapon != WeaponType.Melee;

        ammoText.enabled = hasRangedWeapon;

        if (hasRangedWeapon)
            ammoText.text = shooting.ammoCount.ToString();
    }
}