using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stats to control various aspects of player movement, items, and powerups.
[CreateAssetMenu(fileName = "Data", menuName = "Player/Skins", order = 2)]
public class SkinIndexs : ScriptableObject {

	public int outfitIndex = 0;
	public int hatIndex = 0;
	public int lanceIndex = 0;

	public SkinIndexs(int outfitIndex, int hatIndex, int lanceIndex){
		this.outfitIndex = outfitIndex;
		this.hatIndex = hatIndex;
		this.lanceIndex = lanceIndex;
	}


}
