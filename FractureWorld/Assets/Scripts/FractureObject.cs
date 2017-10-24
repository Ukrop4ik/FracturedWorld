using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureObject : MonoBehaviour {

    [SerializeField]
    private GameObject _originalObj;
    [SerializeField]
    private GameObject _fractureObj;
    [SerializeField]
    private List<GameObject> _fractures = new List<GameObject>();
    [SerializeField]
    private List<string> _tagstoActivate = new List<string>();
    [SerializeField]
    private float _fracturesMass;
    [SerializeField]
    private List<FractureObject> _dependFracture = new List<FractureObject>();
    [SerializeField]
    private bool isDestroy = false;

    public void DestroyFracture()
    {
        Invoke("D", 0.2f);
    }
    private void D()
    {
        isDestroy = true;
    }
	void Start () {

        for(int i =0; i < _fractureObj.transform.childCount; i++)
        {
            _fractures.Add(_fractureObj.transform.GetChild(i).gameObject);
            _fractureObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().mass = _fracturesMass;
        }
        _fractureObj.SetActive(false);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (isDestroy)
        {
            _originalObj.SetActive(false);
            _fractureObj.SetActive(true);

            if (_dependFracture.Count > 0)
            {
                foreach (FractureObject fr in _dependFracture)
                {
                    fr.DestroyFracture();
                }
            }
            this.enabled = false;
            isDestroy = false;
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        foreach(string s in _tagstoActivate)
        {
            if(s == other.gameObject.tag)
                isDestroy = true;
        }
    }
}
