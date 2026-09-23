using UnityEngine;
using System.Collections;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectilePrefab;
    [SerializeField] private GameObject splatterPrefab;

    private void Start() {
        GameObject grapeShadow = Instantiate(grapeProjectilePrefab, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGameShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition) {
        float timePassed = 0f;

        while(timePassed < duration) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector3.Lerp(startPosition, endPosition, linearT) + new Vector3(0f, height);

            yield return null;
        }

        Instantiate(splatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator MoveGameShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition) {
        float timePassed = 0f;

        while(timePassed < duration) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            grapeShadow.transform.position = Vector3.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

        Destroy(grapeShadow);
    }
}
