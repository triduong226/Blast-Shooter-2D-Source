using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public static HealthBar instance;
    public Slider slider;
    public Image oldImage;
    public Sprite newImage;

    void Awake(){
        MakeInstace();
    }
    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }
    void Start(){
        setMaxHealth(maxHealth);
        currentHealth = maxHealth;
        if(GamePlayController.instance != null){
            GamePlayController.instance._SetScore(Plane.score);
        }
    }
    void Update(){
        setHealth(currentHealth);
        if(slider.value == 0){
            oldImage.sprite = newImage;
        }
    }
    public void setMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(int health){
        slider.value =  health;
    }
    public void takeDamage(int damage){
        currentHealth -= damage;
    }

}
