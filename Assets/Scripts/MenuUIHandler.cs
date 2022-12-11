using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{

    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] TextMeshProUGUI _highScore;
    string _highScoreName;
    int _highScorePoints;

    void Start()
    {
        _highScoreName = GameManager.Instance.HighScoreName;
        _highScorePoints = GameManager.Instance.HighScorePoints;
        _highScore.text = "Best Score: " + _highScoreName + ": " + _highScorePoints;
    }

    public void LoadMain()

    {
        GameManager.Instance.PlayerName = _nameInput.text;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
