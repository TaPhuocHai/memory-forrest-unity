﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween.Plugins;
using Holoville.HOTween;

public class SceneScript : MonoBehaviour {

	public Transform cardPrefab;

	private GameObject prevCardOpen;

	static public bool enableToTouch;
	
	void Start () {
		// Init HOTWeen
		HOTween.Init ();

		// Init round
		this.InitRound ();
	}

	void Update () {
	}

	public void OpenCard (GameObject card) {
		enableToTouch = false;
		if (prevCardOpen == null) {
			prevCardOpen = card;
			enableToTouch = true;
		} else {
			StartCoroutine(WaitAndCheckOpenCard(card));
		}
	}
	
	IEnumerator WaitAndCheckOpenCard(GameObject card) {
		yield return new WaitForSeconds(0.65f);

		if (prevCardOpen != null) {
			CardScript prevScript = prevCardOpen.GetComponent<CardScript> ();				
			CardScript cardScript = card.GetComponent<CardScript> ();
			
			if (prevScript.cardType == cardScript.cardType) {
				Animator cardAnimator = card.GetComponent<Animator> ();
				cardAnimator.SetTrigger("exit");
				Animator prevAnimator = prevCardOpen.GetComponent<Animator> ();
				prevAnimator.SetTrigger("exit");

				// Add point				
			} else {
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

	private void InitRound () {
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
		
		// Random card thanh cac cap
		ArrayList cardToShow = new ArrayList ();
		for (int i = 0; i < numberOfObjectToDraw/2;  i ++) {
			
			// Random type
			CardType type = (CardType)Random.Range(0,(int)CardType.VioletButterfly);
			
			// ---------------------------------------
			for (int j = 0; j < 2 ; j ++) {
				var card = Instantiate(cardPrefab) as Transform;
				// Set type
				CardScript cardScript = card.GetComponent<CardScript>();
				cardScript.cardType = type;			
				cardToShow.Add(card);
			}
		}
		
		// Thay doi thu tu vi tri cua card trong mang
		for (int i = 0; i < 100; i ++) {
			int index1 = Random.Range(0,cardToShow.Count);
			int index2 = Random.Range(0,cardToShow.Count);
			var object1 = cardToShow[index1];
			var object2 = cardToShow[index2];
			cardToShow[index1] = object2;
			cardToShow[index2] = object1;
		}
		
		//B3: va cac card tu trai sang phai, tu tren xuong doi
		for (int i = 0; i < cardToShow.Count;  i ++) {
			Transform card = (Transform)cardToShow[i];
			
			int indexCol = i % numberOfCol;
			
			// Tinh vi tri hien thi theo truc x
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			// card nam o top ben ngoai man hinh theo truc y
			float posY = 0 + screenHeight/2 + cardHeight/2;
			card.transform.position = new Vector3 (posX, posY, 0);
		}
		// Animation dropdow
		for (int i = 0; i < cardToShow.Count; i ++) {
			Transform card = (Transform)cardToShow[i];
			
			int indexCol = i % numberOfCol;
			int indexRow = i / numberOfCol;
			
			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			float posY = (float)(topLeftOfFirstCard.y  - indexRow * (cardHeight + 0.1));
			
			TweenParms parms = new TweenParms().Prop("position", new Vector3(posX,posY,0)).Ease(EaseType.EaseOutBack).Delay((float)0.1*i);
			HOTween.To (card, 0.5f, parms);
		}
		
		SceneScript.enableToTouch = true;
	}

	public void ExitToMap () {
		Application.LoadLevel ("Map");
	}
}
