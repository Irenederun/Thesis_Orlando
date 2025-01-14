public class SubmissionBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        gameObject.SetActive(false);
        WordDragManager.instance.reload.SetActive(false);
        WordDragManager.instance.Submission();
        if (!WordDragManager.instance.isInterstitial) GameManager.instance.IncrementCurrentSentence();
        WordDragManager.instance.DestroySelfOnClose();
    }
}
