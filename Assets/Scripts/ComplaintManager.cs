using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class ComplaintManager : MonoBehaviour
{
    public GameObject Bubble;

    public struct Complaint
    {
        public Plaintiff plaintiff;
        public Reasons reason;

        public Complaint(Plaintiff p, Reasons r)
        {
            plaintiff = p;
            reason = r;
        }
    }

    private TMPro.TextMeshProUGUI complaintDisplay;
    Dictionary<Reasons, int> complaintValues;
    Queue<string> ComplaintText;
    public List<Complaint> complaints;
    public bool isShowing;
    Dictionary<Reasons, List<string>> answers;
    // Start is called before the first frame update
    void Start()
    {
        complaints = new List<Complaint>();
        complaintValues = new Dictionary<Reasons, int>();
        complaintValues.Add(Reasons.Designated_driver_driving, 2);
        complaintValues.Add(Reasons.Unsafe_sex, 2);
        complaintValues.Add(Reasons.Designated_driver_keys_taken_early, 1);
        complaintValues.Add(Reasons.Puke, 1);
        complaintValues.Add(Reasons.Kickout, 1);
        complaintValues.Add(Reasons.Minor, 1);
        complaintValues.Add(Reasons.Noise, 1);

        complaintDisplay = GameObject.Find("ComplaintDisplay").GetComponent<TMPro.TextMeshProUGUI>();
        complaintDisplay.text = $"complaints / 00";
        isShowing = false;
        ComplaintText = new Queue<string>();
        answers = new Dictionary<Reasons, List<string>>();
        answers.Add(Reasons.Kickout, new List<string>() { "Yo I got kicked for no reason!" });
        answers.Add(Reasons.Unsafe_sex, new List<string>() {"Wait…. I forgot my condom at home.", "Get out of the shed!", "Uh oh! Herpes alert!", "Perverts!", "We don’t want party babies!"});
        answers.Add(Reasons.Designated_driver_driving, new List<string>() {"You seriously gonna let them drive like that?", "They’re drunk driving!?", "Why’d you let them drive like that!?", "They can barely walk up right!?", "I saw them at the bar- they can’t drive!"});
        answers.Add(Reasons.Designated_driver_keys_taken_early, new List<string>() {"But I’m a responsible driver!", "My keys! I wasn't planning to drink!", "Don’t you trust me?"});
        answers.Add(Reasons.Minor, new List<string>() {"You let my 13 year old drink???", "Y’all ‘bout to turn my daughter into a hoe!", "MY SON SHOULDN’T BE EXPOSED TO THIS DEGENERACY", "This isn’t a day care!"});
        answers.Add(Reasons.Noise, new List<string>() {"I’m working tomorrow! Freakin’ kids…", "Shut up!!!", "Normally I don’t sleep, but tonight I was actually trying too.", "I hate hearing the noise of a party I wasn’t invited too.", "I’m too old for this crappy music.", "Shut up you millennials!", "I need to sleep! I have an exam tomorrow!"});
        answers.Add(Reasons.Puke, new List<string>() {"That’s disgusting!", "I wanna lick it… never mind.", "I can see my own breakfast there…", "So unsanitary.", "I’m gonna be sick!", });
    }

    private void Update()
    {
        
        if (ComplaintText.Count != 0 && !isShowing)
        {
            //Spawn complaint bubble
            SummonBubble(ComplaintText.Dequeue());
            isShowing = true;
        } 
    }

    public enum Reasons
    {
        Kickout,
        Unsafe_sex,
        Consent,
        Designated_driver_driving,
        Designated_driver_keys_taken_early,
        Minor,
        Dirty_house,
        Noise,
        Puke
    }

    public void AddComplaint(Complaint complaint)
    {
        complaints.Add(complaint);
        complaintDisplay.text = $"complaints / {TotalValue():00}";

        List<string> complaintAnswers;
        if (answers.TryGetValue(complaint.reason, out complaintAnswers))
        {
            ComplaintText.Enqueue(complaintAnswers[Random.Range(0, complaintAnswers.Count)]);
        }
        else
        {
            ComplaintText.Enqueue(complaint.reason.ToString().Replace('_', ' '));
        }
    }

    public int TotalValue()
    {
        int total = 0;
        int value;
        foreach (var complaint in complaints)
            if (complaintValues.TryGetValue(complaint.reason, out value))
                total += value;
        return total;
    }

    void SummonBubble(string text)
    {
        GameObject obj = Instantiate(Bubble, new Vector3(7.3f, -3.7f, 0), Quaternion.identity);
        obj.GetComponent<GuestBubble>().SetupBubble(text);
    }
}
