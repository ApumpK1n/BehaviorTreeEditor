using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class BTEditorCreateNodeWindow : ISearchWindowProvider
{
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        throw new System.NotImplementedException();
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        return true;
    }
}
