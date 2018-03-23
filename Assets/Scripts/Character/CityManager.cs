using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour {

    public static CityManager instance;

    public float health;
    public float maxHealth;
    public Image healthBar;

    void Awake() {
        instance = this;
    }

    void Start () {
        health = maxHealth;
	}
	
	void Update () {
        Damage(Time.deltaTime * 0.5f);
	}

    public void Damage(float amount) {
        health -= amount;

        healthBar.fillAmount = health / maxHealth;
    }

    public void Fill() {
        health = 100;

        healthBar.fillAmount = health / maxHealth;
    }
}
