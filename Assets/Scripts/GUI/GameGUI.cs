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
    private bool pointerAtFront;

    [Header("Front")]
    [SerializeField] private Image hpFill;
    [SerializeField] private TextMeshProUGUI textMult;
    [SerializeField] private TextMeshProUGUI textScore;

    void Awake()
    {
        instance = this;
        SetHealthBarFill(1, 1);
        SetScore(0);
        SetMult(1);
        SetFrontPointerActive(true);
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

    public void SetFrontPointerActive(bool value)
    {
        pointerAtFront = value;
        pointerBack.gameObject.SetActive(!pointerAtFront);
        pointerFront.gameObject.SetActive(pointerAtFront);
    }

    public void SetPointerPosition(Vector2 position)
    {
        pointerFront.anchorMin = position;
        pointerFront.anchorMax = position;
        pointerBack.anchorMin = position;
        pointerBack.anchorMax = position;
    }
}
