using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    //odniesienie do menadzera poziomu
    GameObject levelManagerObject;
    //stan osłon w % (1=100%)
    float shieldCapacity = 1;
    






    // Start is called before the first frame update
    void Start()
    {
        levelManagerObject = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do współrzędnych wartość x=1, y=0, z=0 pomnożone przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja działania wygładzonego sterowania (emulacja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie prędkością
        //stwórz nowy wektor przesunięcia o wartości 1 do przodu
        Vector3 movement = transform.forward;
        //pomnożyć przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomnóż go przez "wychylenie joystika"
        movement *= Input.GetAxis("Vertical");
        //pomnóż prze prędkość lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyke
        // ---- transform.position += movement;

        //komponent fizyki wewnatrz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //dodaj siłe - do przodu statku
        rb.AddForce(movement, ForceMode.VelocityChange);


        //obrót
        //modifikacja oś "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemnóż przez czas
        rotation *= Time.deltaTime;
        //przemnóż przez klawiaturę
        rotation *= Input.GetAxis("Horizontal");
        //pomnóż przez prędkość obrotu
        rotation *= rotationSpeed;
        //dodaj obrót do obiektu
        //nie możemy użyć += ponieważ unity używa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);
        UpdateUI();



    }
    private void UpdateUI()
    {
        //metoda wykonuje wszystko związane z aktualizacją interfejsu uzytkownika
        Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPosition;
        //obróc znacznik w strone wyjscia
        transform.Find("NavUI").Find("TargetMarker").LookAt(target);
        //zmień ilość % widoczną w interfejsie
        //TextMeshProUGUI shieldText = 
            //GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshProUGUI>();
        //shieldText.text = " Shield: " + (shieldCapacity*100).ToString() + "%";

        //sprawdzamy czy poziom sie zakończył i czy musimy wyświetlić ekran końcowy
        if(levelManagerObject.GetComponent<LevelManager>().levelComplete)
        {
            GameObject.Find("Canvas").transform.Find("LevelCompleteScreen").gameObject.SetActive(true);
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.transform.CompareTag("Asteroid"))
        {
            //transform asteroidy
            Transform asteroid = collision.collider.transform;
            //policz vector według którego odepchniemy asteroide
            Vector3 shieldForce = asteroid.position - transform.position;
            //popchnij asteroide
            asteroid.GetComponent<Rigidbody>().AddForce(shieldForce * 5, ForceMode.Impulse);
            shieldCapacity -= 0.25f;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //jezeli dotkniemy znacznika końca poziomu to ustaw w levelmenager flagę, że poziom jest ukończony
        if(other.transform.CompareTag("LevelExit"))
        {
            //z
            levelManagerObject.GetComponent<LevelManager>().levelComplete = true;
        }
    }
}
