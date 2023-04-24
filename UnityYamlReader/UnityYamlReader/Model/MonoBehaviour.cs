using System;
using System.Collections.Generic;
using System.Text;

namespace UnityYamlReader.Model
{
    public class MonoBehaviour
    {
        public int? m_ObjectHideFlags { get;set;}
        public UObject? m_CorrespondingSourceObject { get;set;}
        public UObject? m_PrefabInstance { get;set;}
        public UObject? m_PrefabAsset { get;set;}
        public UObject? m_GameObject { get;set;}
        public int? m_Enabled { get;set;}
        public Script? m_Script { get;set;}

    }
}
