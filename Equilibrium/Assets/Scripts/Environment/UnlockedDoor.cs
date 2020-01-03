using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedDoor : MonoBehaviour
{
    private bool closed;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        closed = true;
    }

    public void Open()
    {
        print("collision");
        if (closed)
        {
            anim.SetBool("Closed", false);
            closed = false;
        }
    }
}
