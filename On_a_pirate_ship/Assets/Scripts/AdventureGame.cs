using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AdventureGame : MonoBehaviour
{
    //
    PirateStats pirate;
    string strName, strValue;
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValueText;

    bool flagNextState;
    //[SerializeField] Text textComponent;
    //[SerializeField] State startingState;

    [SerializeField] Text storyText;
    [SerializeField] State startingState;
    [SerializeField] State[] defeatConditions;

    //
    Sprite nextImage;
    //
    State state;

    public List<State> myStates;
    public List<Sprite> mySprites;

    int count;


    void Start()
    {
        pirate = new PirateStats();
        flagNextState = true;
        //
        //nextImage = state.GetStateImage();
        //
        state = startingState;
        storyText.text = state.GetStateStory();
    }


    void Update()
    {
        nextImage = state.GetStateImage();
        ManageState();
        ManageStatistics();
    }


    private void ManageState()
    {
        var nextStates = state.GetNextStates();
        //var nextImage = state.GetStateImage();
        count = myStates.Count;


        var nextStat = state.GetImpactOnStats();
        if (state.GetImpactOnStats().Length == 3 & flagNextState)
        {
            for (int i = 0; i < nextStat.Length; i++)
            {
                if ((pirate.pirateStatistics[nextStat[i].nameOfStat] + nextStat[i].valueOfStat) >= 100)
                {
                    Debug.Log("huhufhausfhas");
                }
                pirate.pirateStatistics[nextStat[i].nameOfStat] += nextStat[i].valueOfStat; //1;
                //Debug.Log(nextStat[i].strStat + " - " + nextStat[i].intStat); //odczyt danych z wlasnej struktury
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

        if (Input.GetKeyDown(KeyCode.Alpha0) & count > 0)
        {
            state = myStates[UnityEngine.Random.Range(0, count)];
            myStates.Remove(state);

            nextImage = state.GetStateImage();
            GetComponent<Image>().sprite = nextImage;
        }

        storyText.text = state.GetStateStory();
        GetComponent<Image>().sprite = nextImage;
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
                //var
                nextImage = state.GetStateImage();
                //
                GetComponent<Image>().sprite = nextImage;
            }
            if(item.Value >= 100)
            {
                state = defeatConditions[i+3];
                storyText.text = state.GetStateStory();
                //var
                nextImage = state.GetStateImage();
                //
                GetComponent<Image>().sprite = nextImage;
            }
            i++;
        }
        statNameText.text = strName;
        statValueText.text = strValue;
        strName = strValue = "";
    }
}
