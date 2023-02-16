using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _maxClicks;
    private int _currentClicks = 0;
    [SerializeField] TMP_Text _clicksText;
    [SerializeField] GameObject _victoryUI;
    [SerializeField] GameObject _failureUI;
    [SerializeField] TMP_Text _failureText;
    [SerializeField] WaterRules _waterRules;
    [SerializeField] GameObject _clickBlocker;
    AudioSource _audio;
    [SerializeField] AudioClip _victorySound;
    [SerializeField] AudioClip _failureSound;

    Zombie[] _zombies;
    public int _numberOfZombies;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _zombies = FindObjectsOfType<Zombie>();
        _numberOfZombies = _zombies.Length;
        RefreshText();
    }

    public void AddOneClick()
    {
        _waterRules.RulesOn = true;
        _currentClicks++;
        if (_currentClicks <= _maxClicks)
        {
            RefreshText();
        }
    }

    public void CheckFailConditions()
    {
        if (_currentClicks >= _maxClicks)
        {
            Failure("Vous n'avez plus assez de force pour continuer à creuser...");
        }
    }

    public void CheckSuccessConditions()
    {
        if(_numberOfZombies <= 0)
        {
            Victory();
        }
        else
        {
            Failure("Vous n'avez pas tué tous les zombies.");
        }
    }

    private void RefreshText()
    {
        _clicksText.text = (_maxClicks - _currentClicks).ToString();
    }

    public void Victory()
    {
        _audio.PlayOneShot(_victorySound);
        _clickBlocker.SetActive(true);
        _victoryUI.SetActive(true);
    }

    public void Failure(string s)
    {
        _waterRules.RulesOn = false;
        _audio.PlayOneShot(_failureSound);
        _clickBlocker.SetActive(true);
        _failureUI.SetActive(true);
        _failureText.text = s;
    }

}
