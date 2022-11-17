using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
    public GameObject healthBar;
    public Slider healthBarSlider;


    public void SetHealthBarActive()
    {
        healthBar.SetActive(true);
    }

    public void SetHealthBarInActive()
    {
        healthBar.SetActive(false);
    }

    public void SetHealthBarValue(float value)
    {
        healthBarSlider.value = value;
    }
}