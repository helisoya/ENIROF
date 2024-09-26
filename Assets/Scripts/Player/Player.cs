using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Infos")]
    [SerializeField] private int maxMult;
    [SerializeField] private int maxHealth;
    [SerializeField] private Transform body;
    [SerializeField] private Animator animator;
    private int score;
    private int health;
    private int currentMult;

    void Awake()
    {
        instance = this;
        health = maxHealth;
        currentMult = 0;
        score = 0;
    }

    public float GetBodyZ()
    {
        return body.position.z;
    }

    public void AddScore(int add, bool useMult = true)
    {
        score += add * (useMult ? currentMult : 1);
        GameGUI.instance.SetScore(score);
    }

    public void IncrementMult()
    {
        currentMult = Mathf.Clamp(currentMult + 1, 0, maxMult);
        GameGUI.instance.SetMult(currentMult);
    }

    public void ResetMult()
    {
        currentMult = 1;
        GameGUI.instance.SetMult(currentMult);
    }

    public void TakeDamage()
    {
        health = Mathf.Clamp(health - 1, 0, maxHealth);
        ResetMult();
        ScrollerManager.instance.SetCurrentSpeed(0.5f);
        GameGUI.instance.SetHealthBarFill(health, maxHealth);
        animator.SetTrigger("Damage");

        if (health == 0)
        {
            GameManager.instance.SetCurrentScore(score);
            GameManager.instance.GoToEndScene();
        }
    }
}
