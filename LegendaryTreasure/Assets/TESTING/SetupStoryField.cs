using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupStoryField : MonoBehaviour
{
    private State state;
    [SerializeField] AdventureGame adventureGame;
    [SerializeField] GameObject storyText;
    [SerializeField] GameObject introText;

    //BUTTONS - max 3 choices for state
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject button_1;
    [SerializeField] GameObject button_2;
    [SerializeField] GameObject button_3;

    private RectTransform storyRectTransform;
    private Vector2 storySizeDelta;
    private State currentState;
    private State previousCurrentState;


    private void Start() //-153 / 434
    {
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
            Debug.Log("BYLA ZMIANA");
            ActiveButtons();
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

    private void ActiveButtons()
    {
        DeactivateButtons();
        previousCurrentState = currentState;
        switch (currentState.GetNumberOfChoices())
        {
            case 1:
                Debug.Log("1");
                ActiveButtons2(1);
                break;
            case 2:
                Debug.Log("2");
                ActiveButtons2(2);
                break;
            case 3:
                Debug.Log("3");
                ActiveButtons2(3);
                break;
        }
    }

    private void ActiveButtons2(int value)
    {
        for(int i=0; i<value; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    private void DeactivateButtons()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
        buttons[2].SetActive(false);
    }
}
