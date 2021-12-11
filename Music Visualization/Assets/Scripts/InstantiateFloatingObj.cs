using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFloatingObj : MonoBehaviour
{
    public GameObject _bodySpherePrefab;
    public GameObject _circleSpherePrefab;
    GameObject[] _floatingCubes = new GameObject[8];


    // Start is called before the first frame update
    void Start()
    {
        GameObject _instanceStage = (GameObject)Instantiate(_bodySpherePrefab);
        _instanceStage.transform.position = this.transform.position;
        _instanceStage.transform.parent = this.transform;
        _instanceStage.name = "FloatingBody";

        float _theta = (2.0f * Mathf.PI) / (float)8;
        for (int i = 0; i < 8; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_circleSpherePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "FloatingCube" + i;
            float _angle = _theta * i;
            _instanceCube.transform.position = new Vector3(_instanceCube.transform.position.x + Mathf.Sin(_angle) * 5, _instanceCube.transform.position.y + Mathf.Cos(_angle) * 5, _instanceCube.transform.position.z);
            _floatingCubes[i] = _instanceCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
