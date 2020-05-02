using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryField : MonoBehaviour
{
    private State state;
    [SerializeField] AdventureGame adventureGame;
    [SerializeField] GameObject storyText;
    [SerializeField] GameObject introText;

    //BUTTONS - currently max 3 choices for state
    [SerializeField] GameObject[] buttons;

    private RectTransform storyRectTransform;
    private Vector2 storySizeDelta;
    private State currentState;
    private State previousCurrentState;
    private float sizeStoryText;

    private void Start()
    {
        DeactivateButtons();
        storyText.SetActive(false);
        introText.SetActive(false);

        storyRectTransform = storyText.GetComponent<RectTransform>();
        storySizeDelta = storyRectTransform.sizeDelta;

        previousCurrentState = adventureGame.GetCurrentState();
    }
    
    private void Update()
    {
        currentState = adventureGame.GetCurrentState();
        if (currentState.GetIntroductionVar())
        {
            storyText.SetActive(false);
            introText.SetActive(true);
        }
        else
        {
            introText.SetActive(false);
            storyText.SetActive(true);
        }

        if(previousCurrentState != currentState)
        {
            sizeStoryText = storyText.GetComponentInChildren<TextMeshProUGUI>().fontSize;
            CheckNumberOfChoices();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            storyRectTransform = storyText.GetComponent<RectTransform>();
            storySizeDelta = storyRectTransform.sizeDelta;
            //Debug.Log(currentState.GetCurrentState().GetNumberOfChoices());
            storySizeDelta = new Vector2(storySizeDelta.x, 200);
            CreateButtons(currentState.GetNumberOfChoices());
        }
    }

    private void CreateButtons(int numberOfButtons)
    {
        for(int i=0; i<numberOfButtons; i++)
        {
            GameObject button = new GameObject();
            button.AddComponent<RectTransform>();
            button.AddComponent<Button>();

            var panel = GameObject.Find("Canvas");
            button.transform.position = panel.transform.position;
            button.transform.position -= new Vector3(0,i*100,0);
            button.GetComponent<RectTransform>().SetParent(panel.transform);
            //button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 10);
            button.SetActive(true);
        } 
    }

    private void CheckNumberOfChoices()
    {
        DeactivateButtons();
        previousCurrentState = currentState;
        switch (currentState.GetNumberOfChoices())
        {
            case 0:
                Debug.Log("Space");
                ConfirmButton();
                break;
            case 1:
                Debug.Log("1");
                ActivateButtons(1);
                break;
            case 2:
                Debug.Log("2");
                ActivateButtons(2);
                break;
            case 3:
                Debug.Log("3");
                ActivateButtons(3);
                break;
        }
    }

    private void ActivateButtons(int value)
    {
        var choices = currentState.GetChoicesText();

        for(int i=0; i<value; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = sizeStoryText;
        }
    }

    private void DeactivateButtons()
    {
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.Left;

        for(int i=0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    private void ConfirmButton()
    {
        buttons[1].SetActive(true);
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.Center;
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Press 'space' or click here to continue...";
    }
}
