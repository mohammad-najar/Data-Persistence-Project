using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public TMP_InputField enterName;
    public TMP_Text playerName;

    string path;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadName();
    }

    private void Start()
    {
        path = Application.persistentDataPath + "/savefile.json";
        LoadName();
    }

    public void StartNew()
    {
        if (!string.IsNullOrEmpty(enterName.text))
        {
            SaveName(enterName.text);
        }

        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    void SaveName(string name)
    {
        SaveData data = new SaveData();
        data.playerName = name;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    void LoadName()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName.text = "Player: " + data.playerName;
        }
    }

    public void Exit()
    {

        //MenuManager.Instance.SaveName();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif

    }
}
