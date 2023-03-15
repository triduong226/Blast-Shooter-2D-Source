using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour
{
    public int id { get; set; }
    public string userName { get; set; }
    public string passWord { get; set; }
    public int bestScore { get; set; }
    public string email { get; set; }

    public override string ToString()
    {
        return "ID: " + id + "   Name: " + userName + "  Password: " + passWord + "  Best Score: " + bestScore + " Email: " +email;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
            Account objAsPart = obj as Account;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    public override int GetHashCode()
    {
        return id;
    }
    public bool Equals(Account other)
    {
        if (other == null) return false;
        return (this.id.Equals(other.id));
    }

}
