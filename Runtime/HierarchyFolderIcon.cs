/*******************************************************************
* COPYRIGHT       : 2025 Professor Akram
* PROJECT         : Hierarchy Folder Unity Editor Extension
* FILE NAME       : HierarchyFolderIcon.cs
* DESCRIPTION     : Handles the rendering of folder icons and text in the Unity hierarchy.
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author         | Comments
* ---------------------------------------------------------------------------
* 2025/09/03        | Professor Akram | Created script and OnHierarchyGUI.
* 2025/09/04        | Professor Akram | Refactored script for optimization by breaking 
methods into smaller, more focused functions.
* 2025/10/20        | Professor Akram | Fix build issue caused by InitializeOnLoad in editor scripts.
*
/*******************************************************************/
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace ProfessorAkram.SimpleUnityHierarchyFolder
{
#if UNITY_EDITOR

    // Static constructor to subscribe to the hierarchy callback
    [InitializeOnLoad]
    public static class HierarchyFolderIcon
    {

        //Constructor method
        static HierarchyFolderIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        /// <summary>
        /// Callback method used by Unity to customize how GameObjects are drawn in the Hierarchy window.
        /// </summary>
        /// <param name="instanceID">
        /// The unique instance ID of the object being drawn in the Hierarchy row.
        /// </param>
        /// <param name="selectionRect">
        /// The rectangle area in the Hierarchy window where the row will be drawn.
        /// </param>
        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            // Get the GameObject (folder) associated with this instance 
            GameObject thisFolder = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (thisFolder == null) return;

            //Check for the HierarchyFolder component
            HierarchyFolder folderComponent = thisFolder.GetComponent<HierarchyFolder>();
            if (folderComponent == null) return;

            //Get the state of the Hierarchy row
            RowState state = GetRowState(selectionRect, instanceID);

            // Determine background color
            Color bgColor = GetHierarchyRowBackground(state);

            // Determine label text color
            Color txtColor = GetHierarchyRowTextColor(state, folderComponent);

            // Draw a background rectangle to hide the default icon with the default editor background color 
            EditorGUI.DrawRect(selectionRect, bgColor);

            DrawFolderName(selectionRect, txtColor, thisFolder);
            DrawFolderIcon(selectionRect, thisFolder);

        }//end OnHierarchyGUI()

        // Current state of a hierarchy row.
        private enum RowState
        {
            Even,
            Odd,
            Hovered,
            Selected
        }

        /// <summary>
        /// Determines the current state of a hierarchy row based on selection and mouse hover.
        /// </summary>
        /// <param name="rect">The rectangle area of the row in the hierarchy.</param>
        /// <param name="instanceID">The instance ID of the GameObject for this row.</param>
        /// <returns>A <see cref="RowState"/> indicating whether the row is normal, hovered, or selected.</returns>
        private static RowState GetRowState(Rect selectionRect, int instanceID)
        {
            if (Selection.activeInstanceID == instanceID)
                return RowState.Selected;

            if (selectionRect.Contains(Event.current.mousePosition))
                return RowState.Hovered;

            int row = (int)(selectionRect.y / selectionRect.height);
            return (row % 2 == 0) ? RowState.Even : RowState.Odd;
        }//end RowState


        /// <summary>
        ///  Method to determine the background color (approximation) of the hierarchy window based on skin
        /// </summary>
        /// <param name="state">The current state of the row (Selected, Hovered, Even, or Odd).</param>
        private static Color GetHierarchyRowBackground(RowState state)
        {
            //Set color based on row state
            switch (state)
            {
                case RowState.Selected:
                    return new Color(0.24f, 0.49f, 0.91f, 1f); // Pro & Light skin selection color
                case RowState.Hovered:
                    return EditorGUIUtility.isProSkin
                        ? new Color(0.3f, 0.3f, 0.3f, 1f) // Pro skin selection color
                        : new Color(0.8f, 0.8f, 0.8f, 1f);// Light skin selection color
                case RowState.Even:
                    return EditorGUIUtility.isProSkin
                        ? new Color(0.219f, 0.219f, 0.219f) // Pro skin selection color
                        : new Color(0.941f, 0.941f, 0.941f);// Light skin selection color
                case RowState.Odd:
                    return EditorGUIUtility.isProSkin
                        ? new Color(0.239f, 0.239f, 0.239f) // Pro skin selection color
                        : new Color(0.855f, 0.855f, 0.855f);// Light skin selection color
                default:
                    return Color.clear;
            }

        }//end GetHierarchyRowBackground()


        /// <summary>
        /// Determines the text color for a hierarchy row based on its current state.
        /// Selected and hovered rows use fixed colors for readability, 
        /// while normal rows use the color defined by the associated folder/component.
        /// </summary>
        /// <param name="state">The current state of the row (Selected, Hovered, Even, or Odd).</param>
        /// <param name="folderComponent">Reference to the Hierarchy Folder component on the game object.</param>
        /// <returns>The appropriate <see cref="Color"/> to use for the row's text.</returns>
        private static Color GetHierarchyRowTextColor(RowState state, HierarchyFolder folderComponent)
        {
            switch (state)
            {
                case RowState.Selected:
                    return EditorGUIUtility.isProSkin
                        ? new Color(1f, 1f, 1f) // white text for selected in Pro skin
                        : new Color(0f, 0f, 0f); // black text for selected in Light skin

                case RowState.Hovered:
                    return EditorGUIUtility.isProSkin
                        ? new Color(1f, 1f, 1f) // white text on hover in Pro
                        : new Color(0f, 0f, 0f); // black text on hover in Light

                case RowState.Even:
                case RowState.Odd:
                    // Use the color defined in the folder/component
                    return folderComponent.GetColor();

                default:
                    return Color.white;
            }
        }//end GetHierarchyRowTextColor()



        /// <summary>
        /// Draws the folder name in the Unity Hierarchy window with a custom style.
        /// </summary>
        /// <param name="selectionRect">
        /// The rectangle area in the Hierarchy window where the row for the GameObject is drawn.
        /// This determines the position and size of the label.
        /// </param>
        /// <param name="thisFolder">
        /// The <see cref="GameObject"/> representing the hierarchy folder whose name will be drawn.
        /// </param>
        private static void DrawFolderName(Rect selectionRect, Color txtColor, GameObject thisFolder)
        {
            //Create the label rectangle 
            Rect labelRect = new Rect(selectionRect.x + 20, selectionRect.y, selectionRect.width - 20, selectionRect.height);

            // Create a copy of the boldLabel style
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);

            // Set text color
            style.normal.textColor = txtColor;

            // Draw the label
            EditorGUI.LabelField(labelRect, thisFolder.name.ToUpper(), style);


        }

        /// <summary>
        /// Draws a folder icon in the Unity Hierarchy window for a folder GameObject.
        /// </summary>
        /// <param name="selectionRect">
        /// The rectangle area in the Hierarchy window representing the row
        /// where the icon should be drawn. Used to calculate icon position.
        /// </param>
        ///  /// <param name="thisFolder">
        /// The <see cref="GameObject"/> representing the hierarchy folder that the icon will be drawn for.
        /// </param>
        private static void DrawFolderIcon(Rect selectionRect,GameObject thisFolder)
        {
            // Define the size of the icon
            float iconSize = 18f; // Standard icon size
            float verticalCenter = selectionRect.y + (selectionRect.height - iconSize) * 0.5f; //Calculate vertical center

            // Align the iconRect to avoid overlapping with the row above
            Rect iconRect = new Rect(selectionRect.x, verticalCenter, iconSize, iconSize);

            // Get the default Unity folder icon
            Texture2D folderIcon = EditorGUIUtility.IconContent("Folder Icon").image as Texture2D;
            
            // Draw the folder icon
            if (folderIcon != null)
            {
                GUI.Label(iconRect, folderIcon);
            }//end if (icon != null)

            //Check if folder is also a prefab
            if (PrefabUtility.IsPartOfPrefabInstance(thisFolder))
            {
                DrawPrefabOverlay(selectionRect);
                
            }//end if(PrefabUtility)

        }//end DrawFolderIcon()

        
        /// <summary>
        /// Draws a small prefab icon overlay within the hierarchy item area.  
        /// Used to visually indicate that a "folder" GameObject is also a prefab instance.
        /// </summary>
        /// <param name="selectionRect">
        /// The rectangle representing the hierarchy item’s drawing bounds,  
        /// used to calculate where to position the prefab overlay icon.
        /// </param>        
        private static void DrawPrefabOverlay(Rect selectionRect)
        {
            //Calculate the size of the icon rectangle
            Rect prefabRect = new Rect(selectionRect.x + 8, selectionRect.y + 8, 8, 8);
            
            // Get the default Unity prefab icon 
            Texture2D  prefabIcon = EditorGUIUtility.IconContent("Prefab Icon").image as Texture2D;
            
            // Draw the prefab icon overlay
            GUI.DrawTexture(prefabRect, prefabIcon);
            
        }//end DrawPrefabOverlay
        

    }//end HierarchyFolderIcon class
    
#endif

}//end Namespace
