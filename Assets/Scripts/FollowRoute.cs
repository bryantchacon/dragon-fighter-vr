using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ESTE SCRIPT ESTA EN CADA DRAGON

public class FollowRoute : MonoBehaviour
{
    GameObject[] waypoints; //Variable array donde se guardaran los waypoints
    Transform newWaypointToGo; //Variable donde se guardara la posicion del nuevo waypoint a ir
    public float dragonSpeed = 5.0f;
    GameObject player;
    public float attackRatio = 0.9f; //Entre mas cercano a uno menos atacaran los dragones
    Animator dragonAnimator;
    
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint"); //FindGameObjectsWithTag en plural es para localizar varios game objects
        player = GameObject.FindWithTag("Player"); //FindWithTag es para encontrar un solo game object con una etiqueta unica en todo el juego
        dragonAnimator = GetComponent<Animator>(); //Recupera el Animator del dragon

        newWaypointToGo = waypoints[Random.Range(0, waypoints.Length)].transform; //Elige aleatoriamente el nuevo waypont a ir con sus coordenadas, al final se pone .transform porque de ese tipo es la variable donde se guardara
    }

    void Update()
    {
        //TRASLACION
        float distance = dragonSpeed * Time.deltaTime; //Calculo de la distancia que se movera el dragon
        transform.position = Vector3.MoveTowards(transform.position, newWaypointToGo.position, distance); //Mueve al dragon desde su posicion actual a la que ira a travez de determinada distancia. Parametros: posicion actual, waypoint al que ira y distancia que recorrera. NOTA: transform se refiere al componente Transform del game object, y .position al parametro Position de el, que es donde estan las tres coordenadas del game object

        //ROTACION
        Vector3 targetPosition = newWaypointToGo.position - transform.position; //Posicion del waypoint RESPECTO DE LA POSICION ACTUAL del dragon
        Vector3 directionToRotate = Vector3.RotateTowards(transform.forward, targetPosition, distance, 0); //Rotacion del dragon al waypoint. Parametros: Posicion actual a la que se esta mirando, waypoint al que mirara, distancia que recorrera y cantidad incremental de rotacion(si se requiere que vaya rotando mas rapido)
        transform.rotation = Quaternion.LookRotation(directionToRotate); //Rota al dragon usando la variable anterior como parametro de la funcion LookRotation de la clase Quaternion y aplicandolo a la posicion actual

        //CAMBIO DE RUTA
        if (targetPosition.magnitude < 0.5f) //Si la magnitud entre el waypoint a ir y la posicion actual del dragon es menor a 0.5...
        {
            if (Random.Range(0.0f, 1.0f) < attackRatio) //Y si el numero generado aleatoriamente es menor al attackRatio...
            {
                newWaypointToGo = waypoints[Random.Range(0, waypoints.Length)].transform; //El dragon se ira a otro waypoint
            }
            else //Si no...
            {
                newWaypointToGo = player.transform; //El 10% restante atacara al player
            }
        }

        //ACTIVA LA ANIMACION DE ATAQUE
        dragonAnimator.SetFloat("Distance", targetPosition.magnitude); //Asigna el parametro Distance del animator del dragon (NOTA: Solo el Terror Dragon tiene los parametros configurados)
    }

    //Distancia = Velocidad * Tiempo
    //Velodicad = Distancia / Tiempo
    //Distancia de un punto a otro = Punto final - punto inicial
}