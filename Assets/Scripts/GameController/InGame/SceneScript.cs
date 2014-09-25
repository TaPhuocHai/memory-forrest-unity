using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween.Plugins;
using Holoville.HOTween;

public class SceneScript : MonoBehaviour {

	public Transform cardPrefab;
	public Transform addPoint;

	// Danh sach card tren man hinh
	private ArrayList  _cardOnScreen;
	// Card da duoc mo
	private GameObject _prevCardOpen;

	// Vung dat user dang choi
	private Region      _region;
	// Luu ket qua choi trong moi game
	private PlayGameData _playGameData;

	// Vong choi hien tai
	private int          _round;

	// Cho phep touch
	static public bool EnableToTouch;

	void Awake () 
	{
		print ("Awake");
	}
	
	void Start () 
	{
		print ("Start");

		// Init Region
		RegionType regionType = (RegionType)PlayerPrefs.GetInt (Constant.kPlayGameInRegion);
		this._region = new Region (regionType);

		// Init playGameData
		this._playGameData = PlayGameData.Instance;

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
	public void OpenCard (GameObject card) 
	{
		EnableToTouch = false;
		if (_prevCardOpen == null) {
			_prevCardOpen = card;
			EnableToTouch = true;
		} else {
			StartCoroutine(WaitAndCheckOpenCard(card));
		}
	}

	/// <summary>
	/// Exits to map.
	/// </summary>
	public void ExitToMap () 
	{
		Application.LoadLevel ("Map");
	}

	/// <summary>
	/// Reset man choi, choi lai
	/// </summary>
	public void ResetRound () 
	{
		// Tao moi lop luu thong tin man choi
		this._playGameData.Reset ();

		// Clear debug
		DebugScript.Clear ();

		// Destroy all card on screen
		for (int i = 0; i < this._cardOnScreen.Count; i ++) {
			Transform card = (Transform)this._cardOnScreen [i];
			Destroy (card.gameObject);
		}

		// Exit Game Over
		Transform gameOver = this.transform.parent.FindChild("GameOver");
		if (gameOver) {
			GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
			gameOverScipt.ExitGameOver ();
		}

		// Reset time
		TimerScript.ResetTimer ();

		// Reset POINT
		this._playGameData.score = 0;

		// Init round
		StartCoroutine (this.InitRound ());
	}

	// -----------------------------------------------------------------------------

	/// <summary>
	/// Waits the and check open card.
	/// </summary>
	/// <param name="card">Card.</param>
	IEnumerator WaitAndCheckOpenCard(GameObject card) 
	{
		// Can doi 1 khoang thoi gian 0.65s de card hien thi tren man hinh truoc khi kiem tra va flip hoac destroy no
		yield return new WaitForSeconds(0.65f);

		if (_prevCardOpen != null) {
			CardScript prevScript = _prevCardOpen.GetComponent<CardScript> ();				
			CardScript cardScript = card.GetComponent<CardScript> ();

			// Neu 2 card cung loai voi nhau
			if (prevScript.cardType == cardScript.cardType) {

				// ----------------------------------------------------------
				// Buoc 1 : kiem tra co cho phep lay diem va xoa 2 card nay ?

				// Doi voi la bai thuong
				bool isNeedDestroyCard = true;
				bool isAllowAddPoint   = true;

				// Thua ngay lap tuc
				if (cardScript.cardType == CardType.Wolf || cardScript.cardType == CardType.WolfKing) {
					isNeedDestroyCard = false;
					isAllowAddPoint   = false;
				}
				// Khong lam gi ca
				else if (cardScript.cardType == CardType.Stone) {
					// just flip it
					isNeedDestroyCard = false;
					isAllowAddPoint   = false;
				}

				if (isAllowAddPoint) {
					// Add point				
					this._playGameData.score += cardScript.card.point;
				}

				if (isNeedDestroyCard) {
					// Animation and auto destroy when finish
					Animator animator = card.GetComponent<Animator> ();
					animator.SetTrigger("exit");
					Animator prevAnimator = _prevCardOpen.GetComponent<Animator> ();
					prevAnimator.SetTrigger("exit");

					// Remove from list
					this._cardOnScreen.Remove(card.transform);
					this._cardOnScreen.Remove(_prevCardOpen.transform);

					// Luu thong tin
					this._playGameData.CollectCard(cardScript.cardType,2);
				}
				else {
					// Flip card
					FlipCardScript flipPrevScript = _prevCardOpen.GetComponent<FlipCardScript> ();
					flipPrevScript.FlipCard(false);
					FlipCardScript flipCardScript = card.GetComponent<FlipCardScript> ();
					flipCardScript.FlipCard(false);
				}

				//---------------------------------------------------------------
				// Buoc 2 - Effect

				// Thua ngay lap tuc
				if (cardScript.cardType == CardType.Wolf || cardScript.cardType == CardType.WolfKing) {
					Transform gameOver = this.transform.parent.FindChild("GameOver");
					if (gameOver) {
						GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
						gameOverScipt.EnterGameOver ();
					}
				}
				// Lap 3 cap bai ngau nhien tren man hinh
				else if (cardScript.cardType == CardType.BlueButterfly) {
					print ("Lat 3 cap bai");
					StartCoroutine(this.Open3CoupleCard ());
				}
				// Them 10s vao thoi gian choi
				else if (cardScript.cardType == CardType.RedButterfly) {
					print ("Them 10s");
					TimerScript.AddMoreTime(10.0f);
				}
				// Doi mat sau cua 3 cap la bai thanh mau khac
				else if (cardScript.cardType == CardType.YellowButterfly) {
					print ("Doi mat sau cua 3 cap bai");
					this.ChangeFaceBack3CoupleCard ();
				}
				// Lat loai lat bai con nhieu nhat trong man choi
				else if (cardScript.cardType == CardType.VioletButterfly) {
					print ("Lat tat ca la bai nhieu nhat trong man choi");
					StartCoroutine(this.OpenCardsWithHighestNumber ());
				} 

				//---------------------------------------------------------------
				// Buoc 3 - kiem tra so card con lai
				yield return new WaitForSeconds(0.2f);
				StartCoroutine(this.CheckCardOnScreenAndInitNextRoundIfNeed ());
			} 
			// Neu 2 card khac loai
			else {
				// Flip card
				FlipCardScript flipPrevScript = _prevCardOpen.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);
				FlipCardScript flipCardScript = card.GetComponent<FlipCardScript> ();
				flipCardScript.FlipCard(false);
				EnableToTouch = true;
			}
			_prevCardOpen = null;
		} else {
			EnableToTouch = true;
		}
	}

