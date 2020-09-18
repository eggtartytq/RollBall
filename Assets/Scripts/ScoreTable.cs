using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Input;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //private List<ScoreEntry> PlayerList;
    private List<Transform> PlayerTransformList;
    [System.Serializable]
    private class ScoreEntry
    {
        public float score;
        public string name;
    }
    private class HighScores
    {
        public List<ScoreEntry> PlayerList;
    }

    private void Awake()
    {
        entryContainer = transform.Find("ItemGroup");
        entryTemplate = entryContainer.Find("ScoreModel");

        entryTemplate.gameObject.SetActive(false);

       // AddPlayerList(20, "ytq");
        
       /* 
         PlayerList = new List<ScoreEntry>()
         {
             new ScoreEntry{ score = 23.5F, name = "YTQ" },
             new ScoreEntry{ score = 24.5F, name = "YAQ" },
             new ScoreEntry{ score = 25.5F, name = "YSQ" },
             new ScoreEntry{ score = 26.5F, name = "YDQ" },
             new ScoreEntry{ score = 27.5F, name = "YFQ" },
             new ScoreEntry{ score = 28.5F, name = "YGQ" },
             new ScoreEntry{ score = 20.5F, name = "YHQ" },
             new ScoreEntry{ score = 22.5F, name = "YJQ" },
             new ScoreEntry{ score = 21.5F, name = "YKQ" },
             new ScoreEntry{ score = 23.4F, name = "YLQ" },
         };
         */
         
        
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        

        for (int i = 0; i < highscores.PlayerList.Count; i++)
        {
            for(int j = i + 1; j < highscores.PlayerList.Count; j++)
            {
                if(highscores.PlayerList[j].score < highscores.PlayerList[i].score)
                {
                    ScoreEntry tmp = highscores.PlayerList[i];
                    highscores.PlayerList[i] = highscores.PlayerList[j];
                    highscores.PlayerList[j] = tmp;
                }
            }
        }
        /*
        //if the num of the list is bigger than 11, delete last one
        while(highscores.PlayerList.Count >= 10)
        {
            highscores.PlayerList.RemoveAt(10);
        }
        */
        PlayerTransformList = new List<Transform>();
        
        //because i still can't delete the extra data, so just output 10
        int totalOutput;
        if(highscores.PlayerList.Count >= 10)
        {
            totalOutput = 10;
        }
        else
        {
            totalOutput = highscores.PlayerList.Count;
        }

        for(int i = 0; i < totalOutput; i++)
        {
            CreateSocreBoard(highscores.PlayerList[i], entryContainer, PlayerTransformList);
        }

        /*
        HighScores highscores = new HighScores { PlayerList = PlayerList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        */
       
    }

    private void CreateSocreBoard(ScoreEntry Players, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectransform = entryTransform.GetComponent<RectTransform>();
        entryRectransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        entryTransform.Find("RankText").GetComponent<Text>().text = rank.ToString();

        string name =Players.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        float score = Players.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        transformList.Add(entryTransform);
    }
    
    private void AddPlayerList(float score, string name)
    {
        ScoreEntry scoreEntry = new ScoreEntry { score = score, name = name };

        //load and save score
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        //add new entry to highscores
        highscores.PlayerList.Add(scoreEntry);

        //save and update
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    
}
