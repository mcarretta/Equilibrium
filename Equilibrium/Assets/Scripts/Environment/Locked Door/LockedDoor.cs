using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] List<LightTrigger> triggerList = new List<LightTrigger>(); //lista di bottoni che attivano la porta
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(UnlockTest(0.33f));
    }

    //coroutine che controlla ogni testTime secondi se la porta è aperta
    private IEnumerator UnlockTest(float testTime)
    {   while (true)
        {
            bool unlocked = true;
            foreach (LightTrigger trigger in triggerList)
                unlocked = unlocked & trigger.IsTriggered();

            if (unlocked)
                anim.SetBool("Closed", false);
            else
                anim.SetBool("Closed", true);

            yield return new WaitForSeconds(testTime);
        }
    }
}
