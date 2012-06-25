//
//  JDRTreeNode.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

import java.io.File;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

public class JDRTreeNode implements Comparable {
    private JDRTreeNode nodeParent;
    private JDRTreeNodeData nodeData;
    private Collection nodeChildren;
    
    public static JDRTreeNode treeNodeWithData(JDRTreeNodeData data) {
        return new JDRTreeNode(data, null);
    }
    
    JDRTreeNode(JDRTreeNodeData data, JDRTreeNode parent) {
        this.setNodeData(data);
        this.setNodeParent(parent);
        this.setNodeChildren(new LinkedList());
    }

    public JDRTreeNode getNodeParent() {
        return nodeParent;
    }

    private void setNodeParent(JDRTreeNode nodeParent) {
        this.nodeParent = nodeParent;
    }

    public JDRTreeNodeData getNodeData() {
        return nodeData;
    }

    public void setNodeData(JDRTreeNodeData nodeData) {
        this.nodeData = nodeData;
    }

    public Collection getNodeChildren() {
        return nodeChildren;
    }

    private void setNodeChildren(Collection nodeChildren) {
        this.nodeChildren = nodeChildren;
    }
    
    private void makeVirtual() {
        File dir = new File(nodeData.makeVirtual());
        String[] children = dir.list();
        
        for (int i = 0; i < children.length; i++) {
            JDRTreeNode child = JDRTreeNode.treeNodeWithData(
                                    JDRTreeNodeData.nodeDataWithPath(new File(dir, children[i]).getAbsolutePath()));
                
            nodeChildren.add(child);
            child.setNodeParent(this);            
        }
    }
    
    public void addChild(JDRTreeNode child) {
        if (!nodeData.isVirtual()) {
            makeVirtual();
        }
        
        nodeChildren.add(child);
        child.setNodeParent(this);
    }
    
    public void addChildren(Collection children) {
        for (Iterator it = children.iterator(); it.hasNext();) {
            Object elem = it.next();
            if( elem instanceof JDRTreeNode ) {
                JDRTreeNode child = (JDRTreeNode)elem;
                nodeChildren.add(child);
                child.setNodeParent(this);
            }
        }
    }
    
    public void removeChild(JDRTreeNode child) {
        nodeChildren.remove(child);
    }
    
    public void removeFromParent() {
        nodeParent.removeChild(this);
    }
    
    public int compareTo(Object o) {
        return nodeData.compareTo(o);
    }
}
