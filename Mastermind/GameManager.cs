using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance
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

    public GameState State;

    [Header("Settings")]
    public int AnswerLenght;
    public List<char> AuthorizedCharacters = new List<char>();

    [Header("Refs and AudioClips")]
    public AudioClip VictorySound;
    public AudioClip DefeatSound;
    public GameObject GamePanel;
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    [HideInInspector]
    public int TurnIndex;

    // Private vars
    private int _maxTries = 15;
    private string _answer;

    private void Start()
    {
        State = GameState.Start;
    }

    private void Update()
    {
        // Cheat code
        if (Input.GetKeyDown(KeyCode.Space)) 
            Debug.Log(_answer);
    }

    public void StartGame()
    {
        TurnIndex = 0;
        _answer = "";
        for (int i = 0; i < AnswerLenght; i++)
        {
            int charIndex = Random.Range(0, AuthorizedCharacters.Count);
            _answer += AuthorizedCharacters[charIndex];
        }
    }

    public void VerifyAnswer(string playerAnswer)
    {
        string answer = _answer;    // We will modify this string but we need to keep the real _answer safe

        int nbOfCorrectCharPos = 0;
        int nbOfCorrectCharIncorrectPos = 0;

        // Verify correct char+pos
        // iterate on playerAnswer, compare every char with answer. If same char : increment nbOfCorrect + remove the char from both strings.
        // this won't change the order of the remaining chars + will prevent errors during next iteration (when looking for misplaced chars)
        for (int i = playerAnswer.Length - 1; i >= 0; i--)
        {
            if(playerAnswer[i] == answer[i])
            {
                nbOfCorrectCharPos++;
                playerAnswer = playerAnswer.Remove(i, 1);
                answer = answer.Remove(i, 1);
            }
        }
        
        // Second iteration to look for misplaced chars
        for (int i = playerAnswer.Length - 1; i >= 0; i--)
        {
            if (answer.Contains(playerAnswer[i]))
            {
                nbOfCorrectCharIncorrectPos++;
                answer = answer.Remove(answer.IndexOf(playerAnswer[i]), 1);
                playerAnswer = playerAnswer.Remove(i, 1);
            }
        }

        // Write game feedback on the board
        UIManager.Instance.WriteGameFeedback(nbOfCorrectCharPos, nbOfCorrectCharIncorrectPos, AnswerLenght - nbOfCorrectCharPos - nbOfCorrectCharIncorrectPos);

        TurnIndex++;

        // Check victory and defeat conditions
        if (nbOfCorrectCharPos == AnswerLenght)
            StartCoroutine(Victory());
        else if (TurnIndex >= 15)
            StartCoroutine(Defeat());
    }

    public void EndPlayerTurn()
    {
        // to be polished
        // use the state Validation to lock player's input, add a small anim / waiting time before writing the answer, etc.
    }

    private IEnumerator Victory()
    {
        AudioManager.Instance.PlaySound(VictorySound);
        GamePanel.SetActive(false);
        yield return new WaitForSeconds(1);
        VictoryPanel.SetActive(true);
    }
    private IEnumerator Defeat()
    {
        AudioManager.Instance.PlaySound(DefeatSound);
        GamePanel.SetActive(false);
        yield return new WaitForSeconds(1);
        DefeatPanel.SetActive(true);
    }

    public enum GameState
    {
        Start,
        PlayerTurn,
        Validation,
        Victory,
        Defeat
    }
}
