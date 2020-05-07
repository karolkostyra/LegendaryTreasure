using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{
    PirateStats pirate;
    string strName, strValue;

    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValueText;
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] State startingState;
    [SerializeField] State endingState;
    [SerializeField] ScrollbarSetup introScrollbar;
    [SerializeField] List<State> defeatConditions;
    //[SerializeField] State[] defeatConditions;

    [SerializeField] List<State> defeatStates = new List<State>();
    [SerializeField] List<int> defeatValues = new List<int>();

    Sprite nextImage;
    GameObject imageFinder;
    State state;

    public List<State> myStates;
    bool flagNextState;
    bool flagSkipIntro;
    bool updateStatistics;
    int count;
    bool end;


    private void Start()
    {
        pirate = new PirateStats();
        SetFlagNextState(true);
        flagSkipIntro = false;
        updateStatistics = true;
        end = false;
        imageFinder = GameObject.Find("Canvas/Panel/Image");
        state = startingState;

        GameObject introHandler = GameObject.Find("Intro Frame");
        TextMeshProUGUI introText = introHandler.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        introText.text = state.GetStateStory();
    }

    private void Update()
    {
        nextImage = state.GetStateImage();
        ManageState(-1);
        if (updateStatistics)
        {
            ManageStatistics();
        }

        if (state.GetIntroductionVar() && introScrollbar.EndOfScroll())
        {
            flagSkipIntro = true;
        }
    }

    public void ManageState(int numberTest = -1)
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
            SetFlagNextState(false);
        }

        for (int i = 0; i < nextStates.Length; i++)
        {
            if (numberTest < 0 && Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                state = nextStates[i];
                SetFlagNextState(true);
            }
            else if(numberTest > 0)
            {
                state = nextStates[numberTest-1];
                SetFlagNextState(true);
            }
            updateStatistics = true;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || numberTest == 0) && state.GetSpaceIsActive() && flagSkipIntro)
        {
            if (count > 0)
            {
                state = myStates[UnityEngine.Random.Range(0, count)];
                myStates.Remove(state);
            }

            if (count == 0 & !end)
            {
                state = endingState;
                end = true;
            }
            updateStatistics = true;
        }
        storyText.text = state.GetStateStory();
        var setImage = imageFinder.GetComponent<Image>();
        setImage.sprite = nextImage;
    }


    private void ManageStatistics()
    {
        //List<State> defeatStates = new List<State>();
        //List<int> defeatValues = new List<int>();

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
                //
                defeatStates.Add(defeatConditions[i]);
                defeatValues.Add(item.Value);
                Debug.Log("INDEX: " + (i));
                //defeatConditions.Remove(defeatConditions[i]);
                //state = defeatConditions[i];
                //storyText.text = state.GetStateStory();
            }
            else if(item.Value >= 100)
            {
                //
                defeatStates.Add(defeatConditions[i+4]);
                defeatValues.Add(item.Value);
                Debug.Log("INDEX: " + (i + 4));
                //defeatConditions.Remove(defeatConditions[i+3]);
                //state = defeatConditions[i+3];
                //storyText.text = state.GetStateStory();
            }
            i++;
        }
        statNameText.text = strName;
        statValueText.text = strValue;
        strName = strValue = "";
        ChooseDefeatCondition(defeatStates, defeatValues);
        updateStatistics = false;
    }

    private void ChooseDefeatCondition(List<State> states, List<int> values)
    {
        if(states.Count == 0)
        {
            return;
        }

        int max = Mathf.Abs(50 - values[0]);
        int index = 0;

        for(int i=1; i<values.Count; i++)
        {
            if (Mathf.Abs(50 - values[i]) > max)
            {
                max = values[i];
                index = i;
            }
        }
        Debug.Log("WARTOSC: " + max);
        state = states[index];
        storyText.text = state.GetStateStory();
    }
    
    private void SetFlagNextState(bool flag)
    {
        flagNextState = flag;
    }

    public State GetCurrentState()
    {
        return state;
    }
}
