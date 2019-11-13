using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int light = 0;
    [SerializeField] private float absorbRange = 5;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject lightBulletPrefab;
    [SerializeField] private Transform firepoint;

    void Start()
    {
        
    }

    void Update()
    {
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
                    ++light;
                }
            }                    
        }
    }

    //rimette la luce in una sorgente
    private void PutLight()
    {
        if (Input.GetButtonDown("PutLight") && light > 0) //se premo la E
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, absorbRange))  //raycast dal centro dello schermo fino a distanza absorbRange
            {
                LightSource ls = hit.collider.gameObject.GetComponent<LightSource>();
                if (ls != null && ls.getIntensity() < ls.getMaxIntensity()) //se è una sorgente di luce e non è piena di lue
                {
                    print("light released");
                    ls.putLight();
                    --light;
                }
            }
        }
    }

    //spara un proiettile di luce
    private void ShootLight ()
    {
        if (Input.GetButtonDown("ShootLight") && light > 0) //se premo il tasto sinistro e ho munizioni di luce
        {
            --light;
            GameObject bullet = Instantiate(lightBulletPrefab);
            bullet.transform.position = camera.transform.position;
            bullet.transform.forward = camera.transform.forward;
            print("munitions: " + light);
        } 
    }
}
