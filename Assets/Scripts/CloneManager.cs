using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloneManager : MonoBehaviour
{
    public List<Clone> clones;
    public Clone currentClone;
    public int maxClones = 1;
    public AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        clones = FindObjectsOfType<Clone>().ToList();
        currentClone = clones.FirstOrDefault(c => c.controlled);
    }

    // Update is called once per frame
    void Update()
    {
        if(clones.Count < 1) return;
        currentClone = clones.FirstOrDefault(c => c.controlled);

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentClone = clones.FirstOrDefault(c => c.controlled);
            bool found = false;
            clones.ForEach(c => c.controlled = false);
            foreach (var clone in clones)
            {
                Vector3 heading = clone.transform.position - currentClone.transform.position;
		        if (AngleDir(transform.forward, heading, transform.up) == 1)
                {
                    clone.controlled = true;
                    clone.rb.velocity = Vector2.zero;
                    found = true;
                    break;
                }
            }
            clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
            if (!found) clones.Any(c=> c.controlled = true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentClone = clones.FirstOrDefault(c => c.controlled);
            bool found = false;
            clones.ForEach(c => c.controlled = false);
            foreach (var clone in clones)
            {
                Vector3 heading = clone.transform.position - currentClone.transform.position;
		        if (AngleDir(transform.forward, heading, transform.up) == -1)
                {
                    clone.controlled = true;
                    clone.rb.velocity = Vector2.zero;
                    found = true;
                    break;
                }
            }
            clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
            if( !found) clones.Any(c=> c.controlled = true);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !(clones.Count <= 1))
        {   
            var clone = clones.Aggregate((c1,c2) => c1.number > c2.number ? c1 : c2);
            clones.Remove(clone);
            Destroy(clone.gameObject);
            if (clones.Count == 1) clones[0].controlled = true;
        }
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) 
    {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
    }

    public void AddClone(GameObject newClone)
    {
        var clone = newClone.GetComponent<Clone>();
        clone.controlled = false;
        clones.Add(clone);
        clone.name = $"Clone {clones.Count - 1}";
        clone.number = clones.Count - 1;
        clone.text.text = (clones.Count - 1).ToString();
        clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
    }

    public void RemoveClone(GameObject oldClone)
    {
        var clone = oldClone.GetComponent<Clone>();
        AudioSource.PlayClipAtPoint(death, clone.transform.position);  
        if(clone.controlled && clones.Count > 1)
        {
           foreach(var c in clones)
            {
                if(c != clone)
                {
                    c.controlled = true;
                    break;
                }
            }
        }
        clones.Remove(clone);
        if(clones.Count > 1)
        {
            clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
        }
    }

    public void RemoveClone(Clone clone)
    {
        AudioSource.PlayClipAtPoint(death, clone.transform.position);  
        if(clone.controlled && clones.Count > 1)
        {
           foreach(var c in clones)
            {
                if(c != clone)
                {
                    c.controlled = true;
                    break;
                }
            }
        }
        clones.Remove(clone);
        if(clones.Count > 1)
        {
            clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
        }
    }

    public void ChangeLevel(Level level)
    {
        maxClones = level.maxClones;
        GameManager.currentLevel = level;
        clones.RemoveAll(c=>!c.controlled);
        foreach(var clone in FindObjectsOfType<Clone>())
        {
            if(!clone.controlled) Destroy(clone.gameObject);
        }
    }

    public static int roundUp(int numToRound, int multiple)
    {
        if (multiple == 0)
            return numToRound;

        int remainder = numToRound % multiple;
        if (remainder == 0)
            return numToRound;

        return numToRound + multiple - remainder;
    }
}
