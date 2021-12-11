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
        for (int i = 0; i < 8; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "MiddleCube" + i;
            _instanceCube.transform.position = new Vector3((i * 3) - 10.5f, 0, -20);
            _middleCubes[i] = _instanceCube;

            _material[i] = _middleCubes[i].GetComponent<MeshRenderer>().materials[0];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 8; i ++)
        {
            if (_middleCubes[i] != null)
            {
                _middleCubes[i].transform.localScale = new Vector3(2, (Audio._bandsBuffer[i] * 30), 2);
                
                Color _color = new Color(0, Audio._ratioBandsBuffer[i], 255);
                _material[i].SetColor("_EmissionColor", _color);
            }
        }
    }
}
