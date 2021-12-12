using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateStage : MonoBehaviour
{
    public GameObject _stagePrefab;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the stage
        GameObject _instanceStage = (GameObject)Instantiate(_stagePrefab);
        _instanceStage.transform.position = this.transform.position;
        _instanceStage.transform.parent = this.transform;
        _instanceStage.name = "Stage";
        _instanceStage.transform.localScale = new Vector3(61, 5, 61);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
