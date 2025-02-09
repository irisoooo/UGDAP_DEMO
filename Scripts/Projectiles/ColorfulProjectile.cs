using UnityEngine;
using Random = UnityEngine.Random;

public class ColorfulProjectile : UProjectile
{
    
    private void Awake() {
        spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }


    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
