using UnityEngine;


[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] string storyText; //mozna zmienic na public zamiast SerializeField
    [SerializeField] public State[] nextStates;
    public Sprite storyImage;

    //[SerializeField] string[] impactOnStats;
    public DataOfStats[] dataOfStats;


    //
    [System.Serializable]
    public class DataOfStats
    {
        public string nameOfStat;
        public int valueOfStat;
    }
    //


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


    /*public string[] GetImpactOnStats()
    {
        return impactOnStats;
    }
    */
    public DataOfStats[] GetImpactOnStats()
    {
        return dataOfStats;
    }
}
