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
        Invoke("DisplayContract", 1.5f);
    }

    public void DisplayContract() => SceneLoader.instance.FadeFromBlack(0.5f);

    public void LoadContract() => contractAnimator.speed = 1;
    public void DisplaySignature() => contractAnimator.SetTrigger("DisplaySignature");
    public void DisplaySubmit() => contractAnimator.SetTrigger("DisplaySubmit");

    public void PlaySFX() => SFXManager.instance.PlayEndDaySFX();
    public void CursorLock() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
    public void ANIM_SelectSignatureInput() => SignatureInput.instance.SelectInputField();
    public void ANIM_PlayShowSubmitButtonSFX() => SFXManager.instance.Play("email_send");
}
