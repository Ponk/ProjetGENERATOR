/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.gen.businnes.logic;

import business.domain.Document;
import com.gen.data.cad.CAD;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.ObjectMessage;

/**
 *
 * @author Hubiquitos
 */
@MessageDriven(mappedName = "jms/DocumentQueue", activationConfig = {
    @ActivationConfigProperty(propertyName = "destinationType", propertyValue = "javax.jms.Queue")
})
public class DocumentProcessor implements MessageListener {

    public Connection connect;
    public Statement stmt;
    public ResultSet rs;

    public int FrenchWords = 0;
    public double tauxConfiance = 0;
    public String DocName;
    public String DocEmail;
    public String DocContent;
    public String DocKey;
    public String[] words;
    
    public CAD cad = new CAD("jdbc:mysql://localhost/dictionnairebdd", "root", "");

    public DocumentProcessor() throws SQLException, ClassNotFoundException {

    }

    /*private void LoadDictionnary() throws SQLException, ClassNotFoundException
     {
     this.cad = new CAD("jdbc:mysql://localhost:8080/dictionnairebdd", "root", "");

     cad.OpenConnection();
        
     //PreparedStatement ps = CAD.connect.prepareStatement("SELECT MOT_DICTIONNAIRE FROM t_dictionnaire WHERE ID_DICTIONNAIRE = ?");
     Statement ps = CAD.connect.createStatement();
     //ps.setInt(1, 18);
     //CAD.rs = ps.executeQuery();
     CAD.rs = ps.executeQuery("SELECT MOT_DICTIONNAIRE FROM t_dictionnaire WHERE ID_DICTIONNAIRE = 18");
     while(CAD.rs.next())
     {
     System.out.println(CAD.rs.getString("MOT_DICTIONNAIRE"));
     }
     cad.CloseConnection();
     }*/
    
    @Override
    public void onMessage(Message message) {

        if (message instanceof ObjectMessage) {
            try {
                ObjectMessage objMsg = (ObjectMessage) message;
                Document document = (Document) objMsg.getObject();

                connect = cad.OpenConnection();
                
                DocName = document.getName();
                DocKey = document.getKey();
                DocContent = document.getContent();
                
                words = DocContent.split("\\s+");
                for (String word : words) {
                    stmt = connect.createStatement();
                    rs = stmt.executeQuery("SELECT MOT_DICTIONNAIRE FROM t_dictionnaire WHERE MOT_DICTIONNAIRE LIKE '"+ word +"'" );
                    while (rs.next()) {
                        FrenchWords++;
                    }
                    
                    if(isEmail(word))
                    {
                        DocEmail = word;
                    }
                }
                connect.close(); 

                tauxConfiance = (100 * FrenchWords/ words.length);
                
                System.out.println("Mots Français: "+FrenchWords);
                System.out.println("Taux de confiance: "+tauxConfiance+" %");
                System.out.println("Mail du terroriste: "+DocEmail);
                
                SendResults(DocContent, DocKey, DocEmail);
                
                System.out.println("le document " + document + " va être retiré de la queue");
            } catch (JMSException | ClassNotFoundException | SQLException ex) {
                Logger.getLogger(DocumentProcessor.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
    
    private boolean isEmail(String Email)
    {
        return Pattern.matches("^[_a-z0-9-]+(\\.[_a-z0-9-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)+$", Email);
    }

    public void SendResults(String DocContent, String DocKey, String DocEmail)
    {
        
    }
}
