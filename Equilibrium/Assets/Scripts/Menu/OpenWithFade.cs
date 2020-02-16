using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenWithFade : MonoBehaviour
{
    [SerializeField] Image img;

    void Start()
    {
        StartCoroutine(Fade(3f));
    }

    IEnumerator Fade(float time)
    {
        yield return new WaitForSeconds(time);
        img.CrossFadeAlpha(0, 1.2f, false);
        yield return new WaitForSeconds(time + 1.2f);
        SceneManager.LoadScene("MainMenu");
    }
}
