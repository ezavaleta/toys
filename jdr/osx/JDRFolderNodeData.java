//
//  JDRFolderNodeData.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

public class JDRFolderNodeData extends JDRTreeNodeData {

    JDRFolderNodeData(String path, String name) {
        this.path = path;
        this.name = name;
    }

    JDRFolderNodeData(String name) {
        this.name = name;
    }

    public boolean isExpandable() {
        return true;
    }

    public boolean isVirtual() {
        return (path == null);
    }
    
    public String getKind() {
        if( path == null ) {
            return "Virtual Folder";
        }
        else {
            return "Real Folder";
        }
    }
}
