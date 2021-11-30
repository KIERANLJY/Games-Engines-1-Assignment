using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject _cubePrefab;
    GameObject[] _cubes = new GameObject[128];
    public float _maxScale;
    public float _rotateSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        float theta = (2.0f * Mathf.PI) / (float)128;
        for (int i = 0; i < 128; i ++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "Cube" + i;
            float angle = theta * i;
            _instanceCube.transform.position = new Vector3(Mathf.Sin(angle) * 100, 0, Mathf.Cos(angle) * 100);
            _cubes[i] = _instanceCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 64; i ++)
        {
            if (_cubes[i] != null && _cubes[i + 64] != null)
            {
                _cubes[i].transform.localScale = new Vector3(2, (Audio._samples[i] * _maxScale) + 1, 2);
                _cubes[i + 64].transform.localScale = new Vector3(2, (Audio._samples[i] * _maxScale) + 1, 2);

                _cubes[i].transform.RotateAround(this.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
                _cubes[i + 64].transform.RotateAround(this.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
            }
        }
    }
}