	/// <summary>
	/// Kiem tra xem da mo het card chua
	/// Neu da mo het roi thi tu dong NextRound
	/// </summary>
	private IEnumerator CheckCardOnScreenAndInitNextRoundIfNeed () {
		// Tu dong kiem tra so card con lai tren man hinh
		// Neu chi con lai Wolf, WolfKing va Stone thi tu dong clear het so card con lai 
		bool isAllowClearAllCardLeft = true; 
		for (int i = 0; i < this._cardOnScreen.Count; i ++) {
			Transform  cardGame = (Transform)this._cardOnScreen [i];
			CardScript cardGameScript = cardGame.GetComponent<CardScript> ();
			if (cardGameScript.cardType != CardType.Wolf &&
			    cardGameScript.cardType != CardType.WolfKing &&
			    cardGameScript.cardType != CardType.Stone) {
				isAllowClearAllCardLeft = false;
				break;
			}
		}
		if (isAllowClearAllCardLeft && this._cardOnScreen.Count != 0) {
			print ("Chi con lai soi voi da");
			// Tu dong mo len
			for (int i = 0; i < this._cardOnScreen.Count; i ++) {
				Transform  cardGame = (Transform)this._cardOnScreen [i];
				FlipCardScript flipPrevScript = cardGame.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);
			}
			
			yield return new WaitForSeconds(1.2f);
			
			// Animation exit all
			for (int i = 0; i < this._cardOnScreen.Count; i ++) {
				Transform  cardGame = (Transform)this._cardOnScreen [i];
				CardScript cardGameScript = cardGame.GetComponent<CardScript> ();
				
				// Cong diem
				this._playGameData.score += cardGameScript.card.point;

				this._playGameData.CollectCard(cardGameScript.cardType,1);
				
				// Animation and auto destroy when finish
				Animator animator = cardGame.GetComponent<Animator> ();
				animator.SetTrigger("exit");
				
				// Remove from list
				this._cardOnScreen.Remove(cardGame.transform);
				i --;
			}
		}
		
