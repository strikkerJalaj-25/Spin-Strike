using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void ReduceHealth(int amount)
    {
        currentHealth -= amount;
        UpdateUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("GAME OVER");
        // open game over panel here if you want
    }
}
