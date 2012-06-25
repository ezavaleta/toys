//
//  JDRFileNodeData.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

public class JDRFileNodeData extends JDRTreeNodeData {
    
    JDRFileNodeData(String path, String name) {
        this.path = path;
        this.name = name;
    }

    public boolean isExpandable() {
        return false;
    }

    public boolean isVirtual() {
        // ONLY ISO/JOLIET ARE SUPPORTED AND THEY
        // DONT ALLOW LINKS, SO THERE ARE NO VIRTUAL FILES
        return false;
    }
    
    public String getKind() {
        return "Real File";
    }
}
