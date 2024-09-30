using UnityEngine;
using System.Collections;

public class SectionActivatorWithFade : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public ScreenFader screenFader;
    private bool sectionActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sectionActive)
        {
            StartCoroutine(ActivateSectionWithFade());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && sectionActive)
        {
            StartCoroutine(DeactivateSectionWithFade());
        }
    }

    private IEnumerator ActivateSectionWithFade()
    {
        sectionActive = true;
        screenFader.FadeOut();
        yield return new WaitForSeconds(screenFader.fadeDuration);
        ActivateSection();
        screenFader.FadeIn();
    }

    private IEnumerator DeactivateSectionWithFade()
    {
        screenFader.FadeOut();
        yield return new WaitForSeconds(screenFader.fadeDuration);
        DeactivateSection();
        screenFader.FadeIn();
        sectionActive = false;
    }

    private void ActivateSection()
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    private void DeactivateSection()
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
    }
}
