using UnityEngine;

public class Wall : MonoBehaviour {

    [SerializeField] private SpriteRenderer sprite;
    private bool bColorResponse = false;
    [SerializeField] private float colorResponseSpeed = 10f;
    [SerializeField] private float fadeColorDeltaTime = .8f;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out UProjectile project)) {
            bColorResponse = true;
            Destroy(project.gameObject);
        }
    }

    private void Update() {
        if (bColorResponse) {
            sprite.color = Color.Lerp(sprite.color, Color.white, colorResponseSpeed * Time.deltaTime);
            fadeColorDeltaTime = .8f;
        }

        bColorResponse = false;
        fadeColorDeltaTime -= Time.deltaTime;
        if (fadeColorDeltaTime <= 0f) {
            sprite.color = Color.Lerp(sprite.color, Color.black, colorResponseSpeed * Time.deltaTime);
        }
    }
}
