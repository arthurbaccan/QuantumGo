using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizpediaManager : MonoBehaviour
{
    public List<QuizCard> quizCards = new List<QuizCard>();
    public GameObject quizCardPrefab; // Assign in inspector: the prefab for a QuizCard
    public GameObject quizpediaContent; // Assign in inspector: the parent GameObject that holds all the QuizCard instances
    public GameObject detailPanel; // Assign in inspector: the panel that shows detailed info about an object
    private EncounterManager encounterManager;
    
    
    public void Start()
    {
        encounterManager = FindAnyObjectByType<EncounterManager>();
        initializeObjspedia();
    }
    
    public void initializeObjspedia()
    {
        QuizCard newCard = null;
        for (int i = 0; i < encounterManager.objectDatabase.allQuizzes.Count; i++)
        {
            // Add cards as necessary
            GameObject quizCardPrefabClone = Instantiate(quizCardPrefab, quizpediaContent.transform, false);

            newCard = quizCardPrefabClone.GetComponent<QuizCard>();

            quizCardPrefabClone.GetComponent<Button>().onClick.AddListener(newCard.OnClick);

            quizCards.Add(newCard);

        }

        foreach (QuizData data in encounterManager.objectDatabase.allQuizzes)
        {
            if (data.id >= 0)
            {
                addToObjspedia(data, quizCards[data.id]);
            }

            if (!encounterManager.foundObjects.Contains(data))
            {
                quizCards[data.id].SetUnfound();
            }
        }
    }
    
    private void addToObjspedia(QuizData objectData, QuizCard quizCard)
    {
        quizCard.SetData(objectData);
    }
}
