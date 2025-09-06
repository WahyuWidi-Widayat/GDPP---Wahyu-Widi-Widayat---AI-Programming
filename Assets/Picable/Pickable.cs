using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType PickableType;
    public Action<Pickable> OnPicked;   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
             OnPicked(this); // Notify the manager that this item has been picked
            Destroy(gameObject); // Remove the item from the scene
           
        }
    }
  
}
