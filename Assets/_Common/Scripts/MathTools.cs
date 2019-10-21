///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:13
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Common {
	public static class MathTools {
       
        /***
         * insert spherical coord, return carthesian coord
         ***/
        public static Vector3 SphericalToCarthesian(float distance, float verticalAngle, float horizontalAngle) {
            return new Vector3(distance * Mathf.Cos(verticalAngle) * Mathf.Cos(horizontalAngle),
                               distance * Mathf.Sin(verticalAngle),
                               distance * Mathf.Cos(verticalAngle) * Mathf.Sin(horizontalAngle));
        }

    }
}