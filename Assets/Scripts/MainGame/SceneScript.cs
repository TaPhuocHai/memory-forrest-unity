using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using DG.Tweening;

public class SceneScript : MonoBehaviour 
{
	#region Singleton
	public static SceneScript Instance { get; private set;}
	#endregion

	public Transform cardPrefab;
	public Transform getScoreText;
	public Transform background;

	/// <summary>
	/// The enable to touch.
	/// Bien nay = true thi moi cho phep touch tren card
	/// </summary>
	static public bool EnableToTouch;

	#region Private variable

	// Danh sach card tren man hinh
	private List<Transform>  _cardOnScreen;
	// Card da duoc mo
	private GameObject _prevCardOpen;

	// Vung dat user dang choi
	private Region      _region;
	// Luu ket qua choi trong moi game
	private PlayGameData _playGameData;

	// Vong choi hien tai
	private int          _round;

#if UNITY_ANDROID
	private AdMobPlugin admob;
	private bool        admobLoaded;
#else
	private InterstitialAd interstitial;
#endif

	#endregion

	#region Game Cycle

	public SceneScript () 
	{
		SceneScript.Instance = this;
	}

	void Awake () {}

	void Start () 
	{
		DebugScript.AddText ("Add Text \n");
		// Init Region
		RegionType regionType = Player.currentRegionPlay;
		this._region = Region.Instance(regionType);

		// Init playGameData
		this._playGameData = PlayGameData.Instance;
		this._playGameData.regionType = regionType;

		// Init round
		StartCoroutine (this.InitRound (0.5f, true));

		AudioSource audioSource = this.gameObject.GetComponent<AudioSource> ();
		AudioClip clip = null;
		if (regionType == RegionType.KingdomOfRabbits) {
			clip = Resources.Load("Sounds/MontaukPoint") as AudioClip;  
		} else if (regionType == RegionType.Forest) {
			clip = Resources.Load("Sounds/SummerDay") as AudioClip;  
		} else if (regionType == RegionType.StoneMountain) {
			clip = Resources.Load("Sounds/Cattails") as AudioClip;  
		} else {
			clip = Resources.Load("Sounds/AtTheShore") as AudioClip;  
		}
		audioSource.clip = clip;
		audio.volume = 0.9f;


		// Change backgroud
		Sprite bgSprite = null;
		Vector3 bgScale = new Vector3 (0.95f, 0.95f, 1);
		if (regionType == RegionType.KingdomOfRabbits) {
			bgSprite = Resources.Load<Sprite>("Textures/MainGame/Map1");  
			bgScale = new Vector3 (1.2f, 1.2f, 1f);
		} else if (regionType == RegionType.Forest) {
			bgSprite = Resources.Load<Sprite>("Textures/MainGame/Map2");
		} else if (regionType == RegionType.StoneMountain) {
			bgSprite = Resources.Load<Sprite>("Textures/MainGame/Map3");
		} else {
			bgSprite = Resources.Load<Sprite>("Textures/MainGame/Map4");
		}
		this.background.localScale = bgScale;
		SpriteRenderer bgRenderer = this.background.GetComponent<SpriteRenderer> ();
		if (bgRenderer == null) {
			Debug.Log ("render is null");
		}
		if (bgSprite == null) {
			Debug.Log ("sprite null");
		}
		bgRenderer.sprite = bgSprite;

#if UNITY_ANDROID
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.AddComponent<AdMobPlugin> ();
		admob = camera.GetComponent<AdMobPlugin> ();
		admob.CreateBanner ("ca-app-pub-5151220756481063/6286731830", AdMobPlugin.AdSize.SMART_BANNER, false, Constant.kAdInterstitialId, false);
		admob.HideBanner ();
		admob.RequestInterstitial ();
		
		AdMobPlugin.InterstitialLoaded += HandleInterstitialLoaded;
#else
		// Init admob
		interstitial = new InterstitialAd(Constant.kAdInterstitialId);
		// Load the interstitial with the request.
		interstitial.LoadAd(createAdRequest());
#endif
	}

	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder().Build();
	}

	void HandleInterstitialLoaded ()
	{
#if UNITY_ANDROID
		admobLoaded = true;
#endif
	}

	void Update () {}

	#endregion

	#region Admob
	
	public void ShowFullAds()
	{
#if UNITY_ANDROID
		if (admobLoaded) {
			admob.ShowInterstitial ();
		}
#else
		if (interstitial.IsLoaded()) {
			interstitial.Show();
		}
#endif
	}

	#endregion

	#region Game navigation

	/// <summary>
	/// Exits to map.
	/// </summary>
	public void ExitToMap () 
	{
		Application.LoadLevel ("Map");
	}

	#endregion
	
	#region Open Card

	/// <summary>
	/// Opens the card - Tinh diem
	/// </summary>
	/// <param name="card">Card.</param>
	public void OpenCard (GameObject card) 
	{
		EnableToTouch = false;

		SoundEffects.Play (SoundEffectTypes.CardFlip);

		StartCoroutine(CheckOpenCard(card));
	}

	/// <summary>
	/// Check open card.
	/// </summary>
	/// <param name="card">Card.</param>
	IEnumerator CheckOpenCard(GameObject card) 
	{
		CardScript cardScript = card.GetComponent<CardScript> ();

		// Da mo 2 la bai
		if (_prevCardOpen != null) {

			// Can doi 1 khoang thoi gian 0.65s de card hien thi tren man hinh truoc khi kiem tra va flip hoac destroy no
			yield return new WaitForSeconds(0.65f);

			CardScript prevScript = _prevCardOpen.GetComponent<CardScript> ();				

			// Neu 2 card cung loai voi nhau
			if (prevScript.cardType == cardScript.cardType) {

				// ----------------------------------------------------------
				// Buoc 1 : kiem tra co cho phep lay diem va xoa 2 card nay ?

				// Play am thanh
				Card.PlaySound (cardScript.cardType);

				// Doi voi la bai thuong
				bool isNeedDestroyCard = true;
				bool isAllowAddPoint   = true;

				// Khi mo trung la wolf hoac wolf king
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

					this.ShowGetScoreText ("+ " + cardScript.card.point/2, prevScript.gameObject.transform.position);
					this.ShowGetScoreText ("+ " + cardScript.card.point/2, cardScript.gameObject.transform.position);

					//Debug.Log ("Get point : " + cardScript.card.point.ToString());
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

					//Debug.Log ("Card : " + cardScript.card.type.ToString());
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

				// Thay doi vi tri khi mo la wolf hoac wolfking
				if (cardScript.cardType == CardType.Wolf || cardScript.cardType == CardType.WolfKing) {
					this.CheckOpenWolfCard(card);
				}
				// Lap 3 cap bai ngau nhien tren man hinh
				else if (cardScript.cardType == CardType.BlueButterfly) {
					print ("Lat 3 cap bai");
					StartCoroutine(this.Open3CoupleCard ());
				}
				// Them 10s vao thoi gian choi
				else if (cardScript.cardType == CardType.RedButterfly) {
					print ("Them 10s");
					TimerManager.Instance.AddMoreTime (10.0f);
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
				else if (cardScript.cardType == CardType.Coins5) {
					Player.Coin += 5;
				}else if (cardScript.cardType == CardType.Coins10) {
					Player.Coin += 10;
				}else if (cardScript.cardType == CardType.Coins20) {
					Player.Coin += 20;
				}else if (cardScript.cardType == CardType.Coins50) {
					Player.Coin += 50;
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

				// Neu card da mo la soi
				if (cardScript.card.type == CardType.Wolf || cardScript.card.type == CardType.WolfKing) {

					SoundEffects.Play (SoundEffectTypes.Wolf);

					// Thuc hien kiem tra khi mo trung la soi
					// EnableToTouch se duoc enable trong ham CheckOpenWolfCard hoac sau do
					this.CheckOpenWolfCard(card);
				} else {
					EnableToTouch = true;
				}
			}
			_prevCardOpen = null;
		} 
		// Moi mo 1 la bai
		else {
			// Neu card da mo la soi
			if (cardScript.card.type == CardType.Wolf || cardScript.card.type == CardType.WolfKing) {

				// Can doi 1 khoang thoi gian 0.65s de card hien thi tren man hinh truoc khi kiem tra va flip hoac destroy no
				yield return new WaitForSeconds(0.65f);

				// flip card
				FlipCardScript flipPrevScript = card.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);

				// Thuc hien kiem tra khi mo trung la soi
				this.CheckOpenWolfCard(card);
			} 
			// Khong phai la soi
			else {
				_prevCardOpen = card;
			}
			// cho phep user tiep tuc chon card khac
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

				// Show score add point
				this.ShowGetScoreText ("+ " + cardGameScript.card.point/2, cardGameScript.gameObject.transform.position);

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

				// Play sound
				SoundEffects.Play (SoundEffectTypes.NewRound);

				// Init round
				StartCoroutine(this.InitRound (0.0f, false));
			} else {
				// Save total score
				Player.totalScore = Player.totalScore + this._playGameData.score;

				// Luu thong tin man choi
				this._playGameData.Save();

				// Stop timer
				TimerManager.Instance.Stop ();

				// show rescue popup
				RescuePopup.Instance.Show (Constant.kPopupAnimationDuraction);			
				this.enabled = false;
			}
		}
		EnableToTouch = true;
	}

	/// <summary>
	/// Checks the open wolf card.
	/// </summary>
	/// <param name="card">Card.</param>
	private void CheckOpenWolfCard(GameObject card) 
	{
		CardScript cardScript = card.GetComponent<CardScript> ();
		
		// Flip card Soi nay lai
		FlipCardScript flipCardScript = card.GetComponent<FlipCardScript> ();
		flipCardScript.FlipCard(false);
		
		// Neu la soi binh thuong
		if (cardScript.card.type == CardType.Wolf) {
			
			List<Transform> listChangePosition = new List<Transform>();
			
			// Thay doi vi tri cua 3 la bai
			if (this._cardOnScreen.Count <= 3) {
				listChangePosition = this._cardOnScreen;
			} else {
				// Random cac index
				List<int> indexRandomed = new List<int> ();
				// Can random du 3 vi tri
				while (indexRandomed.Count < 3) {
					int random = Random.Range(0,this._cardOnScreen.Count);
					// Kiem tra xem gia tri random da ton tai trong danh sach indexRandomed hay chua
					bool isExit = false;
					foreach (int i in indexRandomed) {
						if (i == random) {
							isExit = true;
							break;
						}
					}
					// Neu chua ton tai
					if (!isExit) {
						// Them moi random vao danh sach
						indexRandomed.Add (random);
					}
				}
				// Tao danh sach object
				foreach (int value in indexRandomed) {
					Transform cardObject = this._cardOnScreen[value];
					listChangePosition.Add (cardObject);
				}
			}
			// Goi ham thay doi vi tri
			this.SwapPositionObjects (listChangePosition);
		} 
		// Soi vui
		else {
			// Thay doi vi tri cua tat ca la bai
			this.SwapPositionObjects (this._cardOnScreen);
		}
		// cho phep user tiep tuc chon card khac
		EnableToTouch = true;
	}

	/// <summary>
	/// Doi mat sau cua 3 cap la bai bat ky
	/// </summary>
	private void ChangeFaceBack3CoupleCard () 
	{
		List<Transform> cardNeedOpenList = this.Get3CoupleCardOnScrene ();

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

		List<Transform> cardNeedOpenList = this.Get3CoupleCardOnScrene ();
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
	private List<Transform> Get3CoupleCardOnScrene () 
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

		List<Transform> cardNeedOpenList = new List<Transform> ();

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
		List<Transform> cardNeedOpenList = new List<Transform> ();
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

			// Show score add point
			this.ShowGetScoreText ("+ " + cScript.card.point, c.position);

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

	private void SwapPositionObjects (List<Transform> listObjects) 
	{
		if (listObjects.Count == 0 || listObjects == null) {
			return;
		}

		// B1 : tao ra 1 danh sach position
		List<Vector3> positions = new List<Vector3> ();
		for (int i = 0; i < listObjects.Count; i ++) {
			Transform card = listObjects[i];
			Vector3 pos = card.transform.position;
			positions.Add (new Vector3(pos.x, pos.y,pos.z));
		}

		// B2 : Thay doi vi tri
		// Neu chi co 3 card, thuc hien thay doi tay
		if (positions.Count <= 1) {
			// Khong lam gi ca
		} if (positions.Count == 2) {
			// Thay doi vi tri 2 phan tu
			Vector3 value1 = positions[0];
			Vector3 value2 = positions[1];
			positions[0] = value2;
			positions[1] = value1;
		} if (positions.Count == 3) {
			int firstIndex = Random.Range(1,positions.Count);

			Vector3 pos0 = positions[0];
			Vector3 pos1 = positions[1];
			Vector3 pos2 = positions[2];

			Vector3 posFirstObject = positions[firstIndex];
			positions[0] = posFirstObject;

			if (firstIndex == 1) {
				positions[1] = pos2;
				positions[2] = pos0;
			} else {
				positions[1] = pos0;
				positions[2] = pos1;
			}
		}
		// Chay random thay doi vi tri thu tu trong mang
		else {
			for (int i = 0; i < 100; i ++) {
				int index1 = Random.Range(0,positions.Count);
				int index2 = Random.Range(0,positions.Count);
				
				// Hoan doi vi tri indexPosition
				Vector3 value1 = positions[index1];
				Vector3 value2 = positions[index2];
				positions[index1] = value2;
				positions[index2] = value1;
			}
		}

		// B3 : Thuc hien di chuyen
		for (int i = 0; i < listObjects.Count; i ++) {
			Transform card = listObjects[i];
			Vector3 toPosition = positions[i];

			if (i == listObjects.Count - 1) {
				card.DOMove (toPosition, 0.6f).SetEase (Ease.OutQuint).OnComplete(SwapPositionObjectsComplete);
			} else {
				card.DOMove (toPosition, 0.6f).SetEase (Ease.OutQuint);
			}
		}
	}

	private void SwapPositionObjectsComplete () 
	{
		EnableToTouch = true;
	}

	private void ShowGetScoreText (string text, Vector3 position) 
	{
		Transform scoreText = Instantiate(this.getScoreText,position,Quaternion.identity) as Transform;

		GetScoreText scoreTextScript = scoreText.GetComponent<GetScoreText> ();
		scoreTextScript.label.guiText.text = text;
	}

	#endregion

	#region Start Game

	/// <summary>
	/// Reset man choi, choi lai
	/// </summary>
	public void ResetRound () 
	{
		this.enabled = true;
		
		Player.lastScore = this._playGameData.score;
		
		// Tao moi lop luu thong tin man choi
		this._playGameData.Reset ();
		
		this._round = 0;
		
		// Clear debug
		DebugScript.Clear ();
		
		// Destroy all card on screen
		for (int i = 0; i < this._cardOnScreen.Count; i ++) {
			Transform card = (Transform)this._cardOnScreen [i];
			Destroy (card.gameObject);
		}
		
		// Reset time
		TimerManager.Instance.Reset ();
		
		// Reset POINT
		this._playGameData.score = 0;
		
		// Init round
		StartCoroutine (this.InitRound (0.0f, true));
	}

	/// <summary>
	/// Ramdom card tren man hinh cho moi vong choi
	/// </summary>
	/// <returns>The round.</returns>
	private IEnumerator InitRound (float watingSecondTime, bool startTimer) 
	{
		yield return new WaitForSeconds(watingSecondTime);

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
		                                      edgeLeftBottomVector.y + (screenHeight - heighOfAllCard)/2  + heighOfAllCard - cardHeight/2 - cardHeight);

		// Luu thong tin so luong cac card duoc random
		//Dictionary<string,int> cardDic = new Dictionary<string, int> ();

		// loai card : so luong
		Dictionary<CardType, int> numberCardToRandomWithTypeKey = this._region.GetCards (0);

		// Random card thanh cac the hien 
		this._cardOnScreen = new List<Transform> ();
		foreach (CardType key in numberCardToRandomWithTypeKey.Keys) {
			int numberCardToRandom = (int)numberCardToRandomWithTypeKey[key];
			for (int i = 0; i < numberCardToRandom ; i ++) {
				var card = Instantiate(cardPrefab) as Transform;
				card.transform.parent = this.gameObject.transform;

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

			card.transform.position = new Vector3 (posX, posY, this.transform.position.z);
		}
		// Animation drop down
		for (int i = 0; i < this._cardOnScreen.Count; i ++) {
			Transform card = (Transform)this._cardOnScreen[i];
			
			int indexCol = i % numberOfCol;
			int indexRow = i / numberOfCol;
			
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			float posY = (float)(topLeftOfFirstCard.y  - indexRow * (cardHeight + 0.1));

			card.DOMove (new Vector3(posX,posY,this.transform.position.z), 0.5f).SetEase (Ease.OutBack).SetDelay((float)0.1*i);
		}
		yield return new WaitForSeconds ((float)0.1*this._cardOnScreen.Count);

		// Tu dong mo cac card Wolf
		StartCoroutine (AutoOpenWolfCardAndPlay(startTimer));
	}

	private IEnumerator AutoOpenWolfCardAndPlay (bool startNewTime)
	{
		List<Transform> wolfList = new List<Transform> ();
		foreach (Transform c in this._cardOnScreen) {
			CardScript cScript = c.GetComponent<CardScript> ();
			if (cScript.cardType == CardType.Wolf || 
			    cScript.cardType == CardType.WolfKing) {
				wolfList.Add (c);
			}
		}

		if (wolfList.Count != 0) {
			foreach (Transform c in wolfList) {
				FlipCardScript flipPrevScript = c.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard (false);
			}
			yield return new WaitForSeconds (0.5f);

			// Up lai
			foreach (Transform c in wolfList) {
				FlipCardScript flipPrevScript = c.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);
			}
		}

		if (startNewTime) {
			// Bat dau tinh thoi gian choi
			TimerManager.Instance.Start (Player.secondTimePlay, true);
		}
		
		// Play music backgroud
		PHMusicBackground.Instance.Play ();
		
		// Cho phep user co the choi
		SceneScript.EnableToTouch = true;
	}

	#endregion

	#region End Game

	public void OpenCardOnScreenAndEndGame ()
	{
		SceneScript.EnableToTouch = false;

		if (this._cardOnScreen.Count != 0) {
			foreach (Transform c in this._cardOnScreen) {
				// Open card
				FlipCardScript flipPrevScript = c.GetComponent<FlipCardScript> ();
				flipPrevScript.FlipCard(false);
			}
			StartCoroutine (DropdownCardOnScreen ());
		} else {
			// show time up and end game
			TimeUpPanel.Instance.Show ();
			this.enabled = false;
		}
	}

	IEnumerator DropdownCardOnScreen() 
	{
		yield return new WaitForSeconds (0.4f);

		// Dropdown
		for (int i = 0; i < this._cardOnScreen.Count; i ++) {
			Transform card = (Transform)this._cardOnScreen[i];
			Transform cardBack = card.FindChild ("CardBack");
			float cardHeight = cardBack.transform.renderer.bounds.size.y;

			Vector3 newPosition = new Vector3 ();
			newPosition.x = card.position.x;
			newPosition.z = card.position.z;
			newPosition.y = - PHUtility.WorldHeight/2 - cardHeight/2;

			float deplay = Random.Range (0.04f,0.1f);

			float time = Random.Range (0.35f,0.55f);
			card.DOMove (newPosition,time).SetEase (Ease.InCubic).SetDelay((float)deplay*i);
		}
		yield return new WaitForSeconds ((float)0.08*this._cardOnScreen.Count);

		// Show time up and end game
		TimeUpPanel.Instance.Show ();
		this.enabled = false;
	}

	#endregion
}
