///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:13
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Common {
	public static class MathTools {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="verticalAngle"></param>
        /// <param name="horizontalAngle"></param>
        /// <returns></returns>
        public static Vector3 SphericalToCarthesian(float distance, float verticalAngle, float horizontalAngle) {
            return new Vector3(distance * Mathf.Cos(verticalAngle) * Mathf.Cos(horizontalAngle),
                               distance * Mathf.Sin(verticalAngle),
                               distance * Mathf.Cos(verticalAngle) * Mathf.Sin(horizontalAngle));
        }

    }
}