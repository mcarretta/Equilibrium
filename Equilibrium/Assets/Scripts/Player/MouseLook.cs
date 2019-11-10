using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //time.deltatime è il tempo passato dall'ultimo update, serve per avere la stessa velocità di rotazione
        //indipendentemente dal framerate
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //limito la rotazione della telecamera in alto e in basso
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //sull'asse X ruoto solo la telecamera, non il giocatore
        playerBody.Rotate(Vector3.up * mouseX); //ruoto il giocatore sull'asse Y
    }
}
