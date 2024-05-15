using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform player;
    //odleg�o�� od ko�ca poziomu
    public float levelExitDistance = 100;
    //punkt ko�ca poziomu
    public Vector3 exitPosition;
    public GameObject exitPrefab;
    //zmienna - flaga - oznaczaj�ca uko�czenie poziomu
    public bool levelComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        //znajdz gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //wylosuj pozycje na kole o srednicy 100 jednostek
        Vector2 spawnCircle = Random.insideUnitCircle;
        //chcemy tylko pozycje na okregu, a nie wewnatrz ko�a
        spawnCircle = spawnCircle.normalized;
        spawnCircle *= levelExitDistance;
        //konwertujemy do Vector3
        //podstawiamy x=x, y=0, z=y
        exitPosition = new Vector3(spawnCircle.x, 0, spawnCircle.y);
        Instantiate(exitPrefab, exitPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
