using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] List<LightTrigger> triggerList = new List<LightTrigger>(); //lista di bottoni che attivano la porta

    void Start()
    {
        StartCoroutine(UnlockTest(0.33f));
    }

    //coroutine che controlla ogni testTime secondi se la porta è aperta
    //termina quando la porta è aperta
    private IEnumerator UnlockTest(float testTime)
    {   while (true)
        {
            bool unlocked = true;
            foreach (LightTrigger trigger in triggerList)
                unlocked = unlocked & trigger.IsTriggered();

            if (unlocked)
            {
                print("door unlocked");
                yield break;
            }

            unlocked = true;
            yield return new WaitForSeconds(testTime);
        }
    }
}
