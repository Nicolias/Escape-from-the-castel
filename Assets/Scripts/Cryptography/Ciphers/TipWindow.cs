using Cryptography.Servis;
using System.Collections.Generic;
using UnityEngine;

public class TipWindow : MonoBehaviour
{
    [SerializeField] private GameObject _self;
    [SerializeField] private List<Initializable> _initializableObjects;

    public void Initialize()
    {
        _initializableObjects.ForEach(initializableObject => initializableObject.Initialize());
    }

    public void Open()
    {
        _self.SetActive(true);
    }

    public void Close()
    {
        _self.SetActive(false);        
    }
}
