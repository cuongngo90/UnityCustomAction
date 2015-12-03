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

//#if UNITY_EDITOR
////#define DEBUG_SHOOTING_RESULT_TABLE
//#endif

//using UnityEngine;
//using UnityEngine.UI;
//using System;

//public class ShootingResultTableScript : IntroBehaviour
//{
//	private static readonly float overlayAlpha = 0.70f;

//    /// <summary>
//    /// The row prefab.
//    /// </summary>
//    public GameObject rowPrefab;

//    /// <summary>
//    /// The desired width.
//    /// </summary>
//    public float width = 1900f;

//    /// <summary>
//    /// The row top.
//    /// </summary>
//    public float rowTop = -360f;

//    /// <summary>
//    /// The row step.
//    /// </summary>
//    public float rowStep = 138f;

//    /// <summary>
//    /// The row scale.
//    /// </summary>
//    public float rowScale = 3f;

//	// The disappeared callback
//	private Action _disappearedCallback;

//	public void Construct(ShootingResultRowData[] data, bool isFinished, Action<int> replayCallback, Action appearedCallback, Action disappearedCallback)
//    {
//		// Set disappeared callback
//		_disappearedCallback = disappearedCallback;

//        GameType gameType = GameType.GameShooting;

//        // Fade-in overlay
//        gameObject.Play(FadeAction.FadeTo(overlayAlpha, 0.1f));

//        // Table
//        GameObject table = gameObject.FindInChildren("Table");

//        // Content
//        GameObject content = table.FindInChildren("Content");

//        // Top
//        GameObject top = table.FindInChildren("Top");

//        // Game logo
//        GameObject gameLogo = top.FindInChildren("GameLogo");
////        gameLogo.SetImageSprite(gameType.Logo());

//        // Game name
//        GameObject gameName = top.FindInChildren("GameName");

//        // Round
//        GameObject round = top.FindInChildren("Round");

//        // Logo
//        GameObject logo = top.FindInChildren("Logo");

//        // Fill top
//        top.Play(FillAction.FillTo(1.0f, 0.1f), () => {
//            top.SetImageType(Image.Type.Sliced);

//            // Resize top
//            top.Play(ResizeAction.ResizeTo(new Vector2(width, -1), 0.2f, Ease.SineOut), () => {
//                // Fill game logo
//                gameLogo.Play(FillAction.FillTo(1.0f, 0.1f));

//                // Fill logo
//                logo.Play(FillAction.FillTo(1.0f, 0.1f), () => {
//                    // Type game name
//                    gameName.Play(TypeAction.Create(gameType.Name(), 0.1f), () => {
//                        // Show round
//                        round.Show();
//                    });

//                    // Move content
//                    content.Play(AnchoredMoveAction.MoveTo(new Vector2(content.GetAnchoredPositionX(), 0), 0.1f), () => {
//                        float delay = 0.1f;
//                        Vector3 position = new Vector3(0, rowTop, 0);

//						int rowCount = data.Length;
//						int bestIndex = -1;

//						if (rowCount > 1)
//						{
//							int best = 0;

//							for (int i = 0; i < rowCount; i++)
//							{
//								int total = data[i].Total();

//								if (total > best)
//								{
//									best = total;
//									bestIndex = i;
//								}
//							}

//							if (bestIndex >= 0)
//							{
//								int i = 0;
//								for (; i < rowCount; i++)
//								{
//									if (data[i].Total() < best)
//									{
//										break;
//									}
//								}

//								// Check if draw
//								if (i == rowCount)
//								{
//									bestIndex = -1;
//								}
//							}
//						}

//						for (int i = 0; i < rowCount; i++)
//                        {
//                            GameObject row = Instantiate(rowPrefab) as GameObject;
//                            row.transform.SetParent(content.transform);
//                            row.transform.SetScale(rowScale);

//                            var rowScript = row.GetComponent<ShootingResultRowScript>();

