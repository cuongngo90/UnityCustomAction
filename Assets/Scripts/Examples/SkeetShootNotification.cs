using UnityEngine;
using UnityEngine.UI;

public class SkeetShootNotification : MonoBehaviour
{
	public Sprite[] rounds  = new Sprite[3];
	public Sprite[] targets = new Sprite[3];

    public GameObject round;
    public GameObject target;

	private Vector2 _roundPosition;
	private Vector2 _targetPosition;

	void Start()
	{
		_roundPosition  = round.GetAnchoredPosition();
		_targetPosition = target.GetAnchoredPosition();
	}

    public void OnNextRound(int nextRound)
    {
    	int index = (nextRound > 3) ? 2 : nextRound - 1;

		round.SetImageSprite(rounds[index]);
		target.SetImageSprite(targets[index]);

		round.Show();
		target.Show();

		var roundMove1 = AnchoredMoveAction.MoveTo(new Vector2(0, _roundPosition.y), 0.25f, Ease.SineOut);
		var roundDelay = DelayAction.Create(1.0f);
		var roundMove2 = AnchoredMoveAction.MoveTo(new Vector2(_targetPosition.x, _roundPosition.y), 0.25f, Ease.SineIn);

		var targetMove1 = AnchoredMoveAction.MoveTo(new Vector2(0, _targetPosition.y), 0.25f, Ease.SineOut);
		var targetDelay = DelayAction.Create(1.0f);
		var targetMove2 = AnchoredMoveAction.MoveTo(new Vector2(_roundPosition.x, _targetPosition.y), 0.25f, Ease.SineIn);

		round.Play(SequenceAction.Create(roundMove1, roundDelay, roundMove2, SetAnchoredPositionAction.Create(_roundPosition), HideAction.Create()));
		target.Play(SequenceAction.Create(targetMove1, targetDelay, targetMove2, SetAnchoredPositionAction.Create(_targetPosition), HideAction.Create()));
    }
}
