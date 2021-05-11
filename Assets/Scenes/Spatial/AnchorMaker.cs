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
public class AnchorMaker : MonoBehaviour
{
    public WorldAnchorManager worldAnchorManager;
    bool awaitingStore = true;
    WorldAnchorStore store = null;
    bool savedRoot;
    string[] anchors;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("AnchorMaker: Looking for anchors...");
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

        Debug.Log("IDs: " + store.GetAllIds().Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!awaitingStore)
        {
            Debug.Log("AnchorMaker: Store loaded.");
            awaitingStore = true;
            anchors = this.store.GetAllIds();
            Debug.Log("Found anchors: " + anchors.Length);
            for (int index = 0; index < anchors.Length; index++)
            {
                Debug.Log(anchors[index]);
            }
            LoadGame();
        }

    }

    private void LoadGame()
    {
        // Save data about holograms positioned by this world anchor
        this.savedRoot = this.store.Load(name, this.gameObject);
        if (!this.savedRoot)
        {
            // We didn't actually have the game root saved! We have to re-place our objects or start over
            Debug.Log("LoadGame: No saved anchors found");
        }
    }

    public void OnManipulationEnded()
    {
        WorldAnchor anchor = this.gameObject.GetComponent<WorldAnchor>();
        if (anchor == null)
        {
            string name = worldAnchorManager.AttachAnchor(this.gameObject);
            Debug.Log("Added anchor: " + name);
        }
        else
        {
            if (anchor.name != null)
            {
                Debug.Log("Updating anchor: " + anchor.name);
            }
        }
        SaveGame();
    }
    public void OnManipulationStarted()
    {
        if (anchors != null)
        {
            foreach (string a in anchors)
            {
                Debug.Log("Deleting anchor: " + a);
                store.Delete(a);
            }
            this.savedRoot = false;
        }
    }

    private void SaveGame()
    {
        WorldAnchor anchor = this.gameObject.GetComponent<WorldAnchor>();
        // Save data about holograms positioned by this world anchor
        if (!this.savedRoot && anchor != null) // Only Save the root once
        {
            this.savedRoot = this.store.Save(name, anchor);
            //Assert(this.savedRoot);
            Debug.Log("Saved anchor: " + this.savedRoot);
        }
        else
        {
            Debug.Log("Already saved anchor: " + this.savedRoot);
        }
    }
}