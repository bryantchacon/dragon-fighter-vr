using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    health,
    mana
}

public class MenuBar : MonoBehaviour
{
    private Slider slider; //Variable para guardar el componente Slider del mismo game object
    public GameObject target;
    public BarType barType; //Variable para seleccionar el tipo de barra desde el editor segun las opciones del enum

    void Start()
    {
        slider = GetComponent<Slider>(); //Recupera el componente Slider del mismo game object
        switch (barType) //Asigna al inicio los valores de vida y mana
        {
            case BarType.health:
                slider.maxValue = target.GetComponent<Health>().HealthPoints; //Recupera y guarda el valor de la variable healthPoints por medio de su propiedad HealthPoints
                break;
            case BarType.mana:
                slider.maxValue = Weapons.MAGIC_COOLDOWN_TIME; //Recupera y guarda el valor de la constante MAGIC_COOLDOWN_TIME de cualquier script Wapons. NOTA: No se obtiene con GetComponent por que es una constante, lo obtiene del primer Weapons que encuentre primero
                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        switch (barType) //Acualiza constantemente(por estar aqui en el Update) los valores de vida y mana
        {
            case BarType.health:
                slider.value = target.GetComponent<Health>().HealthPoints;
                break;
            case BarType.mana:
                slider.value = target.GetComponent<Weapons>().magicCooldown;
                break;
            default:
                break;
        }
    }
}