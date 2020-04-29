using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupStoryField : MonoBehaviour
{
    private State state;
    [SerializeField] AdventureGame currentState;
    [SerializeField] GameObject storyText;
    [SerializeField] GameObject introText;

    private RectTransform storyRectTransform;
    private Vector2 storySizeDelta;
    private AdventureGame previousCurrentState;
    private bool flag;

    private void Start() //-153 / 434
    {
        storyText.SetActive(false);
        introText.SetActive(false);

        storyRectTransform = storyText.GetComponent<RectTransform>();
        storySizeDelta = storyRectTransform.sizeDelta;
        flag = true;
    }
    
    private void Update()
    {
        if (currentState.GetCurrentState().GetIntroductionVar())
        {
            storyText.SetActive(false);
            introText.SetActive(true);
        }
        else
        {
            introText.SetActive(false);
            storyText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            storyRectTransform = storyText.GetComponent<RectTransform>();
            storySizeDelta = storyRectTransform.sizeDelta;
            //Debug.Log(currentState.GetCurrentState().GetNumberOfChoices());
            storySizeDelta = new Vector2(storySizeDelta.x, 200);
            CreateButtons(currentState.GetCurrentState().GetNumberOfChoices());
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
}
