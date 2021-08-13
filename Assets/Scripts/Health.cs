using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public DamageType damageType = DamageType.enemy; //Se configura como enemigo por default pero se puede cambiar en el editor, el tipo de dato viene del enum en Damage

    //Propiedad para poder manejar la variable privada healthPoints del personaje
    public float HealthPoints
    {
        get {return healthPoints; } //get regresa el valor de healthPoints

        set //set lo asigna, y puede tener otras funciones
        {
            healthPoints = value; //Asigna a healthPoints el valor que se pase por medio de HealthPoints

            if (healthPoints <= 0) //Si la healthPoints es menor o igual a 0...
            {
                //TODO: Gestionar la muerte del personaje
            }
        }
    }
    [SerializeField] private float healthPoints = 100.0f; //Tiene 100 como valor por default


    void Start()
    {
        
    }
}