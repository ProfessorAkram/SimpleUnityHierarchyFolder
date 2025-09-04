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
* 2025/09/03        | Professor Akram | Created script and implmented color selection
*
/*******************************************************************/
using UnityEngine;

namespace ProfessorAkram.HierarchyFolder
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
            Brown
        }

        // Array of color values to map to ColorName enum
        private static readonly Color[] colorValues = new Color[]
        {
            new Color(0.9f, 0.8f, 0.2f),   // Bright Yellow
            new Color(1.0f, 0.5f, 0.1f),   // Vivid Orange
            new Color(0.9f, 0.2f, 0.2f),   // Strong Red
            new Color(1.0f, 0.5f, 0.6f),   // Vibrant Pink
            new Color(0.8f, 0.3f, 0.8f),   // Vivid Magenta
            new Color(0.6f, 0.4f, 0.9f),   // Bright Purple
            new Color(0.3f, 0.6f, 1.0f),   // Vibrant Blue
            new Color(0.2f, 0.9f, 0.9f),   // Bright Cyan
            new Color(0.2f, 0.8f, 0.7f),   // Vibrant Turquoise
            new Color(0.3f, 0.8f, 0.3f),   // Bright Green
            new Color(0.5f, 0.6f, 0.2f),    // Olive 
            new Color(0.6f, 0.4f, 0.2f)   // Brown 
        };




        [Tooltip("The text color for the folder game object")]
        public ColorName folderColor = ColorName.Yellow;

        // Returns the a color value maping the folderColor enum to it's correpsonding color value
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

}//end name space