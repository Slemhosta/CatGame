using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour {

    public int dayCount = 1;

    public GameObject tuesday;
    public GameObject wednesday;
    public GameObject thursday;
    public GameObject friday;
    public GameObject saturday;
    public GameObject sunday;
    public GameObject player;
	public GameObject endScreen;

    public GameObject ownerObj;
	public GameObject newOwnerObj;
    public GameObject lightsOff;
    public GameObject fullFood;

    public SpriteRenderer catState;

    public Sprite bodHappy;
    public Sprite bodDemand;
    public Sprite bodSad;
    public Sprite bodNervous;
    public Sprite bodVerySad;

    public AudioSource radio;
	public AudioClip track1;
    public AudioClip track2;
	public AudioClip staticTrack;
	public AudioClip track3;

	public bool sleepLap = false;

    private PlayerInteractions pL;

    // Use this for initialization
    void Start () {

        pL = player.GetComponent<PlayerInteractions>();
		
	}

    public IEnumerator WaitCalendar()
    {
        yield return new WaitForSeconds(4.0f);
    }

    public void ChangeDay ()
	{
		dayCount++;

		if (dayCount == 2) {
			StartCoroutine (WaitCalendar ());
			tuesday.SetActive (true);
			pL.foodLayer.SetActive (true);
			radio.clip = track2;
			radio.Play ();

		}

		if (dayCount == 3) {
			StartCoroutine (WaitCalendar ());
			wednesday.SetActive (true);
			catState.sprite = bodDemand;
			//Demanding meow
			pL.foodLayer.SetActive (false);
			//no owner
			lightsOff.SetActive (true);
			ownerObj.SetActive (false);
			radio.clip = staticTrack;
			radio.Play ();

		}

		if (dayCount == 4) {
			catState.sprite = bodSad;
			StartCoroutine (WaitCalendar ());
			thursday.SetActive (true);
			radio.Stop ();
		}

		if (dayCount == 5) {
			catState.sprite = bodNervous;
			StartCoroutine (WaitCalendar ());
			friday.SetActive (true);
			newOwnerObj.SetActive (true);
			lightsOff.SetActive (false);
			pL.foodLayer.SetActive (true);
			radio.clip = track3;
			radio.Play ();
		}

		if (dayCount == 6) {
			catState.sprite = bodVerySad;
			StartCoroutine (WaitCalendar ());
			saturday.SetActive (true);
			//door open
		}

		if (dayCount == 7) {
           
			StartCoroutine (WaitCalendar ());
			sunday.SetActive (true);
			ownerObj.SetActive (true);
			pL.foodLayer.SetActive (true);
			newOwnerObj.SetActive (false);
			radio.clip = track1;
			radio.Play ();

            if (pL.foodLayer.activeInHierarchy == false)
            {
                catState.sprite = bodNervous;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    sleepLap = true;
                    catState.sprite = bodHappy;
                }
                
            }

		}

		if (dayCount == 8) {
			//End of game
			endScreen.SetActive(true);
		}
	}
}