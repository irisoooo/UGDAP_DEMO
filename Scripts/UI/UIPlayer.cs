using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour {

    [SerializeField] private UPlayerController player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float colorTransformSpeed = 10f;
    
    private bool bHurt;
    private bool bCured;
    private Color color;
    
    
    // Start is called before the first frame update
    void Start() {
        color = spriteRenderer.color;
        player.OnHurt += OnHurt;    
    }

    private void OnHurt(object sender, EventArgs e) {
        Debug.Log("Hurt");
    }

    // Update is called once per frame
    void Update() {

        color.a = Mathf.Lerp(color.a, player.GetHealthValRate(), colorTransformSpeed * Time.deltaTime);
        spriteRenderer.color = color;
        
    }
}
