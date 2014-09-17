using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween.Plugins;
using Holoville.HOTween;

public class SceneScript : MonoBehaviour {

	public Transform cardPrefab;
	public Transform addPoint;

	private ArrayList  cardOnScreen;
	private GameObject prevCardOpen;

	static public bool enableToTouch;
	
	void Start () {
		// Init HOTWeen
		HOTween.Init ();

		// Init round
		StartCoroutine (this.InitRound ());
	}

	void Update () {
	}

	// -----------------------------------------------------------------------------

	/// <summary>
	/// Opens the card - Tinh diem
	/// </summary>
	/// <param name="card">Card.</param>
	public void OpenCard (GameObject card) {
		enableToTouch = false;
		if (prevCardOpen == null) {
			prevCardOpen = card;
			enableToTouch = true;
		} else {
			StartCoroutine(WaitAndCheckOpenCard(card));
		}
	}

	/// <summary>
	/// Exits to map.
	/// </summary>
	public void ExitToMap () {
		Application.LoadLevel ("Map");
	}

	/// <summary>
	/// Reset man choi, choi lai
	/// </summary>
	public void ResetRound () {
		// Clear debug
		DebugScript.Clear ();

		// Destroy all card on screen
		for (int i = 0; i < this.cardOnScreen.Count; i ++) {
			Transform card = (Transform)this.cardOnScreen [i];
			Destroy (card.gameObject);
		}

		// Exit Game Over
		Transform gameOver = this.transform.parent.FindChild("GameOver");
		if (gameOver) {
			GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
			gameOverScipt.ExitGameOver ();
		}

		// Reset time
		TimerScript timerScript = this.gameObject.GetComponent<TimerScript> ();
		timerScript.ResetTimer ();

		// Reset POINT
		PlayerScipt.Point = 0;

		// Init round
		StartCoroutine (this.InitRound ());
	}

	// -----------------------------------------------------------------------------

