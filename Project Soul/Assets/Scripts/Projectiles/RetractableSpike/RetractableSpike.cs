using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpike : MonoBehaviour
{
    public float targetHeight = 7f; 
    public float scaleSpeed = 2f;
    public float timeBeforeRetraction = 0.5f;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;

        StartCoroutine(ScaleRoutine());
    }

    IEnumerator ScaleRoutine()
    {
        
        Vector3 targetScale = new Vector3(originalScale.x, targetHeight, originalScale.z);
        Vector3 targetPosition = originalPosition + new Vector3(0, (targetHeight - originalScale.y) / 2.0f, 0);


        yield return StartCoroutine(ScaleTo(targetScale, targetPosition));


        yield return new WaitForSeconds(timeBeforeRetraction);


        yield return StartCoroutine(ScaleTo(originalScale, originalPosition));

        Destroy(gameObject);
    }

    IEnumerator ScaleTo(Vector3 targetScale, Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * scaleSpeed);

            yield return null;
        }

        transform.localScale = targetScale;
        transform.position = targetPosition;
    }

}
