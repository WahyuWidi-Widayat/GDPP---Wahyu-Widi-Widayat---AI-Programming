using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PickableManager : MonoBehaviour
{
    [SerializeField]
     private Player _player;
    [SerializeField]
     private ScoreManager _scoreManager;
    private List<Pickable> _pickableList = new List<Pickable>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPickableList();
    }

    // Update is called once per frame
    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;

        }
        _scoreManager.SetMaxScore(_pickableList.Count);
        
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (_scoreManager != null) { _scoreManager.AddScore(1); }

        if (pickable.PickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("WinScene");
             Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
            // You can trigger any event here, like ending the level or spawning new items
        }
       
        
    }
    
}
