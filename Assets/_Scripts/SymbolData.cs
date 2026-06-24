using UnityEngine;

[CreateAssetMenu(fileName = "NewSymbol", menuName = "SlotGame/Symbol Data")]
public class SymbolData : ScriptableObject
{
    public string symbolName;
    public Sprite sprite;      
    public Color color = Color.white;
    public int payoutValue;
}