		// ---------------- Next round -----------------------
		if (this._cardOnScreen.Count == 0) {
			print("next round");

			// Luu thong tin man choi
			this._playGameData.isClearAllARound = true;
			this._playGameData.roundClearAll = this._round;

			// Kiem da unlock next round
			if (Region.IsUnlockRound (this._region.regionType,this._round + 1)) {
				// round moi
				this._round += 1;

				DebugScript.Clear ();
				// Init round
				StartCoroutine(this.InitRound ());
			} else {
				// Luu thong tin man choi
				this._playGameData.Save();

				// Game Over
				Transform gameOver = this.transform.parent.FindChild("GameOver");
				if (gameOver) {
					GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
					gameOverScipt.EnterGameOver ();
				}
			}
		}
		EnableToTouch = true;
	}

	/// <summary>
	/// Doi mat sau cua 3 cap la bai bat ky
	/// </summary>
	private void ChangeFaceBack3CoupleCard () 
	{
		ArrayList cardNeedOpenList = this.Get3CoupleCardOnScrene ();

		foreach (Transform c in cardNeedOpenList) {
			CardScript cScript = c.GetComponent<CardScript> ();
			cScript.ChangeFaceBack(CardFaceBack.Special);
		}
	}

	/// <summary>
	/// Lat 3 cap bai bat ky
	/// </summary>
	/// <returns>The couple card.</returns>
	private IEnumerator Open3CoupleCard () 
	{
		EnableToTouch = false;

		ArrayList cardNeedOpenList = this.Get3CoupleCardOnScrene ();
		print ("cardNeedOpenList : " + cardNeedOpenList.Count.ToString());

		// Lat tat ca card 
		foreach (Transform c in cardNeedOpenList) {
			FlipCardScript flipPrevScript = c.GetComponent<FlipCardScript> ();
			flipPrevScript.FlipCard(false);
		}
		yield return new WaitForSeconds(1.2f);			
		// Animation exit tat
		foreach (Transform c in cardNeedOpenList) {
			FlipCardScript flipPrevScript = c.GetComponent<FlipCardScript> ();
			flipPrevScript.FlipCard(false);
		}
		
		yield return new WaitForSeconds(0.2f);
		StartCoroutine(this.CheckCardOnScreenAndInitNextRoundIfNeed ());
	}

	/// <summary>
	/// Lay 3 cap card ngau nhien tren man hinh
	/// </summary>
	/// <returns>The couple card on screne.</returns>
	private ArrayList Get3CoupleCardOnScrene () 
	{
		if (this._cardOnScreen.Count <= 6) {
			return this._cardOnScreen;
		}

		// Tim card type va so luong cua moi loai dang co tren man hinh
		//  cardType : so luong
		Dictionary<int, int> numberOfCardWithTypeKey = new Dictionary<int, int>();
		foreach (Transform c in this._cardOnScreen) {
			CardScript cScript = c.GetComponent<CardScript> ();
			int numberOfThisType = 0;
			if (numberOfCardWithTypeKey.ContainsKey((int)cScript.cardType)) {
				numberOfThisType = (int)numberOfCardWithTypeKey[(int)cScript.cardType];
			}
			numberOfThisType += 1;
			numberOfCardWithTypeKey[(int)cScript.cardType] = numberOfThisType;
		}
		
		// Tim ra so luong toi da cua 1 loai card nao do
		int maxCardOfAType = -1;
		foreach (int key in numberOfCardWithTypeKey.Keys) {
			int numberCard = (int)numberOfCardWithTypeKey[key];
			if (numberCard > maxCardOfAType) {
				maxCardOfAType = numberCard;
			}
		}
		print ("maxCardOfAType : " + maxCardOfAType.ToString());
		
		// So luong (theo couple) card can lay cho moi type
		Dictionary<int, int> numberCoupleCardWithEachTypeToOpen = new Dictionary<int, int>();
		int totalValueDidGet = 0;
		
		// Uu tien lay cac card binh thuong
		// So vong duyet : maxCardOfAType/2
		for (int i = 0 ; i < maxCardOfAType/2 ; i++) {
			// Cu moi vong - duyet qua tat ca cac type co tren man hinh
			foreach(int key in numberOfCardWithTypeKey.Keys) {
				int totalCardOfThisType = (int)numberOfCardWithTypeKey[key];
				// Neu so cap cua no nhieu hon vong nay thi co the lay cap nay
				if (totalCardOfThisType/2 >= i) {
					int value = 0;
					if (numberCoupleCardWithEachTypeToOpen.ContainsKey(key)) {
						value = (int)numberCoupleCardWithEachTypeToOpen[key];
					}
					value ++;
					numberCoupleCardWithEachTypeToOpen[key] = value;
					
					// Dem so luong da lay
					totalValueDidGet ++;
				}
				
				if (totalValueDidGet == 3) {
					break;
				}
			}
			
			if (totalValueDidGet == 3) {
				break;
			}
		}

		ArrayList cardNeedOpenList = new ArrayList ();

		// Lay ra cac card can open
		foreach (int key in numberCoupleCardWithEachTypeToOpen.Keys) {
			CardType type = (CardType)key;
			
			int numberCouple = (int)numberCoupleCardWithEachTypeToOpen[key];
			print("type : " + type.ToString() + " - couple " + numberCouple.ToString());
			
			int totalDidGet = 0;
			foreach (Transform c in this._cardOnScreen) {
				CardScript cScript = c.GetComponent<CardScript> ();
				if (cScript.cardType == (CardType)key) {
					cardNeedOpenList.Add(c);
					totalDidGet ++;
					
					if (totalDidGet == numberCouple * 2) {
						break;
					}
				}
			}			
		}
		return cardNeedOpenList;
	}

	/// <summary>
	/// Mo tat ca cac la bai co so luong nhiu nhat
	/// </summary>
	private IEnumerator OpenCardsWithHighestNumber () 
	{
		// Duyet tat ca card tren man hinh va dem so luong
		Dictionary<int ,int> numberCardEachTypeDic = new Dictionary<int, int> ();

		foreach (Transform c in this._cardOnScreen) {
			CardScript cScript = c.GetComponent<CardScript> ();
			int numberCard = 0;
			if (numberCardEachTypeDic.ContainsKey((int)cScript.cardType)) {
				numberCard = numberCardEachTypeDic[(int)cScript.cardType];
			}
			numberCard ++;
			numberCardEachTypeDic[(int)cScript.cardType] = numberCard;
		}
		
		// Tim ra loai co so luong nhieu nhat
		int typeWithMaxValue = 0;
		int maxValue = -1;
		foreach(int key in numberCardEachTypeDic.Keys) {
			if (numberCardEachTypeDic[key] > maxValue) {
				maxValue = numberCardEachTypeDic[key];
				typeWithMaxValue = key;
			}
		}

		CardType type = (CardType)typeWithMaxValue;
		print ("type : " + type.ToString ());

		// Lay tat ca card co type la keyWithMaxValue
		ArrayList cardNeedOpenList = new ArrayList ();
		foreach (Transform c in this._cardOnScreen) {
			CardScript cScript = c.GetComponent<CardScript> ();
			if ((int)cScript.cardType == typeWithMaxValue) {
				cardNeedOpenList.Add(c);
			}
		}

		// Lat tat ca card 
		foreach (Transform c in cardNeedOpenList) {
			CardScript cScript = c.GetComponent<CardScript> ();
			if ((int)cScript.cardType == typeWithMaxValue) {
				FlipCardScript flipPrevScript = c.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);
			}
		}
		yield return new WaitForSeconds(0.5f);	

		// Animation exit tat
		for (int i = 0; i < cardNeedOpenList.Count; i ++) {
			Transform  c = (Transform)cardNeedOpenList [i];					
			CardScript cScript = c.GetComponent<CardScript> ();
			
			// Cong diem
			this._playGameData.score += cScript.card.point;

			// Luu thong tin
			this._playGameData.CollectCard(cScript.cardType,2);

			// Animation and auto destroy when finish
			Animator animator = c.GetComponent<Animator> ();
			animator.SetTrigger("exit");
			
			// Remove from list
			cardNeedOpenList.Remove(c.transform);
			this._cardOnScreen.Remove(c.transform);
			i --;
		}

		yield return new WaitForSeconds(0.2f);
		StartCoroutine(this.CheckCardOnScreenAndInitNextRoundIfNeed ());
	}

	/// <summary>
	/// Ramdom card tren man hinh cho moi vong choi
	/// </summary>
	/// <returns>The round.</returns>
	private IEnumerator InitRound () 
	{
		// So luong object can ve
		int numberOfCol = this._region.numberOfCol;
		int numberOfRow = this._region.numberOfRow;
		
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
		//Dictionary<string,int> cardDic = new Dictionary<string, int> ();

		// loai card : so luong
		Dictionary<int, int> numberCardToRandomWithTypeKey = this._region.GetCards (0);

		// Random card thanh cac the hien 
		this._cardOnScreen = new ArrayList ();
		foreach (int key in numberCardToRandomWithTypeKey.Keys) {
			int numberCardToRandom = (int)numberCardToRandomWithTypeKey[key];
			for (int i = 0; i < numberCardToRandom ; i ++) {
				var card = Instantiate(cardPrefab) as Transform;
				// Set type
				CardScript cardScript = card.GetComponent<CardScript>();
				if (cardScript != null) {
					cardScript.cardType = (CardType)key;			
					this._cardOnScreen.Add(card);
				} else {
					print ("card script is null");
				}
			}
			CardType type = (CardType)key;
			DebugScript.AddText(type.ToString() + " : " + numberCardToRandom.ToString() + " card");
		}

		// Thay doi thu tu vi tri cua card trong mang
		for (int i = 0; i < 100; i ++) {
			int index1 = Random.Range(0,this._cardOnScreen.Count);
			int index2 = Random.Range(0,this._cardOnScreen.Count);
			var object1 = this._cardOnScreen[index1];
			var object2 = this._cardOnScreen[index2];
			this._cardOnScreen[index1] = object2;
			this._cardOnScreen[index2] = object1;
		}
		
		//B3: va cac card tu trai sang phai, tu tren xuong doi
		for (int i = 0; i < this._cardOnScreen.Count;  i ++) {
			Transform card = (Transform)this._cardOnScreen[i];
			
			int indexCol = i % numberOfCol;
			
			// Tinh vi tri hien thi theo truc x
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			// card nam o top ben ngoai man hinh theo truc y
			float posY = 0 + screenHeight/2 + cardHeight/2;

			card.transform.position = new Vector3 (posX, posY, 0);
		}
		// Animation drop down
		for (int i = 0; i < this._cardOnScreen.Count; i ++) {
			Transform card = (Transform)this._cardOnScreen[i];
			
			int indexCol = i % numberOfCol;
			int indexRow = i / numberOfCol;
			
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			float posY = (float)(topLeftOfFirstCard.y  - indexRow * (cardHeight + 0.1));

			TweenParms parms = new TweenParms().Prop("position", new Vector3(posX,posY,0)).Ease(EaseType.EaseOutBack).Delay((float)0.1*i);
			HOTween.To (card, 0.5f, parms);
		}
		yield return new WaitForSeconds ((float)0.1*this._cardOnScreen.Count);

		// Bat dau tinh thoi gian choi
		TimerScript.StartTimer ();

		// Cho phep user co the choi
		SceneScript.EnableToTouch = true;
	}
}
