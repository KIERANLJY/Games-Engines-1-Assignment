using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject _cubePrefab;
    GameObject[] _circleCubes = new GameObject[256];
    public float _maxScale;
    public float _rotateSpeed = 10;

    GameObject[] _middleCubes = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        float theta = (2.0f * Mathf.PI) / (float)256;
        for (int i = 0; i < 256; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "CircleCube" + i;
            float angle = theta * i;
            _instanceCube.transform.position = new Vector3(Mathf.Sin(angle) * 300, 0, Mathf.Cos(angle) * 300);
            _circleCubes[i] = _instanceCube;
        }

        for (int i = 0; i < 8; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "MiddleCube" + i;
            _instanceCube.transform.position = new Vector3((i * 30) - 105, 0, -200);
            _middleCubes[i] = _instanceCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 128; i ++)
        {
            if (_circleCubes[i] != null && _circleCubes[i + 128] != null)
            {
                _circleCubes[i].transform.localScale = new Vector3(2, (Audio._samples[i] * _maxScale) + 1, 2);
                _circleCubes[i + 128].transform.localScale = new Vector3(2, (Audio._samples[i] * _maxScale) + 1, 2);
                _circleCubes[i].transform.RotateAround(this.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
                _circleCubes[i + 128].transform.RotateAround(this.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
            }
        }

        for (int i = 0; i < 8; i ++)
        {
            if (_middleCubes[i] != null)
            {
                _middleCubes[i].transform.localScale = new Vector3(20, (Audio._samples[i * 16] * _maxScale), 20);
            }
        }
    }
}
