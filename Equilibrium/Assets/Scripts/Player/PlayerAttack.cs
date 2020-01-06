using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int light = 0;
    [SerializeField] private float absorbRange = 5;
    [SerializeField] private float doorOpenRange = 4;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject lightBulletPrefab;
    [SerializeField] private GameObject lantern;
    [SerializeField] private Transform firepoint;
    [SerializeField] private TextMeshProUGUI lightBulletText;
    [SerializeField] private GameObject lanternRadialBar;
    private RadialProgressBar lanternRadialProgressBar;

    [SerializeField] private float shootCooldownTime = 2; //tempo di ricarica tra un attacco e l'altro
    private bool shootOnCooldown = false;

    [SerializeField] private float lanternCooldownTime = 3; //tempo ricarica lanterna
    private bool lanternOnCooldown = false;

    [SerializeField] public GameObject ui;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Image absorbIndicatorImage;
    void Start()
    {
        lanternRadialProgressBar = lanternRadialBar.GetComponent<RadialProgressBar>();
        lightBulletText = lightBulletText.GetComponent<TextMeshProUGUI>();
        lanternRadialProgressBar.maxTime = lanternCooldownTime * 2;
        if (lightBulletText != null)
            lightBulletText.text = "" + light;
        
        // Get the images of the crosshair and the indicator from the UI
        Image[] images = ui.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image.CompareTag("Crosshair"))
                crosshairImage = image;
            else if (image.CompareTag("Absorb_indicator"))
                absorbIndicatorImage = image;
        }
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;
        ControlLight();
        ShootLight();
        ToggleLantern();
        
        // Raycast and check if we're hitting a light
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, absorbRange))
        {
            if (hit.collider.CompareTag("Light"))
            {
                // Enable the indicator signaling that there is a light absorbable/placeable
                absorbIndicatorImage.enabled = true;
                crosshairImage.enabled = false;
            }
            else
            {
                absorbIndicatorImage.enabled = false;
                crosshairImage.enabled = true;
            }
        }
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
                //print("colpito qualcosa");
                LightSource2 ls = hit.collider.gameObject.GetComponent<LightSource2>();
                LightTrigger lt = hit.collider.gameObject.GetComponent<LightTrigger>();

                if (ls) //se è una sorgente di luce
                {
                    if (ls.takeLight()) //se è accesa --> prendo luce
                    {
                        Light++;
                        if (lt) lt.Trigger();
                    }

                    else if (Light > 0 && ls.PutLight()) // se è spenta e ho munizioni --> rilascio luce
                    {
                        Light--;
                        if (lt) lt.Trigger();
                    }
                }
            } 
            
            if(Physics.Raycast(ray, out hit, doorOpenRange)) //secondo raycast più corto per vedere se posso aprire una porta
            {
                UnlockedDoor ud = hit.collider.gameObject.GetComponent<UnlockedDoor>();
                if (ud)
                    ud.Open();
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
            bullet.transform.position = firepoint.transform.position;
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
        lanternRadialProgressBar.StartLoading();
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
                lightBulletText.text = "" + light;
        }
    }
}
