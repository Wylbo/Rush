///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 06/11/2019 10:06
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Common {
    
    public class ColorChanger : MonoBehaviour {
        public enum EColor {
            Cyan,
            Yellow,
            Pink,
            Blue,
            Red
        }

        private MaterialPropertyBlock block;

        public MaterialPropertyBlock ChangeColor(EColor color) {
            Color _color = Color.black;
            if (color == EColor.Cyan) _color = Color.cyan;
            else if (color == EColor.Pink) _color = Color.magenta;
            else if (color == EColor.Yellow) _color = Color.yellow;
            else if (color == EColor.Red) _color = Color.red;
            else if (color == EColor.Blue) _color = Color.blue;

            _color.a = 0;


            block = new MaterialPropertyBlock();
            //block.set
            block.SetColor("_Color", _color);
            block.SetColor("_EmissionColor", _color);

            return block;
        }

    }
}