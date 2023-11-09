using UnityEngine;
using UnityEngine.UI;
public abstract class BaseButton : MyBehaviour
{
    [SerializeField] protected Button thisbutton;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadthisbutton();
        this.AddActButton();
    }
    protected void Loadthisbutton()
    {
        thisbutton = GetComponent<Button>();
        if(thisbutton == null) Debug.LogWarning( this.transform + "dont have button");
    }
    protected void AddActButton()
    {
        if(thisbutton == null) return;
        thisbutton.onClick.AddListener(delegate () {this.Act();});
    }
    protected abstract void Act();
}
