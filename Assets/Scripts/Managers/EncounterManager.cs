using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<PhysicistData> foundPhysicists = new List<PhysicistData>(); //lista pública para vermos no inspector quais físicos foram encontrados
    public List<QuizData> foundObjects = new List<QuizData>();
    public QuestDatabase questDatabase;
    public PhysicistDatabase physicistDatabase;
    public ObjectDatabase objectDatabase;
    public QuizpediaManager objpediaManager;
    public PhyspediaManager physpediaManager;
    public SaveSystem.SaveData currentSaveData;

    public void RegisterPhysicistEncounter(PhysicistData physicistData, int foundTimes)  //método que outros scripts vão chamar para registrar um encontro
    {
        physicistData.foundTimes += foundTimes;
        if (!foundPhysicists.Contains(physicistData))
        {
            foundPhysicists.Add(physicistData);
            physpediaManager.physicistCards[physicistData.id].SetFound();
            Debug.Log($"Encontrou e registrou um novo físico: {physicistData.name}!");
        }
        else
        {
            physpediaManager.physicistCards[physicistData.id].SetFoundAgain();
            Debug.Log($"{physicistData.name} já tinha sido encontrado antes.");
        }

        bool physicistAlreadyExists = false;

        for (int i = 0; i < currentSaveData.foundPhysicistsDataPacks.Count; i++)
        {
            if (currentSaveData.foundPhysicistsDataPacks[i].id == physicistData.id)
            {
                physicistAlreadyExists = true;

                // 1. Pull out the temporary copy
                SaveSystem.PhysicistDataPack packCopy = currentSaveData.foundPhysicistsDataPacks[i];

                // 2. Modify the copy
                packCopy.foundTimes = physicistData.foundTimes;

                // 3. Shove the modified copy back into the list
                currentSaveData.foundPhysicistsDataPacks[i] = packCopy;

                SaveSystem.Save(ref currentSaveData);
                break;
            }
        }

        if (!physicistAlreadyExists)
        {
            SaveSystem.PhysicistDataPack newPack = new SaveSystem.PhysicistDataPack
            {
                id = physicistData.id,
                foundTimes = physicistData.foundTimes
            };

            currentSaveData.foundPhysicistsDataPacks.Add(newPack);
            SaveSystem.Save(ref currentSaveData);
        }

    }

    public void RegisterObjectEncounter(QuizData objectData, int foundTimes)
    {
        objectData.foundTimes += foundTimes;
        if (!foundObjects.Contains(objectData))
        {
            foundObjects.Add(objectData);
            objpediaManager.quizCards[objectData.id].SetFound();
            Debug.Log($"Encontrou e registrou um novo físico: {objectData.name}!");
        }
        else
        {
            objpediaManager.quizCards[objectData.id].SetFoundAgain();
            Debug.Log($"{objectData.name} já tinha sido encontrado antes.");
        }

        bool objectFound = false;
        for (int i = 0; i < currentSaveData.foundQuizzesDataPacks.Count; i++)
        {
            if (currentSaveData.foundQuizzesDataPacks[i].id == objectData.id)
            {
               objectFound = true;
                // 1. Pull out the temporary copy
                SaveSystem.QuizDataPack packCopy = currentSaveData.foundQuizzesDataPacks[i];

                // 2. Modify the copy
                packCopy.foundTimes = objectData.foundTimes;

                // 3. Shove the modified copy back into the list
                currentSaveData.foundQuizzesDataPacks[i] = packCopy;

                SaveSystem.Save(ref currentSaveData);
                break;
            }
        }

        if (!objectFound)
        {
            currentSaveData.foundQuizzesDataPacks.Add(new SaveSystem.QuizDataPack
            {
                id = objectData.id,
                foundTimes = objectData.foundTimes
            });

            SaveSystem.Save(ref currentSaveData);
        }
    }

    private void ResetAllQuests()
    {
        foreach (var quest in questDatabase.allQuests)
        {
            quest.questActive = false;
            quest.questCompleted = false;
        }
    }

    private void LoadSaveData()
    {
        bool loadedSuccess = SaveSystem.Load(ref currentSaveData);

        if (!loadedSuccess)
        {
            return;
        }

        physicistDatabase.allPhysicists.ForEach(physicist =>
        {
            // Find the position of the pack in the list (-1 if missing)
            int index = currentSaveData.foundPhysicistsDataPacks.FindIndex(pack => pack.id == physicist.id);

            if (index != -1)
            {
                var match = currentSaveData.foundPhysicistsDataPacks[index];
                physicist.foundTimes = match.foundTimes;
                foundPhysicists.Add(physicist);
            }
        });

        objectDatabase.allQuizzes.ForEach(obj =>
        {
            // Find the position of the pack in the list (-1 if missing)
            int index = currentSaveData.foundQuizzesDataPacks.FindIndex(pack => pack.id == obj.id);

            if (index != -1)
            {
                var match = currentSaveData.foundQuizzesDataPacks[index];
                obj.foundTimes = match.foundTimes;
                foundObjects.Add(obj);
            }
        });
    }
    
    private void Start()
    {
        currentSaveData = new SaveSystem.SaveData();
        // Explicitly initialize the lists so they aren't null
        currentSaveData.foundPhysicistsDataPacks = new List<SaveSystem.PhysicistDataPack>();
        currentSaveData.foundQuizzesDataPacks = new List<SaveSystem.QuizDataPack>();

        LoadSaveData();
        ResetAllQuests();
    }
}