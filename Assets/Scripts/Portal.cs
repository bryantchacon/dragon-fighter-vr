using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject[] enemies; //Array de enemigos
    public int numberOfEnemies; //Numero de enemigos a instanciar por partida

    void Start()
    {
        StartCoroutine("GenerateNewEnemy"); //Activacion de la corrutina de generacion de enemigos al iniciar el juego
    }

    IEnumerator GenerateNewEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, transform); //Instancia al enemigo desde el game object que tenga este script
            yield return new WaitForSeconds(Random.Range(10, 20));
        }
    }
}