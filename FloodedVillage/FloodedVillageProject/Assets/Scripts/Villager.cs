using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Villager : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    Transform _transform;
    SpriteRenderer _renderer;

    Tilemap _map;
    Vector3Int _coordinate;
    
    [SerializeField] TileBase _water;
    [SerializeField] Sprite _drownedVillager;

    void Start()
    {
        _map = FindObjectOfType<Tilemap>();
        if(_levelManager == null)
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }
        _transform = transform;
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _coordinate = _map.WorldToCell(_transform.position);
    }

    void Update()
    {
        TileBase ground = _map.GetTile(_coordinate);
        
        if (ground == _water && _renderer.sprite != _drownedVillager)
        {
            _renderer.sprite = _drownedVillager;
            _levelManager.Failure("Oh non ! Un villageois s'est noyé !");
        }
    }
}
