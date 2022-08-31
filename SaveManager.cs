using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private string fileName = "savedata.mmw";

   public static SaveManager instance { get; private set; }

    private DataBase dataBase;
    private List<DataCollector> dataCollectorObjects;

    private SaveDataFile saveDataFile;

    private void Awake()
    {
        this.saveDataFile = new SaveDataFile(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.dataBase = new DataBase();
    }

    public void LoadGame()
    {
        dataCollectorObjects = FindAllDataCollectorObjects();
        this.dataBase = saveDataFile.Load();

        if (this.dataBase == null)
        {
            NewGame();
        }
        foreach (DataCollector collector in dataCollectorObjects)
        {
            collector.LoadData(dataBase);
        }
    }

    public void SaveGame()
    {
        dataCollectorObjects = FindAllDataCollectorObjects();
        foreach (DataCollector collector in dataCollectorObjects)
        {
            collector.SaveData(dataBase);
        }    
        saveDataFile.Save(dataBase);
    }

    private List<DataCollector> FindAllDataCollectorObjects()
    {
        IEnumerable<DataCollector> dataCollectorObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<DataCollector>();

        return new List<DataCollector>(dataCollectorObjects);
    }
}
