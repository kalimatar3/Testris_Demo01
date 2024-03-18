public class WaitingtimeText : BaseTextUI
{
    protected void FixedUpdate() {
        this.text.text = PhotonManager.Instance.PhotonRoom.minute.ToString("00") + ":" + PhotonManager.Instance.PhotonRoom.seconds.ToString("00");
    }
}
