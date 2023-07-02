using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Transitions : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TestTransitions());
    }


    IEnumerator TestTransitions()
    {
        SceneLoader.instance.FadeFromBlack();

        yield return new WaitForSeconds(1.5f);

        SceneLoader.instance.FadeToBlack();

        yield return new WaitForSeconds(2f);

        SceneLoader.instance.FadeFromBlack();
    }
}
