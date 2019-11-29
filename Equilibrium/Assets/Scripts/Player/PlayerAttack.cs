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
    [SerializeField] private Transform firepoint;
    [SerializeField] private TextMeshProUGUI lightBulletText;
    [SerializeField] private float cooldownTime = 2; //tempo di ricarica tra un attacco e l'altro
    private bool onCooldown = false;

    void Start()
    {
        lightBulletText = lightBulletText.GetComponent<TextMeshProUGUI>();
        if (lightBulletText != null)
            lightBulletText.text = "LIGHT " + light;
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;
        ControlLight();
        ShootLight();
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
                //se è una sorgente di luce ed è accesa --> prendo munizioni
                if (ls != null && ls.takeLight()) 
                {
                    print("light absorbed");
                    ++Light;
                }
                //se è una sorgente di luce ed è spenta, e ho munizioni --> rilascio luce
                else if (ls != null && Light > 0 && ls.PutLight()) 
                {
                    print("light released");
                    --Light;
                }
                //se è un trigger e non è attivo, lo attivo
                else if (lt != null && Light > 0 && !lt.IsTriggered())
                {
                    lt.Trigger();
                    --Light;
                    print("attivo bottone");
                }
            }                    
        }
    }

    //spara un proiettile di luce
    private void ShootLight()
    {
        if (onCooldown) //se ho appena sparato sono in cooldown
            return;

        if (Input.GetButtonDown("ShootLight") && light > 0) //se premo il tasto sinistro e ho munizioni di luce
        {
            --Light;
            GameObject bullet = Instantiate(lightBulletPrefab);
            bullet.transform.position = camera.transform.position;
            bullet.transform.forward = camera.transform.forward;
            StartCoroutine(Cooldown());
        } 
    }

    private IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
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
