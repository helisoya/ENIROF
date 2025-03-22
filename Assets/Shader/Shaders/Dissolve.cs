using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Material material;
    private float fade;
    private bool isAppearing;
    public float transitionSpeed = 2f; // Plus la valeur est grande, plus la transition est rapide

    void Start()
    {
        material = new Material(GetComponent<SpriteRenderer>().material); // Clone le matériau
        GetComponent<SpriteRenderer>().material = material; // Applique le clone à cet objet

        fade = material.GetFloat("_Fade");
    }

    public void ToggleDissolve(bool appear)
    {
        isAppearing = appear;
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        float targetFade = isAppearing ? 1f : 0f;
        while (Mathf.Abs(fade - targetFade) > 0.01f)
        {
            fade = Mathf.MoveTowards(fade, targetFade, Time.deltaTime * transitionSpeed);
            material.SetFloat("_Fade", fade);
            yield return null;
        }
        material.SetFloat("_Fade", targetFade);
    }
}
