using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    public GameObject foodLayer;
    public Animator ownerAnimator;
    public Animator UIAnimatorBed;
    public Animator FoodAnim;
    public Animator dayNight;

    public GameObject dayMan;
    public DayManager dM;
    public GameObject blink;
    public Rigidbody2D toyGO;


    public AudioSource purrSource;
    public AudioClip purr;
	public AudioClip cronch;

    public SpriteRenderer tail;
	public Sprite tail1;
	public Sprite tail2;

    public bool isSwitching = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foodLayer.SetActive(false);
				purrSource.clip = cronch;
				purrSource.Play();
            }
        }

        if (other.gameObject.CompareTag("Bed"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isSwitching)
                {
                    isSwitching = true;
                    dayNight.SetTrigger("SleepTime");
                    dM.ChangeDay();
                    blink.SetActive(true);
                    StartCoroutine(WaitMachine());
                }
            }
        }

        if (other.gameObject.CompareTag("Toy"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //make toy fly
                var x = Random.Range(5, 10);
                var y = Random.Range(5, 10);
                toyGO.AddForce(new Vector2(x, y)*30f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Owner"))
        {
			if (dM.sleepLap == true) {
				isSwitching = true;
				dayNight.SetTrigger ("SleepTime");
				dM.ChangeDay ();
				blink.SetActive (true);
				StartCoroutine (WaitMachine ());
			} 
			else 
			{
				ownerAnimator.SetTrigger("EnterCuddle");
				purrSource.clip = purr;
				purrSource.Play();
				blink.SetActive(true);
			}
        }

        if (collision.gameObject.CompareTag("Bed"))
        {
            UIAnimatorBed.SetTrigger("EnterUI");
        }

        if (collision.gameObject.CompareTag("Food"))
        {
            FoodAnim.SetTrigger("EatEnter");
        }

		if (collision.gameObject.CompareTag ("NewOwner")) 
		{
			tail.sprite = tail2;
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Owner"))
        {
            ownerAnimator.SetTrigger("ExitCuddle");
            purrSource.Stop();
            blink.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Bed"))
        {
            UIAnimatorBed.SetTrigger("ExitUI");
        }

        if (collision.gameObject.CompareTag("Food"))
        {
            FoodAnim.SetTrigger("EatExit");
        }

		if (collision.gameObject.CompareTag ("NewOwner")) 
		{
			tail.sprite = tail1;
		}
    }

    private void Start()
    {
        dM = dayMan.GetComponent<DayManager>();
    }

    public IEnumerator WaitMachine()
    {
        yield return new WaitForSeconds(5.0f);
        blink.SetActive(false);
        isSwitching = false;
    }
}
