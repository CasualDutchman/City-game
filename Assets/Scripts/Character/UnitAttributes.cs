using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAttributes : MonoBehaviour {

    [Header("Wolf Attributes")]
    public float health;
    public float maxhealth;
    public float attackDamage;
    public float attackSpeed;
    public int level;
    public int experience;
    public int maxExperience;

    [Header("UI")]
    public Image healthbar;
    public Image foodbar;
    public Text levelText;
    public Image experienceBar;

    [Header("Misc")]

    float foodTimer;

    public bool atTowerPlace;

	void Start () {
        health = maxhealth;

        UpdateTexts();
        UpdateExperienceTexts();
    }
	
	void Update () {
        
	}

    public void UpdateTexts() {
        if (healthbar != null) {
            healthbar.fillAmount = health / maxhealth;
            //healthText.text = health + "/" + maxhealth;
            //foodText.text = food.ToString("F1") + "/" + maxFood;
        }
    }

    public void UpdateExperienceTexts() {
        if (levelText != null) {
            //levelText.text = "Level: " + level.ToString();
            //experienceBar.fillAmount = (float)experience / (float)maxExperience;
            //xpText.text = experience.ToString();
        }
    }

    public void Damage(float amount) {
        health -= amount;

        UpdateTexts();
    }

    public void ChangeFood(float amount) {

        UpdateTexts();
    }

    public void AddXP(int amount) {
        experience += amount;

        if (experience >= maxExperience) {
            level++;
            experience -= maxExperience;
        }

        UpdateExperienceTexts();
    }
}
