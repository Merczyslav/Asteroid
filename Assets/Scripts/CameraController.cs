using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //wspó³rzêdne gracza
    Transform player;
    //wysokoœæ kamery
    public float cameraHeight = 10f;
    //prêdkoœæ kamery - do u¿ytku dla smoothdamp
    Vector3 cameraSpeed;
    //szybkoœæ wyg³adzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //pod³¹cz pozycjê gracza do lokalnej zmiennej korzystaj¹c z jego taga
        //to nie jest zapisanie wartoœci jeden raz tylko referencja do obiektu
        //to znaczy, ¿e player zawszê bedzie zawiera³ aktualn¹ pozycje gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //oblicz docelow¹ pozycje kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;

        //p³ynnie przesuñ kamerê w kierunku gracza
        //funkcja Vector3.Lerp
        //p³ynnie przechodzi z pozycji pierwszego argumentu do drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //smootdamp dzia³a jak sprê¿yna staraj¹ca siê doci¹gn¹æ kamerê do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed dampSpeed);
    }
}