	/// <summary>
	/// Waits the and check open card.
	/// </summary>
	/// <param name="card">Card.</param>
	IEnumerator WaitAndCheckOpenCard(GameObject card) {

		// Can doi 1 khoang thoi gian 0.65s de card hien thi tren man hinh truoc khi kiem tra va flip hoac destroy no
		yield return new WaitForSeconds(0.65f);

		if (prevCardOpen != null) {
			CardScript prevScript = prevCardOpen.GetComponent<CardScript> ();				
			CardScript cardScript = card.GetComponent<CardScript> ();

			// Neu 2 card cung loai voi nhau
			if (prevScript.cardType == cardScript.cardType) {

				bool isNeedDestroyCard = true;
				bool isAllowAddPoint   = true;

				// Thua ngay lap tuc
				if (cardScript.cardType == CardType.Wolf || cardScript.cardType == CardType.WolfKing) {
					Transform gameOver = this.transform.parent.FindChild("GameOver");
					if (gameOver) {
						GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
						gameOverScipt.EnterGameOver ();
					}
					isNeedDestroyCard = false;
					isAllowAddPoint   = false;

					print ("Gap soi : game over");
				}
				// Khong lam gi ca
				else if (cardScript.cardType == CardType.Stone) {
					print ("Khong lam gi ca");
					// just flip it
					isNeedDestroyCard = false;
					isAllowAddPoint   = false;
				}
				// Lap 3 cap bai ngau nhien tren man hinh
				else if (cardScript.cardType == CardType.BlueButterfly) {
					print ("Lat 3 la bai");
				}
				// Them 10s vao thoi gian choi
				else if (cardScript.cardType == CardType.RedButterfly) {
					print ("Them 10s");
				}
				// Doi mat sau cua 3 cap la bai thanh mau khac
				else if (cardScript.cardType == CardType.YellowButterfly) {
					print ("Doi mat sau cua 3 cap bai");
				}
				// Lat loai lat bai con nhieu nhat trong man choi
				else if (cardScript.cardType == CardType.VioletButterfly) {
					print ("Lat tat ca la bai nhieu nhat trong man choi");
				} 
				// Nhung la bai binh thuong
				else {
				}

				if (isAllowAddPoint) {
					// Add point				
					PlayerScipt.Point += cardScript.cardProperties.point;
				}

				if (isNeedDestroyCard) {
					// Animation and auto destroy when finish
					Animator animator = card.GetComponent<Animator> ();
					animator.SetTrigger("exit");
					Animator prevAnimator = prevCardOpen.GetComponent<Animator> ();
					prevAnimator.SetTrigger("exit");

					// Remove from list
					this.cardOnScreen.Remove(card.transform);
					this.cardOnScreen.Remove(prevCardOpen.transform);
				}
				else {
					// Flip card
					FlipCardScript flipPrevScript = prevCardOpen.GetComponent<FlipCardScript> ();
					flipPrevScript.FlipCard(false);
					FlipCardScript flipCardScript = card.GetComponent<FlipCardScript> ();
					flipCardScript.FlipCard(false);
				}

				yield return new WaitForSeconds(0.2f);

				// Tu dong kiem tra so card con lai tren man hinh
				// Neu chi con lai Wolf, WolfKing va Stone thi tu dong clear het so card con lai 
				bool isAllowClearAllCardLeft = true; 
				for (int i = 0; i < this.cardOnScreen.Count; i ++) {
					Transform  cardGame = (Transform)this.cardOnScreen [i];
					CardScript cardGameScript = cardGame.GetComponent<CardScript> ();
					if (cardGameScript.cardType != CardType.Wolf &&
					    cardGameScript.cardType != CardType.WolfKing &&
					    cardGameScript.cardType != CardType.Stone) {
						print ("La " + cardGameScript.cardType.ToString());
						isAllowClearAllCardLeft = false;
						break;
					}
				}
				if (isAllowClearAllCardLeft) {
					print ("Chi con lai soi voi da");
					// Tu dong mo len
					for (int i = 0; i < this.cardOnScreen.Count; i ++) {
						Transform  cardGame = (Transform)this.cardOnScreen [i];
						FlipCardScript flipPrevScript = cardGame.GetComponent<FlipCardScript> ();
						flipPrevScript.FlipCard(false);
					}

					yield return new WaitForSeconds(1.2f);

					// Animation exit all
					for (int i = 0; i < this.cardOnScreen.Count; i ++) {
						Transform  cardGame = (Transform)this.cardOnScreen [i];
						CardScript cardGameScript = cardGame.GetComponent<CardScript> ();

						// Cong diem
						PlayerScipt.Point += cardGameScript.cardProperties.point;

						// Animation and auto destroy when finish
						Animator animator = cardGame.GetComponent<Animator> ();
						animator.SetTrigger("exit");
						
						// Remove from list
						this.cardOnScreen.Remove(cardGame.transform);
						i --;
					}
				}

				// ---------------- Next round -----------------------
				if (this.cardOnScreen.Count == 0) {
					print("next round");

					DebugScript.Clear ();
					// Next round
					StartCoroutine(this.InitRound ());
				}
			} 
			// Neu 2 card khac loai
			else {
				// Flip card
				FlipCardScript flipPrevScript = prevCardOpen.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);
				FlipCardScript flipCardScript = card.GetComponent<FlipCardScript> ();
				flipCardScript.FlipCard(false);
			}
			prevCardOpen = null;
		}

