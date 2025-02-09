using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    
    public EventHandler OnPlayerEnterRoom;
    public EventHandler OnPlayerExitRoom;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out UPlayerController player)) {
            OnPlayerEnterRoom?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent(out UPlayerController player)) {
            OnPlayerExitRoom?.Invoke(this, EventArgs.Empty);
        }      
    }
}