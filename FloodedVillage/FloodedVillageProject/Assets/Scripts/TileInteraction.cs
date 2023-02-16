using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInteraction : MonoBehaviour
{
    Tilemap _tileMap;
    [SerializeField] TileBase _empty;
    [SerializeField] TileBase _sand;
    [SerializeField] LevelManager _levelManager;

    [SerializeField] Texture2D _cursorShovel;
    [SerializeField] Vector2 _hotspotShovel;
    [SerializeField] Texture2D _cursorBucket;
    Vector2 _hotspotBucket;

    AudioSource _audio;
    [SerializeField] AudioClip _sandClip;


    private void Awake()
    {
        _hotspotShovel = new Vector2(_cursorShovel.width * 0.2f, _cursorShovel.height * 0.8f);
        _hotspotBucket = new Vector2(_cursorBucket.width * 0.5f, _cursorBucket.height * 0.5f);
    }

    private void Start()
    {
        _tileMap = FindObjectOfType<Tilemap>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = _tileMap.WorldToCell(mouseWorldPos);
        
        if (Input.GetKey("space"))
        {
            if (_tileMap.GetTile(coordinate) == _empty)
            {
                _levelManager.AddOneClick();
                FillWithSand(coordinate);
            }
        }
        else if(_tileMap.GetTile(coordinate) == _sand)
        {
            _levelManager.AddOneClick();
            TurnIntoEmpty(coordinate);
        }
    }

    private void OnMouseOver()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = _tileMap.WorldToCell(mouseWorldPos);

        if (Input.GetKey("space"))
        {
            ChangeCursor(_cursorBucket, _hotspotBucket);
        }
        else
        {
            ChangeCursor(_cursorShovel, _hotspotShovel);
        }
    }

    private void TurnIntoEmpty(Vector3Int pos)
    {
        _tileMap.SetTile(pos, _empty);
        _tileMap.RefreshAllTiles();
    }

    private void FillWithSand(Vector3Int pos)
    {
        _audio.PlayOneShot(_sandClip);
        _tileMap.SetTile(pos, _sand);
        _tileMap.RefreshAllTiles();
    }

    private void ChangeCursor(Texture2D cursor, Vector2 hotspot)
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }
}
