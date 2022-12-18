using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAudio : MonoBehaviour
{
	// Start is called before the first frame update
	public static ManageAudio instance;
	void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    public void M_Sound()
	{
		FindObjectOfType<AudioManager>().MuteSound("click");
		FindObjectOfType<AudioManager>().MuteSound("winning");
		FindObjectOfType<AudioManager>().MuteSound("ather_ball");
		FindObjectOfType<AudioManager>().MuteSound("hit_the_wall");
		FindObjectOfType<AudioManager>().MuteSound("active_shield");
		FindObjectOfType<AudioManager>().MuteSound("cristal_win");
		FindObjectOfType<AudioManager>().MuteSound("game_over");
		FindObjectOfType<AudioManager>().MuteSound("pop_box");
		FindObjectOfType<AudioManager>().MuteSound("congrats_endles");
		FindObjectOfType<AudioManager>().MuteSound("soun_of_failed");
	}
	public void M_Music()
	{
		FindObjectOfType<AudioManager>().MuteSound("background");
	}

}
