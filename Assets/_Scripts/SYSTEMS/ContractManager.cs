using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    public Animator contractAnimator;

    private void OnDisable()
    {
        SceneLoader.instance.Loaded -= LoadContract;
        SignatureInput.instance.ShowSubmitButton -= DisplaySubmit;
    }

    private void Start()
    {
        SceneLoader.instance.Loaded += LoadContract;
        SignatureInput.instance.ShowSubmitButton += DisplaySubmit;
        contractAnimator.speed = 0;
        SceneLoader.instance.FadeFromBlack();
    }

    public void LoadContract() => contractAnimator.speed = 1;
    public void DisplaySignature() => contractAnimator.SetTrigger("DisplaySignature");
    public void DisplaySubmit() => contractAnimator.SetTrigger("DisplaySubmit");

    public void ANIM_SelectSignatureInput() => SignatureInput.instance.SelectInputField();
}
