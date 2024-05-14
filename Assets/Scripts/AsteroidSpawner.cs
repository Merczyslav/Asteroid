using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //gracz (jego pozycja)
    Transform player;

    //prefab statycznej asteroidy
    public GameObject staticAsteroid;

    //czas od ostatnio wygenerowanej asteroidy
    float timeSinceSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //znajd� gracza i przypisz do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //dolicz czas od ostatniej klatki
        timeSinceSpawn += Time.deltaTime;
        //je�eli czas przekrocyz� sekunde to spawnuj i zresetuj
        if (timeSinceSpawn > 0.6)
        {
            GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }

        AsteroidCountControll();
    }

    GameObject? SpawnAsteroid(GameObject prefab)
    {
        //generyczna funkcja s�u��ca do wylosowania wsp�rz�dnych i umieszczenia
        //w tym miejscu asteroidy z prefaba



        //stw�rz losow� pozycje na okr�gu (x, y)
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        //losowa pozycha w odleg�o�ci 10 jednostek od �rodka �wiata
        //mapujemy x->x , y->0
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * 10;

        //na�� pozycje gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //sprawd� czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if (!Physics.CheckSphere(randomPosition, 5))
        {
            //stw�rz zmienn� asteroid, zespawnuj nowy asteroid korzystaj�c z prefaba w losowym miejscu, z rotacj� domy�ln� (Quaternion.indentity)
            GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

            //zwr�� asteroid� jako wynik dzia�ania
            return asteroid;
        }
        else
        {
            return null;
        }


    }
    void AsteroidCountControll()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //przejd� p�tl� przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg�o�� od gracza

            //wektor przesuni�cia miedzy graczem a asteroid�
            //o ile musze przesunac gracza, zeby znalaz� sie w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;

            float distanceToPlayer = delta.magnitude;

            if(distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
            
        }
    }
}
