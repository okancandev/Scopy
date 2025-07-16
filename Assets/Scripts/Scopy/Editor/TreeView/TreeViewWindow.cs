using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Collections.Generic;

namespace Okancandev.Scopy.Editor
{
    public class TreeViewWindow : EditorWindow
    {
        [MenuItem("Window/Custom TreeView")]
        public static void ShowWindow()
        {
            GetWindow<TreeViewWindow>("TreeView Example");
        }
    
        private TreeViewExample _treeView;
        private TreeViewState _treeViewState;
        private MultiColumnHeaderState _headerState;
    
        void OnEnable()
        {
            EditorApplication.update += AutoUpdateTree;
            
            if (_treeViewState == null)
                _treeViewState = new TreeViewState();
    
            var header = CreateHeader();
            _treeView = new TreeViewExample(_treeViewState, header);
        }

        private void OnDisable()
        {
            EditorApplication.update -= AutoUpdateTree;
        }

        private void AutoUpdateTree()
        {
            _treeView.Reload();
            //Repaint();
        }
    
        void OnGUI()
        {
            _treeView.OnGUI(new Rect(0, 0, position.width, position.height));
        }
        
        MultiColumnHeader CreateHeader()
        {
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Registered Type"),
                    width = 200,
                    minWidth = 200,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Service"),
                    width = 200,
                    minWidth = 200,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Id"),
                    width = 25,
                    minWidth = 25,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Tag"),
                    width = 200,
                    minWidth = 200,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Extra"),
                    width = 250,
                    minWidth = 50,
                    autoResize = true
                }
            };
    
            _headerState = new MultiColumnHeaderState(columns);
            return new MultiColumnHeader(_headerState);
        }
    }
}