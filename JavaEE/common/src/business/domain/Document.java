/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package business.domain;

import java.io.Serializable;

/**
 *
 * @author Hubiquitos
 */
public class Document implements Serializable {
    
    private String name;
    private String content;
    private String key;
    
    public Document(String name, String content, String key)
    {
        this.name = name;
        this.content = content;
        this.key = key;
    }
    
    public String getName()
    {
        return this.name;
    }
    
    public String getContent()
    {
        return this.content;
    }
    
    public String getKey()
    {
        return this.key;
    }
}
