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

    [Header("Start Timer")]
    [SerializeField] private float waitTimeLength;
    private float startWaitTime;
    public bool waitingStart { get; private set; }

    private int score;
    private int health;
    private int currentMult;

    [Tooltip("Sound effects")]
    [SerializeField] private AudioSource Damaged_audiosource;
    [SerializeField] private AudioClip[] Damaged_Sounds;
    [Range(0.0f,3.0f)] public float volume= 1.0f;    
    [SerializeField] private AudioClip[] Death_Sounds;

    private void PlayDamagedRandomSound(){
        AudioClip Damaged_Sounds = GetRandomClip();
        Damaged_audiosource.PlayOneShot(Damaged_Sounds,volume);
    }

    
    private void PlayDeathRandomSound(){
        AudioClip Death_Sounds = GetRandomClip();
        Damaged_audiosource.PlayOneShot(Death_Sounds,volume);
    }

    private AudioClip GetRandomClip(){
        return Damaged_Sounds[UnityEngine.Random.Range(0,Damaged_Sounds.Length)];
        Damaged_audiosource.volume=Random.Range(0.02f,0.05f);
        Damaged_audiosource.pitch=Random.Range(0.9f,1.2f);
        
    }

    void Awake()
    {
        instance = this;
        health = maxHealth;
        currentMult = 0;
        score = 0;
    }

    void Start()
    {
        startWaitTime = Time.time;
        waitingStart = true;
    }

    void Update()
    {
        if (waitingStart && Time.time - startWaitTime >= waitTimeLength)
        {
            waitingStart = false;
        }
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
        PlayDamagedRandomSound();

        if (health == 0)
        {
            GameManager.instance.SetCurrentScore(score);
            PlayDeathRandomSound();
            GameManager.instance.GoToEndScene();
        }
    }
}
