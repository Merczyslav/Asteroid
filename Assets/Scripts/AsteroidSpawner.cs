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

    //odleg³oœæ w jakiej spawnuj¹ sie asteroidy
    public float spawnDistance = 10;

    //odleg³oœæ pomiedzy asteroidami
    public float safeDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        //znajdŸ gracza i przypisz do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnAsteroid(staticAsteroid);


        AsteroidCountControll();
    }

    GameObject? SpawnAsteroid(GameObject prefab)
    {
        //generyczna funkcja s³u¿¹ca do wylosowania wspó³rzêdnych i umieszczenia
        //w tym miejscu asteroidy z prefaba



        //stwórz losow¹ pozycje na okrêgu (x, y)
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        //losowa pozycha w odleg³oœci 10 jednostek od œrodka œwiata
        //mapujemy x->x , y->0
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * spawnDistance;

        //na³ó¿ pozycje gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //sprawdŸ czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if (!Physics.CheckSphere(randomPosition, safeDistance))
        {
            //stwórz zmienn¹ asteroid, zespawnuj nowy asteroid korzystaj¹c z prefaba w losowym miejscu, z rotacj¹ domyœln¹ (Quaternion.indentity)
            GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

            //zwróæ asteroidê jako wynik dzia³ania
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

        //przejdŸ pêtl¹ przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg³oœæ od gracza

            //wektor przesuniêcia miedzy graczem a asteroid¹
            //o ile musze przesunac gracza, zeby znalaz³ sie w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;

            float distanceToPlayer = delta.magnitude;

            if(distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
            
        }
    }
}
