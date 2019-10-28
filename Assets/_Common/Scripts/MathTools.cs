///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:13
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Common {
	public static class MathTools {
       
        /// <summary>
        /// convertit des coordonées spherique en carthesien
        /// </summary>
        /// <param name="distance">distance depuis le pivot</param>
        /// <param name="verticalAngle">angle vertical entre -pi/2 et pi/2</param>
        /// <param name="horizontalAngle">angle horizontal entre -pi et pi</param>
        /// <returns></returns>
        public static Vector3 SphericalToCarthesian(float distance, float verticalAngle, float horizontalAngle) {
            return new Vector3(distance * Mathf.Cos(verticalAngle) * Mathf.Cos(horizontalAngle),
                               distance * Mathf.Sin(verticalAngle),
                               distance * Mathf.Cos(verticalAngle) * Mathf.Sin(horizontalAngle));
        }

    }
}