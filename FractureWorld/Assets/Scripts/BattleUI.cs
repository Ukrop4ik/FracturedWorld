using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

    private Player _player;
    [SerializeField]
    private Text _speedText;
    [SerializeField]
    private Text _powerText;

	// Use this for initialization
	void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(UpdateUI());
    }
	
    private IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(0.1f);

        _speedText.text = _player.GetSpeed().ToString("0");
        _powerText.text = _player.GetPower().ToString("0");

        StartCoroutine(UpdateUI());
    }
}
