using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int light = 0;
    [SerializeField] private float absorbRange = 5;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject lightBulletPrefab;
    [SerializeField] private GameObject lantern;
    [SerializeField] private Transform firepoint;
    [SerializeField] private TextMeshProUGUI lightBulletText;
    [SerializeField] private GameObject lanternRadialBar;

    [SerializeField] private float shootCooldownTime = 2; //tempo di ricarica tra un attacco e l'altro
    private bool shootOnCooldown = false;

    [SerializeField] private float lanternCooldownTime = 3; //tempo ricarica lanterna
    private bool lanternOnCooldown = false;

    void Start()
    {
        lightBulletText = lightBulletText.GetComponent<TextMeshProUGUI>();
        lanternRadialBar.GetComponent<RadialProgressBar>().maxTime = lanternCooldownTime * 2;
        if (lightBulletText != null)
            lightBulletText.text = "LIGHT " + light;
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;
        ControlLight();
        ShootLight();
        ToggleLantern();
    }

    //assorbe luce da una sorgente
    private void ControlLight()
    {
        if (Input.GetButtonDown("AbsorbLight")) //se premo il tasto destro
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, absorbRange))  //raycast dal centro dello schermo fino a distanza absorbRange
            {
                print("colpito qualcosa");
                LightSource2 ls = hit.collider.gameObject.GetComponent<LightSource2>();
                LightTrigger lt = hit.collider.gameObject.GetComponent<LightTrigger>();

                if(ls) //se è una sorgente di luce
                {
                    if (ls.takeLight()) //se è accesa --> prendo luce
                        Light++;

                    else if (Light > 0 && ls.PutLight()) // se è spenta e ho munizioni --> rilascio luce
                        Light--;

                    if (lt) //se è uno switch per le porte lo triggero
                        lt.Trigger();
                }

            }                    
        }
    }

    //spara un proiettile di luce
    private void ShootLight()
    {
        if (shootOnCooldown) //se ho appena sparato sono in cooldown
            return;

        if (Input.GetButtonDown("ShootLight") && light > 0) //se premo il tasto sinistro e ho munizioni di luce
        {
            --Light;
            GameObject bullet = Instantiate(lightBulletPrefab);
            bullet.transform.position = camera.transform.position;
            bullet.transform.forward = camera.transform.forward;
            StartCoroutine(ShootCooldown());
        } 
    }

    private void ToggleLantern()
    {
        if (lanternOnCooldown)
            return;

        if (Input.GetButtonDown("Lantern"))
        {
            lantern.SetActive(true);
            StartCoroutine(LanternCooldown());
        }

    }

    private IEnumerator ShootCooldown()
    {
        shootOnCooldown = true;
        yield return new WaitForSeconds(shootCooldownTime);
        shootOnCooldown = false;
    }

    private IEnumerator LanternCooldown()
    {
        lanternOnCooldown = true; // la lanterna entra in cooldown
        lanternRadialBar.GetComponent<RadialProgressBar>().StartLoading();
        yield return new WaitForSeconds(lanternCooldownTime);
        lantern.SetActive(false); //dopo n secondi si spegne
        yield return new WaitForSeconds(lanternCooldownTime); //sta in cooldown n secondi
        lanternOnCooldown = false; //esce dal cooldown
    }

    public int Light
    {
        get
        {
            return light;
        }
        set
        {
            light = value;
            if(lightBulletText != null)
                lightBulletText.text = "LIGHT " + light;
        }
    }
}
