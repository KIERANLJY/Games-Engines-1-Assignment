using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCircleCubes : MonoBehaviour
{
    public GameObject _cubePrefab;
    GameObject[] _circleCubes = new GameObject[128];
    public float _maxScale = 50;
    public float _rotateSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        float _theta = (2.0f * Mathf.PI) / (float)128;
        for (int i = 0; i < 128; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "CircleCube" + i;
            float _angle = _theta * i;
            _instanceCube.transform.position = new Vector3(Mathf.Sin(_angle) * 30, 0, Mathf.Cos(_angle) * 30);
            _circleCubes[i] = _instanceCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 64; i ++)
        {
            if (_circleCubes[i] != null && _circleCubes[i + 64] != null)
            {
                _circleCubes[i].transform.localScale = new Vector3(0.2f, (Audio._samples[i] * _maxScale) + 0.1f, 0.2f);
                _circleCubes[i + 64].transform.localScale = new Vector3(0.2f, (Audio._samples[i] * _maxScale) + 0.1f, 0.2f);
                _circleCubes[i].transform.RotateAround(this.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
                _circleCubes[i + 64].transform.RotateAround(this.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
            }
        }
    }
}
