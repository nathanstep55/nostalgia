using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public GameManager gm;
  public IDictionary<KeyCode, int> keydict = new Dictionary<KeyCode, int>(){
      {KeyCode.Q, 0},
      {KeyCode.W, 1},
      {KeyCode.E, 2},
      {KeyCode.R, 3},
      {KeyCode.T, 4},
      {KeyCode.Y, 5},
      {KeyCode.U, 6},
      {KeyCode.I, 7},
      {KeyCode.O, 8},
      {KeyCode.P, 9}
  };

  public bool midimode = true;
  public MidiChannel midichannel = MidiChannel.Ch1;
  public int[] midikeyrange = {24, 72};

  public int startwindow = -1;
  public int endwindow = 2;
  
	void Start ()
  {
		
	}
	
	void Update ()
  {
    if (!gm.audioManager.source.isPlaying)
      return;
    
    if (!midimode)
      foreach (KeyCode key in keydict.Keys)
        if (Input.GetKeyDown(key))
        {
          Tap(keydict[key]);
          //Light(keydict[key]);
        }

    else
    {
      for (int i = midikeyrange[0]; i <= midikeyrange[1]; i++)
        if (MidiMaster.GetKeyDown(midichannel, i))
        {
          Tap(i-midikeyrange[0]);
          //Light(i-startwindow);
        }
    }
      
	}

// osu playfield size is 512x384, code does *3/64 so 24x18
  void Tap(int x)
  {
    Note n = gm.noteManager.noteList[0];
    if (n != null)
    {
      // Debug.Log(n.x);
      // Debug.Log((double)x / (double)(keydict.Count-1) * 24.0);
      if (!midimode)
      {
        if (n.x >= (double)(x+startwindow) / (double)(keydict.Count-1) * 24.0 && n.x < (double)(x+endwindow) / (double)(keydict.Count-1) * 24.0)
          n.Tap(gm.audioManager.position);
      }
      else if (n.x >= (double)(x+startwindow) / (double)(midikeyrange[1]-midikeyrange[0]-1) * 24.0 && n.x < (double)(x+endwindow) / (double)(midikeyrange[1]-midikeyrange[0]-1) * 24.0)
        n.Tap(gm.audioManager.position);
    }
  }
  void Light(int x)
  {
    return;
    //setLight((double)(x+startwindow) / (double)(keydict.Count-1) * 24.0, (double)(x+endwindow) / (double)(keydict.Count-1) * 24.0)
  }
}
