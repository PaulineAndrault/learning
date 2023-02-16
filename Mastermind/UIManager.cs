using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField InputField;
    public AudioClip ErrorClip;
    public AudioClip TurnEnds;
    public GameObject PlayerAnswerContainer;
    private TMP_Text[] _playerAnswerTexts;
    public GameObject GameFeedbackContainer;
    private TMP_Text[] _gameFeedbackTexts;

    #region Singleton Pattern
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    private void Start()
    {
        _playerAnswerTexts = PlayerAnswerContainer.GetComponentsInChildren<TMP_Text>();
        _gameFeedbackTexts = GameFeedbackContainer.GetComponentsInChildren<TMP_Text>();
    }

    public void StartButton()
    {
        // Game Manager is updated
        GameManager.Instance.StartGame();
        GameManager.Instance.State = GameManager.GameState.PlayerTurn;

        InputField.characterLimit = GameManager.Instance.AnswerLenght;
        // Game Panel appears (button method)
        // Start Panel disappears (button method)

        // Erase previous game data
        foreach (TMP_Text text in _playerAnswerTexts)
            text.text = "";
        foreach (TMP_Text text in _gameFeedbackTexts)
            text.text = "";
    }

    public void SendButton()
    {
        // Validate input format
        if (InputField.text.Length != GameManager.Instance.AnswerLenght)
        {
            ErrorInput();
            return;
        }

        // Validate Answer
        WritePlayerAnswer(InputField.text);
        GameManager.Instance.VerifyAnswer(InputField.text.ToUpper());
    }

    private void WritePlayerAnswer(string playerAnswer)
    {
        _playerAnswerTexts[GameManager.Instance.TurnIndex].text = playerAnswer.ToUpper();
    }

    public void WriteGameFeedback(int nbCorrect, int nbMisplaced, int nbError)
    {
        _gameFeedbackTexts[GameManager.Instance.TurnIndex].text = $"<color=green>{nbCorrect}</color> <color=orange>{nbMisplaced}</color> <color=red>{nbError}</color>";
        AudioManager.Instance.PlaySound(TurnEnds);
    }

    private void ErrorInput()
    {
        AudioManager.Instance.PlaySound(ErrorClip);
    }
}
