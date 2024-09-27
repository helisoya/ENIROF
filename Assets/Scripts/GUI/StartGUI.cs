using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGUI : MonoBehaviour
{
    [Header("Player Name")]
    [SerializeField] private TMP_InputField inputFieldName;


    public void Click_ConfirmName()
    {
        if (!string.IsNullOrEmpty(inputFieldName.text))
        {
            GameManager.instance.SetCurrentName(inputFieldName.text);
            GameManager.instance.SetCurrentScore(0);
            GameManager.instance.GoToMainScene();
        }
    }
}
