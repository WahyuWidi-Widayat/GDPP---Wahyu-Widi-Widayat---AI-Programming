using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PickableManager : MonoBehaviour
{
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
        Debug.Log("Total Pickable Objects: " + _pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        Debug.Log("Remaining Pickable Objects: " + _pickableList.Count);
        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
            // You can trigger any event here, like ending the level or spawning new items
        }
    }
    
}
