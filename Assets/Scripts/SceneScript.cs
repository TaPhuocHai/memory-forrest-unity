using UnityEngine;
using System.Collections;

public class SceneScript : MonoBehaviour {

	ArrayList sprites;


	// Use this for initialization
	void Start () {

		// Load animate sprite
		this.LoadAnimalSprite ();

		// So luong object can ve
		int numberOfCol = 4;
		int numberOfRow = 4;
		int numberOfObjectToDraw = numberOfCol * numberOfRow;

		// Load card game
		GameObject cardGame = Resources.LoadAssetAtPath <GameObject> ("Assets/Prefab/CardPrefab.prefab");				

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
		float cardWidth = cardGame.transform.renderer.bounds.size.x;
		float cardHeight = cardGame.transform.renderer.bounds.size.y;

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

		//B3: va cac card tu trai sang phai, tu tren xuong doi
		for (int i = 0; i < numberOfObjectToDraw; i ++) {
			var card = Instantiate(cardGame) as GameObject;		
			card.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)] as Sprite;				
		
			int indexCol = i % numberOfCol;
			int indexRow = i / numberOfCol;

			float posX = (float)(topLeftOfFirstCard.x + indexCol * (cardWidth + 0.1)); 
			float posY = (float)(topLeftOfFirstCard.y  - indexRow * (cardHeight + 0.1));

			card.transform.position = new Vector3 (posX, posY, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadAnimalSprite () {
		sprites = new ArrayList();
		sprites.Add(Resources.Load("Textures/Animal/char1", typeof(Sprite)));
		sprites.Add(Resources.Load("Textures/Animal/char2", typeof(Sprite)));
		sprites.Add(Resources.Load("Textures/Animal/char3", typeof(Sprite)));
		sprites.Add(Resources.Load("Textures/Animal/char4", typeof(Sprite)));
	}
}
