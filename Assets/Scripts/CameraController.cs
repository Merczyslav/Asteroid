﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //współrzędne gracza
    Transform player;
    //wysokość kamery
    public float cameraHeight = 10f;
    //prędkość kamery - do użytku dla smoothdamp
    Vector3 cameraSpeed;
    //szybkość wygładzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //podłącz pozycję gracza do lokalnej zmiennej korzystając z jego taga
        //to nie jest zapisanie wartości jeden raz tylko referencja do obiektu
        //to znaczy, że player zawszę bedzie zawierał aktualną pozycje gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //oblicz docelową pozycje kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;

        //płynnie przesuń kamerę w kierunku gracza
        //funkcja Vector3.Lerp
        //płynnie przechodzi z pozycji pierwszego argumentu do drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //smootdamp działa jak sprężyna starająca się dociągnąć kamerę do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}
