using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject rightHand, leftHand;
    Vector3 lastPositionRight, lastPositionLeft;

    public GameObject rightWeapon, leftWeapon, rightWeaponAlt, magicOrigin;
    public GameObject fireBall;
    GameObject currentMagic;

    public float weaponCooldown, magicCooldown = 0.0f;
    public const float WEAPON_COOLDOWN_TIME = 0.5f;
    public const float MAGIC_COOLDOWN_TIME = 2.0f;

    public bool shieldActive = false;

    public AudioClip throwClip;

    void Start()
    {
        rightWeaponAlt.SetActive(false);
    }
    
    void Update()
    {
        weaponCooldown += Time.deltaTime;
        magicCooldown += Time.deltaTime;

        //Cubrirse con el escudo
        if (Input.GetAxis("HTC_VIU_LeftTrigger") > 0.1 && leftWeapon.activeInHierarchy)
        {
            shieldActive = true;
        }
        else
        {
            shieldActive = false;
        }

        //Disparar la bola de fuego si se tiene la vara seleccionada
        if (Input.GetAxis("HTC_VIU_RightTrigger") > 0.1 && magicCooldown > MAGIC_COOLDOWN_TIME)
        {
            if (currentMagic != null)
            {
                magicCooldown = 0;

                Vector3 speedForce = 20f * (rightHand.transform.position - lastPositionRight) / Time.deltaTime; //Formula de la velocidad, v = d/t, pero aqui se multiplica por 20 para que el lanzamiento sea mas fuerte

                currentMagic.transform.parent = null; //Anula el vinculo entre la magia actual y su padre(la vara)
                currentMagic.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //Elimina las constraints que tiene la bola de fuego en el editor
                currentMagic.GetComponent<Rigidbody>().AddForce(speedForce, ForceMode.Impulse); //Lanza la bola de fuego
                
                currentMagic.GetComponent<AudioSource>().PlayOneShot(throwClip); //Reproduce el audio de lanzamiento

                Invoke("ChargeFireBall", MAGIC_COOLDOWN_TIME); //Y vuelve a cargar una bola de fuego despues de cierto tiempo
            }
        }

        //Mostrar ocultar escudo
        if (Input.GetAxis("HTC_VIU_LeftGrip") > 0.1 && weaponCooldown > WEAPON_COOLDOWN_TIME)
        {
            weaponCooldown = 0;
            leftWeapon.SetActive(!leftWeapon.activeInHierarchy);
        }

        //Cambiar entre espada y vara
        if (Input.GetAxis("HTC_VIU_RightGrip") > 0.1 && weaponCooldown > WEAPON_COOLDOWN_TIME)
        {
            weaponCooldown = 0;
            rightWeapon.SetActive(!rightWeapon.activeInHierarchy);
            rightWeaponAlt.SetActive(!rightWeaponAlt.activeInHierarchy);

            if (rightWeaponAlt.activeInHierarchy)
            {
                ChargeFireBall();
            }
            else
            {
                Destroy(currentMagic);
            }
        }

        lastPositionRight = rightHand.transform.position;
        lastPositionLeft = leftHand.transform.position;
    }

    //Funcion para cargar la bola de fuego
    private void ChargeFireBall()
    {
        if (currentMagic != null)
        {
            Destroy(currentMagic);
        }

        currentMagic = Instantiate(fireBall, magicOrigin.transform); //No se pone .position porque si no la bola de fuego se quedaria en la posicion del momento en que fue instanciada, osea, en el aire
    }
}