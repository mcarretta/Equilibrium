using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int light = 0;
    [SerializeField] private float absorbRange = 5;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject lightBulletPrefab;
    [SerializeField] private Transform firepoint;
    [SerializeField] private Text lightBulletText;

    void Start()
    {
        //lightBulletText.text = "LIGHT: " + light;
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;
        AbsorbLight();
        PutLight();
        ShootLight();
    }

    //assorbe luce da una sorgente
    private void AbsorbLight()
    {
        if (Input.GetButtonDown("AbsorbLight")) //se premo il tasto destro
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, absorbRange))  //raycast dal centro dello schermo fino a distanza absorbRange
            {
                LightSource ls = hit.collider.gameObject.GetComponent<LightSource>();
                if (ls != null && ls.getIntensity() > 0) //se è una sorgente di luce ed è accesa, prendo munizioni
                {
                    print("light absorbed");
                    ls.takeLight();
                    ++Light;
                }
            }                    
        }
    }

    //rimette la luce in una sorgente
    private void PutLight()
    {
        if (Input.GetButtonDown("PutLight") && Light > 0) //se premo la E
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, absorbRange))  //raycast dal centro dello schermo fino a distanza absorbRange
            {
                LightSource ls = hit.collider.gameObject.GetComponent<LightSource>();
                LightTrigger lt = hit.collider.gameObject.GetComponent<LightTrigger>();
                //se è una sorgente di luce e non è piena di luce
                if (ls != null && ls.getIntensity() < ls.getMaxIntensity()) 
                {
                    print("light released");
                    ls.putLight();
                    --Light;
                }
                //se è un trigger e non è attivo, lo attivo
                if (lt != null && !lt.IsTriggered())
                {
                    lt.Trigger();
                    --Light;
                    print("attivo bottone");
                }
            }
        }
    }

    //spara un proiettile di luce
    private void ShootLight ()
    {
        if (Input.GetButtonDown("ShootLight") && light > 0) //se premo il tasto sinistro e ho munizioni di luce
        {
            --Light;
            GameObject bullet = Instantiate(lightBulletPrefab);
            bullet.transform.position = camera.transform.position;
            bullet.transform.forward = camera.transform.forward;
        } 
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
            //lightBulletText.text = "LIGHT: " + light;
        }
    }
}
