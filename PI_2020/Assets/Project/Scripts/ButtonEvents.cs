using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour {

	public RectTransform arrow;

	public void ChangeToWhite (Text text) {

		text.color = Color.white;
	}

	public void ChangeToBlack (Text text) {

		text.color = Color.black;
	}

	public void ChangeArrowY (RectTransform button) {

		arrow.position = new Vector2 (arrow.position.x, button.position.y);
	}
}
