/*******************************************************************
* COPYRIGHT       : 2025 Professor Akram
* PROJECT         : Hierarchy Folder Unity Editor Extension
* FILE NAME       : HierarchyFolder.cs
* DESCRIPTION     : Component that allows a GameObject to function as a folder in the Unity hierarchy,
enabling organization with configurable colors and labels.
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author         | Comments
* ---------------------------------------------------------------------------
* 2025/09/03        | Professor Akram | Created script and implemented color selection
*
/*******************************************************************/
using UnityEngine;

namespace ProfessorAkram.SimpleUnityHierarchyFolder
{

    public class HierarchyFolder : MonoBehaviour
    {

        // Enum for folder color names
        public enum ColorName
        {
            Yellow,
            Orange,
            Red,
            Pink,
            Magenta,
            Purple,
            Blue,
            Cyan,
            Turquoise,
            Green,
            Olive,
            Brown,
            Silver,
            Gold
        }

        // Array of color values to map to ColorName enum
        private static readonly Color[] colorValues = new Color[]
        {
            new Color(0.90f, 0.80f, 0.20f),   // Bright Yellow
            new Color(1.00f, 0.50f, 0.10f),   // Vivid Orange
            new Color(0.90f, 0.20f, 0.20f),   // Strong Red
            new Color(1.00f, 0.50f, 0.60f),   // Vibrant Pink
            new Color(0.80f, 0.30f, 0.80f),   // Vivid Magenta
            new Color(0.60f, 0.40f, 0.90f),   // Bright Purple
            new Color(0.25f, 0.70f, 1.00f),   // Vibrant Blue
            new Color(0.20f, 0.90f, 0.90f),   // Bright Cyan
            new Color(0.20f, 0.80f, 0.70f),   // Vibrant Turquoise
            new Color(0.30f, 0.80f, 0.30f),   // Bright Green
            new Color(0.50f, 0.60f, 0.20f),   // Olive 
            new Color(0.60f, 0.40f, 0.20f),   // Brown
            new Color(0.75f, 0.75f, 0.75f),   // Silver 
            new Color(0.70f, 0.55f, 0.10f)    // Gold 
        };



        [Tooltip("The text color for the folder game object")]
        public ColorName folderColor = ColorName.Yellow;

        // Returns a color value mapping the folderColor enum to its corresponding color value
        public Color GetColor()
        {
            return colorValues[(int)folderColor];
        }

        //Called automatically by Unity when the script is loaded or a value is changed in the inspector.
        private void OnValidate()
        {
            transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
        }


    }//end HierarchyFolder()

}//end namespace