//                            if (i < rowCount - 1)
//                            {
//                                rowScript.Construct(data[i], position, delay, replayCallback);
//                            }
//                            else
//                            {
//                                rowScript.Construct(data[i], position, delay, replayCallback, () => {
//									// Get mask
//									Mask mask = table.GetComponent<Mask>();
									
//									if (mask != null)
//									{
//										// Hide mask
//										mask.enabled = false;
//									}

//									ShootingResultRowScript[] scripts = content.GetComponentsInChildren<ShootingResultRowScript>();
//									int count = scripts.Length;
									
//									Action highlightReplay = () => {
//										for (int j = 0; j < count; j++)
//										{
//											scripts[j].HighlightReplay();
//										}

//										if (isFinished)
//										{
//											table.SetHeightDelta(1000);
											
//											GameObject bottom = table.FindInChildren("Bottom");
//											bottom.Show();

//											//TODO
//											Button back = bottom.GetComponentInChildren<Button>("Back");
//											back.onClick.AddListener(() => {
//												// Stop music
//												SoundManager.Instance.StopMusic();
												
//												// Stop all sounds
//												SoundManager.Instance.StopAllSounds();
												
//												// Back to Login
//												TransitionManager.Instance.FadeTransitionScene("Login");
//											});

//											Button replay = bottom.GetComponentInChildren<Button>("Replay");
//											replay.onClick.AddListener(() => {
//												Application.LoadLevel(Application.loadedLevel);
//											});

//											if (appearedCallback != null)
//											{
//												appearedCallback();
//											}
//										}
//										else
//										{
//											disappearDelayTime = -1f;
//											this.Interactable = true;
//										}
//									};

//									for (int j = 0; j < count; j++)
//									{
//										bool isBest = (bestIndex < 0 || bestIndex == j);

//										if (j < count - 1)
//										{
//											scripts[j].HighlightTotal(isBest);
//										}
//										else
//										{
//											scripts[j].HighlightTotal(isBest, () => {
//												if (bestIndex >= 0)
//												{
//													if (data[0].Total() < data[1].Total())
//													{
//														Vector2 position1 = scripts[0].gameObject.GetAnchoredPosition();
//														Vector2 position2 = scripts[1].gameObject.GetAnchoredPosition();
														
//														scripts[0].MoveTo(position2, false);
//														scripts[1].MoveTo(position1, true, highlightReplay);
//													}
//													else
//													{
//														scripts[0].Highlight();
														
//														highlightReplay();
//													}
//												}
//												else
//												{
//													highlightReplay();
//												}
//											});
//										}
//									}
//                                });
//                            }

//                            position.y -= rowStep;
//                            delay += 0.4f;
//                        }
//                    });
//                });
//            });
//        });
//    }

//	protected override void OnDisappear()
//	{
//		// Fade-out overlay
//		gameObject.Play(FadeAction.FadeTo(0.0f, 0.1f));

//		// Table
//		GameObject table = gameObject.FindInChildren("Table");

//		// Move-out
//		table.Play(AnchoredMoveAction.MoveTo(new Vector2(-2000, table.GetAnchoredPositionY()), 0.25f, Ease.SineIn), () => {
//			if (_disappearedCallback != null)
//			{
//				_disappearedCallback();
//			}

//#if !DEBUG_SHOOTING_RESULT_TABLE
//			// Self-destroy
//			Destroy(gameObject);
//#endif
//		});
//	}

//#if DEBUG_SHOOTING_RESULT_TABLE

//	void Start()
//	{
//		ShootingResultRowData[] data = new ShootingResultRowData[]
//		{
////			new ShootingResultRowData(1, Country.Vietnam, "Nguyen Van A", true, 5, 7, 10),
////			new ShootingResultRowData(2, Country.GreatBritain, "Great Britain", false, 3, 8, 9)

//			new ShootingResultRowData(2, Country.Vietnam, "Nguyen Van A", true, 5, 7, 9),
//			new ShootingResultRowData(1, Country.GreatBritain, "Great Britain", false, 7, 8, 9)

