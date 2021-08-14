using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public DamageType damageType = DamageType.enemy; //Se configura como enemigo por default pero se puede cambiar en el editor, el tipo de dato viene del enum en Damage

    private Animator characterAnimator;

    void Start()
    {
        if (GetComponent<Animator>() != null) //Si el game object que tiene este script tiene un animator... NOTA: GetComponent<> es para buscar un componente dentro del game object donde esta el mismo script
        {
            characterAnimator = GetComponent<Animator>(); //Se guarda en la variable characterAnimator...
            characterAnimator.SetFloat("Health", HealthPoints); //Y setea el valor del parametro Health del animator al de HealthPoints(propiedad de la variable privada de vida) al iniciar el juego
        }
    }

    //Propiedad para poder manejar la variable privada healthPoints del personaje
    public float HealthPoints
    {
        get {return healthPoints; } //get regresa el valor de healthPoints

        set //set lo asigna, y puede tener otras funciones
        {
            healthPoints = value; //Asigna a healthPoints el valor que se pase por medio de HealthPoints
            if (characterAnimator != null)
            {
                characterAnimator.SetFloat("Health", healthPoints); //Setea el valor del parametro Health del animator al de healthPoints cuando se setea aqui arriba
            }

            if (healthPoints <= 0) //Si la healthPoints es menor o igual a 0...
            {
                if (damageType == DamageType.enemy) //Y si el tipo de daño que hace el personaje es enemigo, osea, el personaje es un enemigo...
                {
                    Destroy(gameObject, 2.0f); //Se destruira luego de 2 segundos
                }
            }
        }
    }
    [SerializeField] private float healthPoints = 100.0f; //Tiene 100 como valor por default
}