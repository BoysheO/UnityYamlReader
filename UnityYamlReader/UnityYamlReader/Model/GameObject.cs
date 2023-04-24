using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UnityYamlReader.Model
{
    public class GameObject
    {
        public int? m_ObjectHideFlags { get; set; }
        public UObject? m_CorrespondingSourceObject { get; set; }
        public UObject? m_PrefabInstance { get; set; }
        public UObject? m_PrefabAsset { get; set; }
        public int? serializedVersion { get; set; }
        public int? m_Layer { get; set; }
        public string? m_Name { get; set; }
        public string? m_TagString { get; set; }
        public UObject? m_Icon { get; set; }
        public int? m_NavMeshLayer { get; set; }
        public int? m_StaticEditorFlags { get; set; }
        public int? m_IsActive { get; set; }
        public UObject[]? m_Component { get; set; }
    }
}
