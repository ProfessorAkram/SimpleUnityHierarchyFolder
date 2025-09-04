/*******************************************************************
* COPYRIGHT       : 2025 Professor Akram
* PROJECT         : Hierarchy Folder Unity Editor Extension
* FILE NAME       : HierarchyFolderEditor.cs
* DESCRIPTION     : Provides a Unity Editor interface for managing HierarchyFolder objects.
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author         | Comments
* ---------------------------------------------------------------------------
* 2025/09/03        | Professor Akram | Created script with creation menu
*
/*******************************************************************/
using UnityEngine;
using UnityEditor;

namespace ProfessorAkram.SimpleUnityHierarchyFolder
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(HierarchyFolder))]
    public class HierarchyFolderEditor : Editor
    {
        //Context menu item to create a new HierarchyFolder
        [MenuItem("GameObject/Create Hierarchy Folder", false, 00)]
        private static void CreateHierarchyFolder(MenuCommand menuCommand)
        {
            // Create a new "folder" GameObject with the HierarchyFolder component
            GameObject folder = new GameObject("Hierarchy Folder");
            folder.AddComponent<HierarchyFolder>();

            // Parent it under the selected object, if any
            if (menuCommand.context is GameObject parent)
            {
                folder.transform.SetParent(parent.transform);
            }

            // Register undo and select the new folder
            Undo.RegisterCreatedObjectUndo(folder, "Create Hierarchy Folder");
            Selection.activeObject = folder;
        }//end CreateHierarchyFolder()

        //Hide the transform tools when a HierarchyFolder is enabled
        void OnEnable()
        {
            Tools.hidden = true;
        }//end OnEnable()

        // Reset transform on disable
        void OnDisable()
        {
            Tools.hidden = false;
        }//end On Disable

    }//end HierarchyFolderEditor

}//end Namespace