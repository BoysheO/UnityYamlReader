using System;
using System.Collections.Generic;
using System.Text;

namespace UnityYamlReader.Model
{
    public class RectTransform
    {
        public UObject? m_CorrespondingSourceObject { get;set;}
        public UObject? m_PrefabInstance { get;set;}
        public UObject? m_PrefabAsset { get;set;}
        public UObject? m_GameObject { get;set;}
        public UObject[]? m_Children { get;set;}
        public UObject? m_Father { get;set;}
        public int? m_RootOrder { get;set;}
    }
}
