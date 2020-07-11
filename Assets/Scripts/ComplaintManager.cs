using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComplaintManager : MonoBehaviour
{
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
        complaintValues.Add(Reasons.Designated_driver, 2);
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
        Designated_driver,
        Minor,
        Dirty_house,
        Noise
    }


    public void AddComplaint(Complaint complaint)
    {
        complaints.Add(complaint);
        complaintDisplay.text = $"complaints / {complaints.Count:00}";
        Debug.Log("Complaint added");
    }

    public int TotalValue()
    {
        return complaints.Aggregate(0, (acc, x) => acc + complaintValues[x.reason]);
    }
}
