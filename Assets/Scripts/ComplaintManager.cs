using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    List<Complaint> complaints;
    // Start is called before the first frame update
    void Start()
    {
        complaints = new List<Complaint>();
        complaintValues = new Dictionary<Reasons, int>();
        complaintValues.Add(Reasons.Designated_driver_driving, 2);
        complaintValues.Add(Reasons.Unsafe_sex, 2);
        complaintValues.Add(Reasons.Consent, 2);
        complaintValues.Add(Reasons.Dirty_house, 1);
        complaintValues.Add(Reasons.Kickout, 1);
        complaintValues.Add(Reasons.Minor, 1);
        complaintValues.Add(Reasons.Noise, 1);

        complaintDisplay = GameObject.Find("ComplaintDisplay").GetComponent<TMPro.TextMeshProUGUI>();
        complaintDisplay.text = $"complaints / 00";
        Debug.Log(complaints.Count);
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
        complaintDisplay.text = $"complaints / {complaints.Count:00}";

        List<string> ComplaintText = new List<string>();
        //ComplaintText.Add("");
        switch (complaint.reason)
        {
            case Reasons.Kickout:
                ComplaintText.Add("Yo I got kicked for no reason!");
                break;
            case Reasons.Unsafe_sex:
                break;
            case Reasons.Consent:
                break;
            case Reasons.Designated_driver_driving:
                ComplaintText.Add("You seriously gonna let them leave like that?");
                ComplaintText.Add("They’re drunk driving!?");
                ComplaintText.Add("Why’d you let them drive like that!?");
                ComplaintText.Add("They can barely walk up right!?");
                ComplaintText.Add("I saw them at the bar- they can’t drive!");
                break;
            case Reasons.Designated_driver_keys_taken_early:
                ComplaintText.Add("But I’m a responsible driver!");
                ComplaintText.Add("What gives dude?");
                ComplaintText.Add("Don’t you trust me?");
                break;
            case Reasons.Minor:
                ComplaintText.Add("You let my 13 year old drink???");
                ComplaintText.Add("Y’all ‘bout to turn my daughter into a hoe!");
                ComplaintText.Add("MY SON SHOULDN’T BE EXPOSED TO THIS DEGENERACY");
                ComplaintText.Add("This isn’t a day care!");
                break;
            case Reasons.Dirty_house:
                break;
            case Reasons.Noise:
                ComplaintText.Add("I’m working tomorrow! Freakin’ kids…");
                ComplaintText.Add("Shut up!!!");
                ComplaintText.Add("Normally I don’t sleep, but tonight I was actually trying too.");
                ComplaintText.Add("I hate hearing the noise of a party I wasn’t invited too.");
                ComplaintText.Add("I’m too old for this crappy music.");
                ComplaintText.Add("Shut up you millennials!");
                ComplaintText.Add("I need to sleep! I have an exam tomorrow!");
                break;
            case Reasons.Puke:
                ComplaintText.Add("That’s disgusting!");
                ComplaintText.Add("I wanna lick it… never mind.");
                ComplaintText.Add("I can see my own breakfast there…");
                ComplaintText.Add("So unsanitary.");
                ComplaintText.Add("I’m gonna be sick!");
                break;
            default:
                break;
        }

        if (ComplaintText.Count != 0)
        {
            //Spawn complaint bubble
            SummonBubble(ComplaintText[Random.Range(0, ComplaintText.Count)]);
        }

        Debug.Log("Complaint added");
    }

    public int TotalValue()
    {
        return complaints.Aggregate(0, (acc, x) => acc + complaintValues[x.reason]);
    }

    void SummonBubble(string text)
    {
        GameObject obj = Instantiate(Bubble, new Vector3(7.3f, -3.7f, 0), Quaternion.identity);
        obj.GetComponent<GuestBubble>().SetupBubble(text);
    }
}
