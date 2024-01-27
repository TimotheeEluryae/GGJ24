using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public float delayBetweenCaracter;

    public TMP_Text clientNameTxt;
    public TMP_Text clientSpeakingTxt;

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
        currentDialogs = dialogs;
        clientNameTxt.text = clientName;
    }

    public void NextButton()
    {
        if(isWriting) StopCoroutine(currentCoroutine);
        else
        {
            currentDialogIndex += 1;

            if(currentDialogIndex >= currentDialogs.Count)
            {
                EndDialog();
                return;
            }
            currentCoroutine = StartCoroutine(SetTmpTxt(currentDialogs[currentDialogIndex].txt));
        } 
    }

    void EndDialog()
    {
        //Arretez le dialogue
    }

    IEnumerator SetTmpTxt(string textToWrite)
    {
        isWriting = true;
        foreach (char letter in textToWrite)
        {
            clientNameTxt.text += letter;

            yield return new WaitForSeconds(delayBetweenCaracter);
        }
        isWriting = false;
    }
}