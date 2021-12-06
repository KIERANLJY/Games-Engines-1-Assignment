using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFloatingCubes : MonoBehaviour
{
    public GameObject _floatingCubePrefab;
    GameObject[] _floatingCubes = new GameObject[128];
    // Start is called before the first frame update
    void Start()
    {
        float _theta = (2.0f * Mathf.PI) / (float)128;
        for (int i = 0; i < 128; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_floatingCubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "FloatingCube" + i;
            float _angle = _theta * i;
            _instanceCube.transform.position = new Vector3(Mathf.Sin(_angle) * 29, 0, Mathf.Cos(_angle) * 29);
            _instanceCube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            _floatingCubes[i] = _instanceCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 64; i ++)
        {
            if (_floatingCubes[i] != null && _floatingCubes[i + 64] != null)
            {
                _floatingCubes[i].transform.position = new Vector3(_floatingCubes[i].transform.position.x, (Audio._samples[i] * 50), _floatingCubes[i].transform.position.z);
                _floatingCubes[i + 64].transform.position = new Vector3(_floatingCubes[i + 64].transform.position.x, (Audio._samples[i] * 50), _floatingCubes[i + 64].transform.position.z);
            }
        }
    }
}
