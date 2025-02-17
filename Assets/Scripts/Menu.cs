using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_InputField NameInput;
    public void StartGame()
    {
        ScoreManager.Instance.currentPlayer = NameInput.text;
        SceneManager.LoadScene("main");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
