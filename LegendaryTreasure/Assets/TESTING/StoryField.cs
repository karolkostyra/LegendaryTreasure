using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryField : MonoBehaviour
{
    [SerializeField] AdventureGame adventureGame;
    [SerializeField] ScrollbarSetup scrollbarSetup;
    [SerializeField] GameObject storyText;
    [SerializeField] GameObject introText;

    //BUTTONS - currently max 3 choices for state
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject confirmButton;

    private RectTransform storyRectTransform;
    private Vector2 storySizeDelta;
    private State currentState;
    private State previousCurrentState;
    private float sizeStoryText;


    private void Start()
    {
        DeactivateChoiceButtons();
        storyText.SetActive(false);
        introText.SetActive(false);

        storyRectTransform = storyText.GetComponent<RectTransform>();
        storySizeDelta = storyRectTransform.sizeDelta;

        previousCurrentState = adventureGame.GetCurrentState();
    }

    private void Update()
    {
        currentState = adventureGame.GetCurrentState();
        SetupConfirmButton();
        if (currentState.GetIntroductionVar())
        {
            storyText.SetActive(false);
            introText.SetActive(true);
            if (scrollbarSetup.EndOfScroll())
            {
                ConfirmButton(true);
            }
            else
            {
                ConfirmButton(false);
            }
        }
        else
        {
            introText.SetActive(false);
            storyText.SetActive(true);
        }

        if (previousCurrentState != currentState)
        {
            sizeStoryText = storyText.GetComponentInChildren<TextMeshProUGUI>().fontSize;
            CheckNumberOfChoices();
        }
    }

    private void CheckNumberOfChoices()
    {
        DeactivateChoiceButtons();
        previousCurrentState = currentState;
        switch (currentState.GetNumberOfChoices())
        {
            case 0:
                Debug.Log("Space");
                ConfirmButton(true);
                break;
            case 1:
                Debug.Log("1");
                ActivateChoiceButtons(1);
                break;
            case 2:
                Debug.Log("2");
                ActivateChoiceButtons(2);
                break;
            case 3:
                Debug.Log("3");
                ActivateChoiceButtons(3);
                break;
        }
    }

    private void ActivateChoiceButtons(int value)
    {
        var choices = currentState.GetChoicesText();

        for(int i=0; i<value; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = sizeStoryText;
        }
    }

    private void DeactivateChoiceButtons()
    {
        for(int i=0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    private void SetupConfirmButton()
    {
        float textSize;
        if(storyText.activeSelf == false)
        {
            textSize = introText.GetComponentInChildren<TextMeshProUGUI>().fontSize;
        }
        else
        {
            textSize = storyText.GetComponentInChildren<TextMeshProUGUI>().fontSize;
        }
        confirmButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = textSize;
        confirmButton.GetComponentInChildren<TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.Center;
        confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = "Press 'space' or click here to continue...";
    }

    private void ConfirmButton(bool flag)
    {
        confirmButton.SetActive(flag);
    }
}
