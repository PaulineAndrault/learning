using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterRules : MonoBehaviour
{
    #region AllTiles
    [SerializeField] TileBase _water;
    [SerializeField] TileBase _empty;
    [SerializeField] TileBase _nextWater;
    [SerializeField] TileBase _seed;
    [SerializeField] TileBase _crop;
    #endregion

    [SerializeField] Tilemap _tileMap;
    private Vector3Int _mapSize;
    private int _numberOfTiles;

    public bool RulesOn = true;
    private float _lastUpdate;
    [SerializeField] public float _delay;
    [SerializeField] LevelManager _levelManager;

    AudioSource _audio;
    [SerializeField] AudioClip _waterClip;

    private void Start()
    {
        _tileMap = FindObjectOfType<Tilemap>();
        _audio = GetComponent<AudioSource>();
        _mapSize = _tileMap.size;
        _numberOfTiles = _mapSize.x * _mapSize.y;
    }

    private void Update()
    {
        if(Time.time > _lastUpdate + _delay && RulesOn)
        {
            FindSandAroundWater();
        }
    }

    void FindSandAroundWater()
    {
        _lastUpdate = Time.time;
        bool _nothingChanged = true;

        for(int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                Vector3Int coordinate = new Vector3Int(i, j, 0);

                if (_tileMap.GetTile(coordinate) == _water)
                {

                    // Find sand around
                    Vector3Int left = new Vector3Int(i - 1, j, 0);
                    Vector3Int right = new Vector3Int(i + 1, j, 0);
                    Vector3Int up = new Vector3Int(i, j + 1, 0);
                    Vector3Int down = new Vector3Int(i, j - 1, 0);
                    Vector3Int[] neighbours = new Vector3Int[] { left, right, up, down };

                    foreach (Vector3Int n in neighbours)
                    {
                        if (_tileMap.GetTile(n) == _empty)
                        {
                            _tileMap.SetTile(n, _nextWater);
                            _nothingChanged = false;
                        }
                        else if (_tileMap.GetTile(n) == _seed)
                        {
                            _tileMap.SetTile(n, _crop);
                            _nothingChanged = false;
                        }
                    }
                }
            }
        }
        
        if(_nothingChanged)
        {
            RulesOn = false;
            CheckCrops();
        }
        else
        {
            TurnIntoWater();
        }
    }

    private void CheckCrops()
    {
        bool _allGood = true;

        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                Vector3Int coordinate = new Vector3Int(i, j, 0);
                if (_tileMap.GetTile(coordinate) == _seed)
                {
                    _allGood = false;
                }
            }
        }

        if (_allGood)
        {
            _levelManager.CheckSuccessConditions();
        }
        else
        {
            _levelManager.CheckFailConditions();
        }
    }

    void TurnIntoWater()
    {
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                Vector3Int coordinate = new Vector3Int(i, j, 0);

                if (_tileMap.GetTile(coordinate) == _nextWater)
                {
                    _audio.PlayOneShot(_waterClip);
                    _tileMap.SetTile(coordinate, _water);
                }
            }
        }

    }
}
