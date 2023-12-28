public class WaitingtimeText : BaseTextUI
{
    public FindorCreateRoombutton findorCreateRoombutton;
    protected void FixedUpdate() {
        this.text.text = " Waiting Time : \n" + findorCreateRoombutton.minute.ToString("00") + ":" + findorCreateRoombutton.seconds.ToString("00");
    }
}
