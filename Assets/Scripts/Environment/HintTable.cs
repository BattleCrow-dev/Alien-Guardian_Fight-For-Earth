using UnityEngine;

public class HintTable : MonoBehaviour
{
    [TextArea()]
    [SerializeField] private string _hintText;

    public string GetHintText()
    {
        return _hintText;
    }
}
