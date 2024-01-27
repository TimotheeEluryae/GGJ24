using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public float delayBetweenCaracter;

    public TMP_Text clientNameTxt;
    public TMP_Text clientSpeakingTxt;
    public GameObject continueButton;
    public TMP_Text continueButtonTxt;

    Coroutine currentCoroutine;

    List<Dialog> currentDialogs = new List<Dialog>();

    int currentDialogIndex = -1;
    bool isWriting = false;

    public static DialogSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDialog(string clientName, List<Dialog> dialogs)
    {
        continueButton.SetActive(true);
        currentDialogs = dialogs;
        clientNameTxt.text = clientName;
        currentDialogIndex = 0;
        clientSpeakingTxt.text = "";
        continueButtonTxt.text = "Continuer";
        currentCoroutine = StartCoroutine(SetTmpTxt(currentDialogs[currentDialogIndex].txt));
    }

    public void NextButton()
    {
        if (currentDialogIndex >= currentDialogs.Count) return;

        if (isWriting)
        {
            StopCoroutine(currentCoroutine);
            clientSpeakingTxt.text = currentDialogs[currentDialogIndex].txt;
            isWriting = false;
        }
        else
        {
            currentDialogIndex += 1;

            if(currentDialogIndex >= currentDialogs.Count)
            {
                continueButtonTxt.text = "Terminer";
                EndDialog();
                return;
            }
            clientSpeakingTxt.text = "";
            currentCoroutine = StartCoroutine(SetTmpTxt(currentDialogs[currentDialogIndex].txt));
        } 
    }

    void EndDialog()
    {
        LoopManager.instance.EndSpeak();
        continueButton.SetActive(false);
        isWriting = false;
    }

    IEnumerator SetTmpTxt(string textToWrite)
    {
        isWriting = true;
        foreach (char letter in textToWrite)
        {
            clientSpeakingTxt.text += letter;

            yield return new WaitForSeconds(delayBetweenCaracter);
        }
        isWriting = false;

        if(currentDialogIndex + 1 >= currentDialogs.Count) continueButtonTxt.text = "Terminer";
    }
}