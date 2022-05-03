using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private SO_IntBase _difficulty;
    [SerializeField] private SO_IntBase_resetting _score;

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Escape)
            {
                Application.Quit();
                Debug.Log("Quit");
            }
        }
    }
    
    public void SetDifficulty(int index)
    {
        index = index < 0 ? 0 : index;
        index = index > 2 ? 2 : index;

        _difficulty.Value = index;

        LoadGameLevel();
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
    }

    public void LoadFinishLevel()
    {
        SceneManager.LoadScene("Results", LoadSceneMode.Single);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
        _score.Value = 0;
    }
}
