using UnityEngine;


[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] string storyText;
    [TextArea(5, 7)] [SerializeField] string[] choicesText;
    [SerializeField] public State[] nextStates;
    public Sprite storyImage;

    public DataOfStats[] dataOfStats;

    public bool spaceIsActive;
    public bool introduction;
    
    [System.Serializable]
    public class DataOfStats
    {
        public string nameOfStat;
        public int valueOfStat;
    }


    public string GetStateStory()
    {
        return storyText;
    }


    public State[] GetNextStates()
    {
        return nextStates;
    }


    public Sprite GetStateImage()
    {
        return storyImage;
    }


    public DataOfStats[] GetImpactOnStats()
    {
        return dataOfStats;
    }

    public bool GetSpaceIsActive()
    {
        return spaceIsActive;
    }

    public int GetNumberOfChoices()
    {
        return choicesText.Length;
    }

    public bool GetIntroductionVar()
    {
        return introduction;
    }
}
