using UnityEngine;


[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] string storyText;
    [SerializeField] public State[] nextStates;
    public Sprite storyImage;

    public DataOfStats[] dataOfStats;

    
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
}