		enableToTouch = true;
	}

	private IEnumerator InitRound () {
		// So luong object can ve
		int numberOfCol = PlayerPrefs.GetInt("MapSizeLoadCol",4);
		int numberOfRow = PlayerPrefs.GetInt("MapSizeLoadRow",4);
		int numberOfObjectToDraw = numberOfCol * numberOfRow;
		
		//  -----------------------------------------------------
		/*
		 * Cach ve cac doi tuong card len man hinh :
		 * Khi ve het cac la bai len man hinh se tao ra 1 vung ve 
		 * B1: tinh chieu rong va chieu cao cua vung ve
		 * B2: tim top, left cua vi tri vung ve tren man hinh chinh la top,left cua cai card dau tien
		 * De tim top, left cua vung ve can tinh width, height va top,left,bottom,right cua visiable screen so voi camera
		 * B3: va cac card tu trai sang phai, tu tren xuong doi
		 */ 
		
		// Size of card
		Transform cardBack = cardPrefab.FindChild ("CardBack");
		float cardWidth = cardBack.transform.renderer.bounds.size.x;
		float cardHeight = cardBack.transform.renderer.bounds.size.y;
		
		// Khi ve tat ca len man hinh, tat ca card se tao ra 1 vung
		// B1: tinh chieu rong va chieu cao cua vung ve
		float widthOfAllCard = ((float)(cardWidth * numberOfCol)  + (float)0.1 * (numberOfCol - 1));
		float heighOfAllCard = ((float)(cardHeight * numberOfRow) + (float)0.1 * (numberOfRow - 1));
		
		// Top,left, boom, right cua visiable screen so voi camera
		Vector2 edgeTopRightVector  = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		Vector2 edgeLeftBottomVector = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		// width, height cua visiable screen so voi camera
		float screenWidth  = edgeTopRightVector.x * 2;
		float screenHeight = edgeTopRightVector.y * 2;
		
		// Top/left of normal card on visiable screen
		// B2: tim top, left cua vi tri vung ve tren man hinh chinh la top,left cua cai card dau tien
		var topLeftOfFirstCard = new Vector2 (edgeLeftBottomVector.x + (screenWidth - widthOfAllCard) / 2 + cardWidth/2,
		                                      edgeLeftBottomVector.y + (screenHeight - heighOfAllCard)/2  + heighOfAllCard - cardHeight/2);

		// Luu thong tin so luong cac card duoc random
		Dictionary<string,int> cardDic = new Dictionary<string, int> ();

		// Random card thanh cac cap
		this.cardOnScreen = new ArrayList ();
		for (int i = 0; i < numberOfObjectToDraw/2;  i ++) {
			
			// Random type
			CardType type = (CardType)Random.Range(0,(int)CardType.VioletButterfly);
			
			// ---------------------------------------
			for (int j = 0; j < 2 ; j ++) {
				var card = Instantiate(cardPrefab) as Transform;
				// Set type
				CardScript cardScript = card.GetComponent<CardScript>();
				cardScript.cardType = type;			
				this.cardOnScreen.Add(card);
			}

			// Luu so luong card duoc random
			int numberCardDidRandom = 0;
			if (cardDic.ContainsKey(type.ToString())) {
				numberCardDidRandom = cardDic[type.ToString()];
			}
			numberCardDidRandom += 2;
			cardDic[type.ToString()] = numberCardDidRandom;
		}

		// Debug
		print ("init card : " + this.cardOnScreen.Count.ToString ());
		foreach (string key in cardDic.Keys) {
			int numberCardDidRandom = cardDic[key];
			DebugScript.AddText(key + " : " + numberCardDidRandom.ToString() + " card");
		}

		// Thay doi thu tu vi tri cua card trong mang
		for (int i = 0; i < 100; i ++) {
			int index1 = Random.Range(0,this.cardOnScreen.Count);
			int index2 = Random.Range(0,this.cardOnScreen.Count);
			var object1 = this.cardOnScreen[index1];
			var object2 = this.cardOnScreen[index2];
			this.cardOnScreen[index1] = object2;
			this.cardOnScreen[index2] = object1;
		}
		
		//B3: va cac card tu trai sang phai, tu tren xuong doi
		for (int i = 0; i < this.cardOnScreen.Count;  i ++) {
			Transform card = (Transform)this.cardOnScreen[i];
			
			int indexCol = i % numberOfCol;
			
			// Tinh vi tri hien thi theo truc x
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			// card nam o top ben ngoai man hinh theo truc y
			float posY = 0 + screenHeight/2 + cardHeight/2;

			card.transform.position = new Vector3 (posX, posY, 0);
		}
		// Animation drop down
		for (int i = 0; i < this.cardOnScreen.Count; i ++) {
			Transform card = (Transform)this.cardOnScreen[i];
			
			int indexCol = i % numberOfCol;
			int indexRow = i / numberOfCol;
			
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			float posY = (float)(topLeftOfFirstCard.y  - indexRow * (cardHeight + 0.1));

			TweenParms parms = new TweenParms().Prop("position", new Vector3(posX,posY,0)).Ease(EaseType.EaseOutBack).Delay((float)0.1*i);
			HOTween.To (card, 0.5f, parms);
		}
		yield return new WaitForSeconds ((float)0.1*this.cardOnScreen.Count);

		// Bat dau tinh thoi gian choi
		TimerScript timerScript = this.gameObject.GetComponent<TimerScript> ();
		timerScript.StartTimer ();

		// Cho phep user co the choi
		SceneScript.enableToTouch = true;
	}
}
