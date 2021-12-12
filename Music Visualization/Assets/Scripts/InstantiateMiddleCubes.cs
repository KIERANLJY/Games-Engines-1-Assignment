using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMiddleCubes : MonoBehaviour
{
    public GameObject _cubePrefab;
    GameObject[] _middleCubes = new GameObject[8];
    Material[] _material = new Material[8];

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate 8 cubes in the middle
        for (int i = 0; i < 8; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "MiddleCube" + i;
            // Calculate positions of the cubes
            _instanceCube.transform.position = new Vector3((i * 3) - 10.5f, 0, -20);
            _middleCubes[i] = _instanceCube;

            // Get materials of the cubes
            _material[i] = _middleCubes[i].GetComponent<MeshRenderer>().materials[0];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make the cubes move according to frequency bands of the audio source
        for (int i = 0; i < 8; i ++)
        {
            if (_middleCubes[i] != null)
            {
                // Change heights of the cubes according to frequency bands
                _middleCubes[i].transform.localScale = new Vector3(2, (Audio._bandsBuffer[i] * 30), 2);
                // Change color of the cubes according to frequency bands
                Color _color = new Color(0, Audio._ratioBandsBuffer[i], 255);
                _material[i].SetColor("_EmissionColor", _color);
            }
        }
    }
}
