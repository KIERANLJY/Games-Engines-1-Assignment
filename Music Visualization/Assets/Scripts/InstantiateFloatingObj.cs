using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFloatingObj : MonoBehaviour
{
    public GameObject _bodySpherePrefab;
    public GameObject _circleSpherePrefab;
    GameObject _bodySphere;
    GameObject[] _circleSpheres = new GameObject[8];
    public float _rotateSpeed;
    Material[] _material = new Material[8];


    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the sphere in the middle
        _bodySphere = (GameObject)Instantiate(_bodySpherePrefab);
        _bodySphere.transform.position = this.transform.position;
        _bodySphere.transform.parent = this.transform;
        _bodySphere.name = "BodySphere";

        // Instantiate 8 spheres in a circle around the middle sphere
        float _theta = (2.0f * Mathf.PI) / (float)8;
        for (int i = 0; i < 8; i ++)
        {
            GameObject _instanceSphere = (GameObject)Instantiate(_circleSpherePrefab);
            _instanceSphere.transform.position = this.transform.position;
            _instanceSphere.transform.parent = this.transform;
            _instanceSphere.name = "CircleSphere" + i;
            // Calculate positions of spheres by their angles
            float _angle = _theta * i;
            _instanceSphere.transform.position += new Vector3(Mathf.Sin(_angle) * 8, Mathf.Cos(_angle) * 8, 0);
            _circleSpheres[i] = _instanceSphere;

            // Get materials of the spheres
            _material[i] = _circleSpheres[i].GetComponent<MeshRenderer>().materials[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Change scale of the middle sphere according to amplitude of the audio source
        _bodySphere.transform.localScale = new Vector3((Audio._ratioAmplitudeBuffer + 1) * 5, (Audio._ratioAmplitudeBuffer + 1) * 5, (Audio._ratioAmplitudeBuffer + 1) * 5);
        
        // Make the spheres in the circle move according to amplitude
        for (int i = 0; i < 8; i ++)
        {
            if (_circleSpheres[i] != null)
            {
                // Change scale of spheres in the circle according to amplitude
                _circleSpheres[i].transform.localScale = new Vector3((Audio._ratioAmplitudeBuffer + 1) * 2, (Audio._ratioAmplitudeBuffer + 1) * 2, (Audio._ratioAmplitudeBuffer + 1) * 2);
                // Rotate the spheres around the sphere in the middle
                _circleSpheres[i].transform.RotateAround(this.transform.position, Vector3.forward, _rotateSpeed * Time.deltaTime);
                // Change color of the spheres according to amplitude
                Color _color = new Color(0.2f + 0.8f * Audio._ratioAmplitudeBuffer, 0, 0.4f + 0.6f * Audio._ratioAmplitudeBuffer);
                _material[i].SetColor("_EmissionColor", _color);
            }
        }   
    }
}
