using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private int Score;
    private SingleJoystick _joy;
    private Rigidbody _rigedbody;
    [SerializeField]
    private Vector3 _targetVector;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _force;
    [SerializeField]
    private float _power;

	// Use this for initialization
	void Start ()
    {
        _joy = GameObject.Find("Joy").GetComponent<SingleJoystick>();
        _rigedbody = GetComponent<Rigidbody>();
        StartCoroutine(Speedometer());
    }

    void FixedUpdate()
    {

        //if (_playerTransform)
        //{
        //    _targetVector = _playerTransform.position - transform.position;
        //}

        //_rigedbody.AddForce(_targetVector * _force);
        //Debug.Log(_joy.GetInputDirection());
        _rigedbody.AddForce(Vector3.forward * _joy.GetInputDirection().y * _force);
        _rigedbody.AddForce(Vector3.right * _joy.GetInputDirection().x * _force);
    }

    private IEnumerator Speedometer()
    {
        Vector3 start;
        Vector3 end;

        start = transform.position;

        yield return new WaitForSeconds(0.1f);

        end = transform.position;

        _speed = Vector3.Distance(start, end) * 10f;
        _power = _speed * _rigedbody.mass;
        StartCoroutine(Speedometer());
    }

    public float GetSpeed()
    {
        return _speed;
    }
    public float GetPower()
    {
        return _power;
    }
    public int GetScore()
    {
        return Score;
    }
    public void SetScore(int value)
    {
        Score += value;
    }
}
