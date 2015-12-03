/////////////////////////////////////////////////////////////////////////////////
//// Copyright (C) 2015 AsNet Co., Ltd.
//// All Rights Reserved. These instructions, statements, computer
//// programs, and/or related material (collectively, the "Source")
//// contain unpublished information proprietary to AsNet Co., Ltd
//// which is protected by US federal copyright law and by
//// international treaties. This Source may NOT be disclosed to
//// third parties, or be copied or duplicated, in whole or in
//// part, without the written consent of AsNet Co., Ltd.
/////////////////////////////////////////////////////////////////////////////////

//using UnityEngine;
//using UnityEngine.UI;
//using System;

//public class ShootingResultRowData
//{
//	public int 		order;
//	public Country 	country;
//	public string 	name;
//	public bool 	isMain;
//	public int[]	rounds;

//	public ShootingResultRowData(int order, Country country, string name, bool isMain, params int[] rounds)
//	{
//		this.order 	  = order;
//		this.country  = country;
//		this.name 	  = name;
//		this.isMain   = isMain;
//		this.rounds   = rounds;
//	}

//	public int Total()
//	{
//		int total = 0;

//		for (int i = 0; i < rounds.Length; i++)
//		{
//			if (rounds[i] > 0)
//			{
//				total += rounds[i];
//			}
//		}

//		return total;
//	}
//}

//public class ShootingResultRowScript : MonoBehaviour
//{
//	/// <summary>
//	/// The highest total's color.
//	/// </summary>
//	public Color bestColor = new Color(1.0f, 0.823529f, 0.0f, 1.0f);

//	/// <summary>
//	/// The flag position x.
//	/// </summary>
//	public float flagPosX = 240.0f;

//	public void Construct(ShootingResultRowData data, Vector2 position, float delay, Action<int> replayCallback, Action callback = null)
//	{
//		// Order
//		GameObject order = gameObject.FindInChildren("Order");
//		order.SetText(data.order.ToString());

//		// Overlay
//		GameObject overlay = gameObject.FindInChildren("Overlay");

//		// Country
//		GameObject country = gameObject.FindInChildren("Country");
//		country.SetText(data.country.Abbreviate());

//		// Flag
//		GameObject flag = gameObject.FindInChildren("Flag");
//		flag.SetImageSprite(data.country.Flag());

//		// Name
//		GameObject name = gameObject.FindInChildren("Name");

//		// Hide
//		gameObject.SetAnchoredPositionY(float.MinValue);

//		// Zoom-in
//		var zoomIn = SequenceAction.Create(DelayAction.Create(delay), SetAnchoredPositionAction.Create(position), ScaleAction.ScaleTo(Vector3.one, 0.2f, Ease.SineOut));
//		gameObject.Play(zoomIn, () => {
//			// Resize overlay
//			overlay.Play(ResizeAction.ResizeTo(new Vector2(0, -1), 0.1f), () => {
//				// Destroy overlay
//				GameObject.Destroy(overlay);
//			});

//			// Move flag
//			flag.Play(AnchoredMoveAction.MoveTo(new Vector2(flagPosX, flag.GetAnchoredPositionY()), 0.1f), () => {
//				// Type name
//				name.Play(TypeAction.Create(data.name, 0.1f), () => {
//					// Check if main player
//					if (data.isMain)
//					{
//						GameObject player = gameObject.FindInChildren("Player");
//						player.Show();
//					}
//					else
//					{
//						GameObject replay = gameObject.FindInChildren("Replay");
//						replay.Show();

//						if (replayCallback != null)
//						{
//							replay.GetComponent<Button>().onClick.AddListener(() => { replayCallback(data.order); });
//						}
//					}

//					delay = 0.05f;

//					int total = 0;

//					// Rounds
//					for (int i = 0; i < data.rounds.Length; i++)
//					{
//						int round = data.rounds[i];

//						if (round >= 0)
//						{
//							GameObject roundObj = gameObject.FindInChildren(string.Format("Round{0}", i + 1));

//							if (roundObj != null)
//							{
//								roundObj.Play(DelayAction.Create(delay), () => {
//									roundObj.SetText(round.ToString());
//								});

//								delay += 0.05f;
//							}

//							total += round;
//						}
//					}

//					// Total
//					GameObject totalObj = gameObject.FindInChildren("Total");
//					totalObj.Play(DelayAction.Create(delay), () => {
//						totalObj.SetText(total.ToString());

//						if (callback != null)
//						{
//							callback();
//						}
//					});
//				});
//			});
//		});
//	}

