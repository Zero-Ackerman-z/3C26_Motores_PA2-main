using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGenerator : MonoBehaviour
{
    public List<GameObject> actual_Objects = new List<GameObject>(); // Lista de dulces generados
    public static ObjectGenerator instance;
    public ObjectsGeneratorData objectsGeneratorData; // ScriptableObject 
    private float actualTime = 0f;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        if (objectsGeneratorData.timeToCreate <= actualTime)
        {
            int randomIndex = Random.Range(0, objectsGeneratorData.Objects.Length);
            GameObject objects = Instantiate(objectsGeneratorData.Objects[randomIndex],
                new Vector3(transform.position.x, Random.Range(objectsGeneratorData.limitInferior, objectsGeneratorData.limitSuperior), 0f), Quaternion.identity);
            objects.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0);
            actualTime = 0f;
            actual_Objects.Add(objects); // Usar la lista del ScriptableObject
        }
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectsGeneratorData.limitInferior = -(bounds.y * 0.9f);
        objectsGeneratorData.limitSuperior = (bounds.y * 0.9f);
    }

    public void ManageObjects(ObjectsController candyScript, PlayerMovement playerScript = null)
    {
        ScoreData scoreData = new ScoreData();
        if (playerScript == null)
        {
            Destroy(candyScript.gameObject);
            RemoveCandyFromList(candyScript.gameObject);
            int scoreToAdd = candyScript.frame; // Obtener el puntaje del objeto
            scoreData.AddScore(scoreToAdd); // Agregar el puntaje al jugador
        }
        else
        {
            int scoreToAdd = candyScript.frame; // Obtener el puntaje del objeto
            scoreData.AddScore(scoreToAdd); // Agregar el puntaje al jugador

            if (candyScript.frame == 3)
            {
                SceneManager.LoadScene("GameOver");
                return;
            }

            int lives = playerScript.player_lives;
            int liveChanger = candyScript.lifeChanges;
            lives += liveChanger;

            if (lives <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }

            playerScript.player_lives = lives;
        }
        Destroy(candyScript.gameObject);
        RemoveCandyFromList(candyScript.gameObject);

    }
    // Método para eliminar un caramelo de la  lista de dulces generados
    public void RemoveCandyFromList(GameObject candyToRemove)
    {
        actual_Objects.Remove(candyToRemove);
    }
}
