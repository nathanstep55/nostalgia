using System.Collections;
using System.Collections.Generic;
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

  public int startwindow = -1;
  public int endwindow = 2;
  
	void Start ()
  {
		
	}
	
	void Update ()
  {
    if (!gm.audioManager.source.isPlaying)
      return;
    
    foreach (KeyCode key in keydict.Keys)
    {
      if (Input.GetKeyDown(key))
        Tap(keydict[key]);
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
      if (n.x >= (double)(x+startwindow) / (double)(keydict.Count-1) * 24.0 && n.x < (double)(x+endwindow) / (double)(keydict.Count-1) * 24.0)
        n.Tap(gm.audioManager.position);
    }
  }
}
