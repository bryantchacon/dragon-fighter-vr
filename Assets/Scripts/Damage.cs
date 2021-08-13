using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script esta en los game objects que hacen daño

public enum DamageType
{
    player,
    enemy
}

public class Damage : MonoBehaviour
{
    public DamageType damageType = DamageType.enemy; //Se configura como enemigo por default pero se puede cambiar en el editor
    public float damageAmount = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null && other.GetComponent<Health>().damageType != this.damageType) //Si el player/enemigo choca contra algo con vida y su tipo de daño es diferente al mio(osea es el player)...
        {
            //Codigo para reducir el daño inflingido por el enemigo al personaje cuando golpea el escudo
            float currentDamage = damageAmount; //damageAmount se asigna a esta variable porque cuando el enemigo choque contra el escudo del player, el daño se reducira, como lo indica el siguiente if
            if (other.GetComponent<Weapons>() != null && other.GetComponent<Weapons>().shieldActive) //Si el enemigo choca contra un arma y el escudo esta activo(osea choca contra el escudo activo)...
            {
                currentDamage /= 5; //El daño se divide entre 5
            }

            other.GetComponent<Health>().HealthPoints -= currentDamage; //Le hara daño al player/enemigo
        }
    }
}