////			new ShootingResultRowData(1, Country.Vietnam, "Nguyen Van A", true, 5, 7, 10),
////			new ShootingResultRowData(2, Country.GreatBritain, "Great Britain", false, 5, 8, 9)
//		};

//		bool isFinished = true;

//		Construct(data, isFinished,
//		(order) => {
//			Debug.Log("Replay " + order);
//		},
//		() => {
//			Debug.Log("Appeared!");
//		},
//		() => {
//			if (isFinished)
//			{
//				Canvas canvas = GameObject.FindObjectOfType<Canvas>();
//				GameObject victory = canvas.gameObject.FindInChildren("Victory");

//				if (victory != null)
//				{
//					victory.Show();

//					ShootingVictory script = victory.GetComponent<ShootingVictory>();
//					script.Construct("Nguyen Van A");
//				}

//				GameObject fireworks = GameObject.Find("Fireworks");
				
//				if (fireworks != null)
//				{
//					fireworks.FindInChildren("Fireworks").Show();
//				}
//			}
//			else
//			{
//				Debug.Log("Done!");
//			}
//		});
//	}

//	void OnGUI()
//	{
//		if (GUI.Button(new Rect(10, 10, 100, 40), "Replay"))
//		{
//			Application.LoadLevel(Application.loadedLevel);
//		}
//	}

//    void OnLayout()
//    {
//        // Show overlay
//        gameObject.SetImageAlpha(overlayAlpha);

//        // Table
//        GameObject table = gameObject.FindInChildren("Table");

//        // Top
//        GameObject top = table.FindInChildren("Top");
//        top.SetWidthDelta(width);
//        top.SetImageType(Image.Type.Sliced);

//        // Game logo
//        GameObject gameLogo = top.FindInChildren("GameLogo");
//        gameLogo.SetImageFillAmount(1.0f);

//        // Game name
//        GameObject gameName = top.FindInChildren("GameName");
//        gameName.SetText("SHOOTING");

//        // Logo
//        GameObject logo = top.FindInChildren("Logo");
//        logo.SetImageFillAmount(1.0f);

//        // Round
//        GameObject round = top.FindInChildren("Round");
//        round.Show();

//        // Content
//        GameObject content = table.FindInChildren("Content");
//        content.SetAnchoredPositionY(0);

//        // Row
//		GameObject row = Instantiate(rowPrefab) as GameObject;
//		row.transform.SetParent(content.transform);
//		row.transform.localScale = Vector3.one;
//		row.SetAnchoredPosition(new Vector2(0, rowTop));
//		row.SendMessage("OnLayout");
//	}

//    void OnUnlayout()
//    {
//        // Hide overlay
//        gameObject.SetImageAlpha(0);

//        // Table
//        GameObject table = gameObject.FindInChildren("Table");

//        // Top
//        GameObject top = table.FindInChildren("Top");
//		top.SetWidthDelta(239f);
//        top.SetImageFilledLeft();
//        top.SetImageFillAmount(0.0f);

//        // Game logo
//        GameObject gameLogo = top.FindInChildren("GameLogo");
//        gameLogo.SetImageFillAmount(0.0f);

//        // Game name
//        GameObject gameName = top.FindInChildren("GameName");
//        gameName.SetText("");

//        // Logo
//        GameObject logo = top.FindInChildren("Logo");
//        logo.SetImageFillAmount(0.0f);

//        // Round
//        GameObject round = top.FindInChildren("Round");
//        round.Hide();

//        // Content
//        GameObject content = table.FindInChildren("Content");
//        content.SetAnchoredPositionY(-content.GetHeightDelta());

//		// Rows
//		ShootingResultRowScript[] rows = table.GetComponentsInChildren<ShootingResultRowScript>();
		
//		for (int i = 0; i < rows.Length; i++)
//		{
//			DestroyImmediate(rows[i].gameObject);
//		}
//	}
	
//#endif
//}
