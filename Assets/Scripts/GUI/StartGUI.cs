using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGUI : MonoBehaviour
{
    [Header("Player Name")]
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private GameObject GUIRoot;
    [SerializeField] private float cutsceneLength;
    [SerializeField] private Animator custceneAnimator;

    private bool skip;

    public void Click_ConfirmName()
    {
        if (!string.IsNullOrEmpty(inputFieldName.text))
        {
            GameManager.instance.SetCurrentName(inputFieldName.text);
            GameManager.instance.SetCurrentScore(0);
            GUIRoot.SetActive(false);
            StartCoroutine(Routine_Text());
        }
    }

    void OnMove(InputValue inputValue)
    {
        skip = true;
    }

    IEnumerator Routine_Text()
    {
        skip = false;
        custceneAnimator.SetTrigger("Cutscene");

        float currentWaitTime = 0;

        while (!skip && currentWaitTime < cutsceneLength)
        {
            currentWaitTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.GoToMainScene();

    }
}
