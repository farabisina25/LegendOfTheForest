using UnityEngine;
using System.Collections;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine() {
        float elapsedTime = 0f;
        float startvalue = spriteRenderer.color.a;

        while(elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startvalue, 0f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
