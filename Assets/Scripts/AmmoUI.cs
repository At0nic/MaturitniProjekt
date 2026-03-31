using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Shooting shooting; 
    public TMP_Text ammoText;
    
    void Update()
    {
        ammoText.text = shooting.ammoCount.ToString();
        if (shooting.currentItem == "" || shooting.currentItem == "melee")
        {
            ammoText.enabled = false;
        }
        else
        {
                ammoText.enabled = true;
        }
       
    }
}
