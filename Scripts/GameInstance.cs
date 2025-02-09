using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour {

    [SerializeField] private new GameObject light;
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private MapGenerator MapGenerator;

    private static GameInstance Singleton;
    private UPlayerController player;
    
    private void Start() {

        if (Singleton == null) {
            Singleton = this;
        }
        
        
        
        MapGenerator.OnGenerateMap += OnGenerateMap;
        
    }

    private void OnGenerateMap(object sender, EventArgs e) {
        var startPosition = MapGenerator.startRoom.transform.position;
        
        light.transform.position = startPosition;
        camera.transform.position = new Vector3(startPosition.x, startPosition.y, -10);
        
        player = Instantiate(playerPrefab, startPosition, Quaternion.identity).GetComponent<UPlayerController>();
        player.SetUpRooms(MapGenerator.rooms);
        player.SetUpCamera(camera);
    }

    public GameObject GetLight() {
        return light;
    }
    
    public static GameInstance GetSingleton() {
        return Singleton;
    }
    
}
