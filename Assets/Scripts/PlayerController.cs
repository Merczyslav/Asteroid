using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wsp�rz�dnych warto�� x=1, y=0, z=0 pomno�one przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzia�ania wyg�adzonego sterowania (emulacja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie pr�dko�ci�
        //stw�rz nowy wektor przesuni�cia o warto�ci 1 do przodu
        Vector3 movement = Vector3.forward;
        //pomno�y� przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomn� go przez "wychylenie joystika"
        movement *= Input.GetAxis("Vertical");
        //dodaj ruch do obiektu
        transform.position += movement;


        //obr�t
        //modifikacja o� "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemn� przez czas
        rotation *= Time.deltaTime;
        //przemn� przez klawiatur�
        rotation *= Input.GetAxis("Horizontal");
        //pomn� przez pr�dko�� obrotu
        rotation *= rotationSpeed;
        //dodaj obr�t do obiektu
        //nie mo�emy u�y� += poniewa� unity u�ywa Quaternion�w do zapisu rotacji
        transform.Rotate(rotation);



    }
}
