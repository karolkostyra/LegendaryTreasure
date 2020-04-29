using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AdventureGame : MonoBehaviour
{
    PirateStats pirate;
    string strName, strValue;

    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValueText;
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] State startingState;
    //[SerializeField] State instructionState;
    [SerializeField] State endingState;
    [SerializeField] State[] defeatConditions;

    Sprite nextImage;
    GameObject imageFinder;
    State state;

    public List<State> myStates;
    bool flagNextState;
    int count;
    bool end;


    private void Start()
    {
        pirate = new PirateStats();
        flagNextState = true;
        end = false;
        imageFinder = GameObject.Find("Canvas/Panel/Image");
        state = startingState;

        GameObject introText = GameObject.Find("Intro Frame");
        introText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = state.GetStateStory();
    }


    private void Update()
    {
        nextImage = state.GetStateImage();
        ManageState();
        ManageStatistics();
    }


    private void ManageState()
    {
        var nextStates = state.GetNextStates();
        count = myStates.Count;


        var nextStat = state.GetImpactOnStats();
        if (state.GetImpactOnStats().Length == 4 & flagNextState)
        {
            for (int i = 0; i < nextStat.Length; i++)
            {
                pirate.pirateStatistics[nextStat[i].nameOfStat] += nextStat[i].valueOfStat;
            }
            flagNextState = false;
        }


        for (int i = 0; i < nextStates.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                state = nextStates[i];
                flagNextState = true;
            }
        }


        if(Input.GetKeyDown(KeyCode.Space) && state.GetSpaceIsActive())
        {
            if(count > 0)
            {
                state = myStates[UnityEngine.Random.Range(0, count)];
                myStates.Remove(state);
            }

            if(count == 0 & !end)
            {
                state = endingState;
                end = true;
            }
        }

        storyText.text = state.GetStateStory();
        var setImage = imageFinder.GetComponent<Image>();
        setImage.sprite = nextImage;
    }


    private void ManageStatistics()
    {
        int i = 0;
        foreach(var item in pirate.pirateStatistics)
        {
            if(item.Key != "None" & item.Value > 0)
            {
                strName += item.Key + ": \n";
                strValue += item.Value + "%\n";
            }
            if(item.Value <= 0)
            {
                strName += item.Key + ": \n";
                strValue += item.Value + "%\n";
                state = defeatConditions[i];
                storyText.text = state.GetStateStory();
            }
            if(item.Value >= 100)
            {
                state = defeatConditions[i+3];
                storyText.text = state.GetStateStory();
            }
            i++;
        }
        statNameText.text = strName;
        statValueText.text = strValue;
        strName = strValue = "";
    }

    public State GetCurrentState()
    {
        return state;
    }
}