//	public void HighlightTotal(bool isBest, Action callback = null)
//	{
//		GameObject total = gameObject.FindInChildren("Total");
//		var scale = ScaleAction.ScaleTo(new Vector3(2, 2), 0.15f, Ease.Linear, LerpDirection.PingPong);

//		if (isBest)
//		{
//			total.Play(SequenceAction.Create(scale, SetRGBAction.Create(bestColor.RGB())), callback);
//		}
//		else
//		{
//			total.Play(scale, callback);
//		}
//	}

//	public void HighlightReplay()
//	{
//		GameObject replay = gameObject.FindInChildren("Replay");

//		if (replay.activeSelf)
//		{
//			Vector2 position = replay.GetAnchoredPosition();

//			var delay = DelayAction.Create(0.1f);
//			var scale = ScaleAction.ScaleTo(new Vector3(2, 2), 0.15f, Ease.Linear, LerpDirection.PingPong);
//			var bound = SequenceAction.Create(AnchoredMoveAction.MoveTo(new Vector2(position.x, position.y + 40.0f), 0.1f, Ease.SineOut),
//			                                  AnchoredMoveAction.MoveTo(position, 0.5f, Ease.BounceOut));
//			var action = SequenceAction.Create(delay, scale, RepeatAction.Create(SequenceAction.Create(DelayAction.Create(1.0f), bound), -1));

//			replay.Play(action);
//		}
//	}

//	public void Highlight()
//	{
//		// Show border
//		gameObject.FindInChildren("Border").Show();
//	}

//	public void MoveTo(Vector2 position, bool isHighlight, Action callback = null)
//	{
//		var move = AnchoredMoveAction.MoveTo(position, 0.3f, Ease.SineOut);

//		if (isHighlight)
//		{
//			// Get highlight
//			GameObject highlight = gameObject.FindInChildren("Highlight");
//			highlight.Show();

//			var zoom = ScaleAction.ScaleTo(new Vector3(2, 2), 0.15f, Ease.Linear, LerpDirection.PingPong);
//			var delay = DelayAction.Create(0.1f);
//			var showBorder = CallFuncAction.Create(() => {
//				// Destroy highlight
//				Destroy(highlight);

//				// Show border
//				GameObject border = gameObject.FindInChildren("Border");
//				border.Show();
//			});

//			var action = SequenceAction.Create(ParallelAction.ParallelAll(move, zoom), showBorder, delay);

//			gameObject.Play(action, callback);
//		}
//		else
//		{
//			gameObject.Play(move, callback);
//		}
//	}

//#if UNITY_EDITOR

//	void OnLayout()
//	{
//		// Overlay
//		GameObject overlay = gameObject.FindInChildren("Overlay");
//		overlay.SetWidthDelta(0);

//		// Flag
//		GameObject flag = gameObject.FindInChildren("Flag");
//		flag.SetAnchoredPositionX(flagPosX);

//		// Name
//		GameObject name = gameObject.FindInChildren("Name");
//		name.SetText("Nguyen Van A");

//		// Player
//		GameObject player = gameObject.FindInChildren("Player");
//		player.Show();

//		// Round 1
//		GameObject round1 = gameObject.FindInChildren("Round1");
//		round1.SetText("5");
		
//		// Round 2
//		GameObject round2 = gameObject.FindInChildren("Round2");
//		round2.SetText("7");
		
//		// Round 3
//		GameObject round3 = gameObject.FindInChildren("Round3");
//		round3.SetText("10");
		
//		// Total
//		GameObject total = gameObject.FindInChildren("Total");
//		total.SetText("22");
//	}

//	void OnUnlayout()
//	{
//		// Overlay
//		GameObject overlay = gameObject.FindInChildren("Overlay");
//		overlay.SetWidthDelta(130.0f);
		
//		// Flag
//		GameObject flag = gameObject.FindInChildren("Flag");
//		flag.SetAnchoredPositionX(flagPosX - 100);
		
//		// Name
//		GameObject name = gameObject.FindInChildren("Name");
//		name.SetText("");
		
//		// Player
//		GameObject player = gameObject.FindInChildren("Player");
//		player.Hide();
		
//		// Round 1
//		GameObject round1 = gameObject.FindInChildren("Round1");
//		round1.SetText("");
		
//		// Round 2
//		GameObject round2 = gameObject.FindInChildren("Round2");
//		round2.SetText("");
		
//		// Round 3
//		GameObject round3 = gameObject.FindInChildren("Round3");
//		round3.SetText("");
		
//		// Total
//		GameObject total = gameObject.FindInChildren("Total");
//		total.SetText("");
//	}

//#endif
//}
