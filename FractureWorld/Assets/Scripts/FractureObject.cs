using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureObject : MonoBehaviour {
    private Player _player;
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
    private int removeIndx = 0;
    private bool Stop;
    [SerializeField]
    private float _defence;
    [SerializeField]
    private int _scoreValue;
    public void DestroyFracture()
    {
        Invoke("D", 0.2f);
    }
    private void D()
    {
        _player.SetScore(_scoreValue);
        isDestroy = true;
    }
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        for(int i =0; i < _fractureObj.transform.childCount; i++)
        {
            _fractures.Add(_fractureObj.transform.GetChild(i).gameObject);
            _fractureObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().mass = _fracturesMass;
        }
        _fractureObj.SetActive(false);

        _scoreValue += (int)_defence * 5;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (isDestroy && !Stop)
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
            isDestroy = false;
            InvokeRepeating("Remove", 0, 3f);
            Stop = true;
        }

	}

    private void Remove()
    {
        //  Destroy(this.gameObject);

        if (removeIndx < _fractures.Count)
        {
            Destroy(_fractures[removeIndx]);
            removeIndx++;
        }
        else
        {
            CancelInvoke("Remove");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(string s in _tagstoActivate)
        {
            if (s == other.gameObject.tag)
            {
                if(s == "Player")
                {
                   if(_player.GetPower() >= _defence)
                    {
                        isDestroy = true;
                        _player.SetScore(_scoreValue);
                    }
                }

            }
        }
    }
}
