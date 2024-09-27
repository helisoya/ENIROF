using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public static GameGUI instance;

    [Header("Pointer")]
    [SerializeField] private RectTransform pointerBack;
    [SerializeField] private RectTransform pointerFront;
    private Image pointerBackColor;
    private Image pointerFrontColor;

    [Header("Front")]
    [SerializeField] private Image hpFill;
    [SerializeField] private TextMeshProUGUI textMult;
    [SerializeField] private TextMeshProUGUI textScore;



    [SerializeField] private GameObject Front_hidden; 
    [SerializeField] private GameObject Back_hidden; 

    [Tooltip("Sound effects")]
    [SerializeField] private AudioSource switch_audiosource;
    [SerializeField] private AudioClip[] switch_sounds;
    [Range(0.0f,3.0f)] public float volume= 1.0f;    
    private bool hasGameStarted = false;


    private void PlaySwitchRandomSound(){
        AudioClip switch_sounds = GetRandomClip();
        switch_audiosource.PlayOneShot(switch_sounds,volume);
    }

    private AudioClip GetRandomClip(){
        switch_audiosource.volume=Random.Range(0.02f,0.05f);
        switch_audiosource.pitch=Random.Range(0.9f,1.2f);
        return switch_sounds[UnityEngine.Random.Range(0,switch_sounds.Length)];
        
    }

    void Awake()
    {
        instance = this;

        pointerBackColor = pointerBack.GetComponent<Image>();
        pointerFrontColor = pointerFront.GetComponent<Image>();

        SetHealthBarFill(1, 1);
        SetScore(0);
        SetMult(1);
        SetFrontPointerActive(true);
        SetPointerColor(Color.green);
        hasGameStarted = true;
    }

    public void SetHealthBarFill(float currentHP, float maxHP)
    {
        hpFill.fillAmount = currentHP / maxHP;
    }

    public void SetScore(int score)
    {
        textScore.text = score.ToString();
    }

    public void SetMult(int mult)
    {
        textMult.text = mult.ToString();
    }

    public void SetFrontPointerActive(bool pointerAtFront)
    {
        pointerBack.gameObject.SetActive(!pointerAtFront);
        pointerFront.gameObject.SetActive(pointerAtFront);
        if (hasGameStarted)
        {
            PlaySwitchRandomSound();
        }
        
        Front_hidden.SetActive(pointerAtFront);   
        Back_hidden.SetActive(!pointerAtFront); 



    }

    public void SetPointerPosition(Vector2 position)
    {
        pointerFront.anchorMin = position;
        pointerFront.anchorMax = position;
        pointerBack.anchorMin = position;
        pointerBack.anchorMax = position;
    }

    public void SetPointerColor(Color color)
    {
        pointerFrontColor.color = color;
        pointerBackColor.color = color;
    }
}
