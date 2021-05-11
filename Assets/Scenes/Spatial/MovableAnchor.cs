using Microsoft.MixedReality.Toolkit.Experimental.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
using Microsoft.MixedReality.Toolkit.UI;

/// <summary>
/// Code from https://github.com/anders-lundgren/mrtk-world-anchors
/// </summary>
/// 

[RequireComponent(typeof(ObjectManipulator))]
[RequireComponent(typeof(WorldAnchor))]
public class MovableAnchor : MonoBehaviour
{
    public WorldAnchorManager worldAnchorManager;
    bool awaitingStore = true;
    WorldAnchorStore store = null;
    bool savedRoot;
    string[] anchors;
    WorldAnchor anchor;

    void Awake()
    {
        Debug.Log(name + " loading anchor store");
        WorldAnchorStore.GetAsync(StoreLoaded);
    }

    // Add events on ObjectManipulator
    ObjectManipulator manipulator;
    private void Start()
    {
        manipulator = GetComponent<ObjectManipulator>();
        manipulator.OnManipulationStarted.AddListener((data) => OnManipulationStarted());
        manipulator.OnManipulationEnded.AddListener((data) => OnManipulationEnded());
    }

    private void StoreLoaded(WorldAnchorStore store)
    {
        this.store = store;
        awaitingStore = false;
        Debug.Log(name + ": Store loaded.");

        Debug.Log("IDs: " + store.GetAllIds().Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!awaitingStore)
        {
            awaitingStore = true;
            store.Load(name, gameObject);

            UpdateAnchor();
        }
    }

    void UpdateAnchor()
    {
        anchor = GetComponent<WorldAnchor>();
        store.Delete(name);
        store.Save(name, anchor);
    }

    public void OnManipulationEnded()
    {
        worldAnchorManager.AttachAnchor(gameObject, name);
        UpdateAnchor();
    }
    public void OnManipulationStarted()
    {
        worldAnchorManager.RemoveAnchor(gameObject);
    }
}