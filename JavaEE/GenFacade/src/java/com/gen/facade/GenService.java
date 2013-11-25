/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package com.gen.facade;

import business.domain.Document;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.annotation.PostConstruct;
import javax.annotation.PreDestroy;
import javax.annotation.Resource;
import javax.ejb.EJBException;
import javax.jws.WebService;
import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.ejb.Stateless;
import javax.ejb.TransactionAttribute;
import javax.ejb.TransactionAttributeType;
import javax.jms.Connection;
import javax.jms.JMSException;
import javax.jms.MessageProducer;
import javax.jms.ObjectMessage;
import javax.jms.Queue;
import javax.jms.QueueConnectionFactory;
import javax.jms.Session;

/**
 *
 * @author Hubiquitos
 */
@WebService(serviceName = "GenService")
@Stateless()
public class GenService {

    Connection cnx;
    int count = 10000;
    @Resource(mappedName = "jms/queueCF")
    private QueueConnectionFactory factory;
    
    @Resource(mappedName = "jms/DocumentQueue")
    private Queue queue;
    
    //ouverture de la connexion dans une méthode d'écoute du cycle de vie (évènement
    //PostConstruct)
    @PostConstruct
    protected void init(){
    try {
    //connexion au provider JMS établie
    cnx = factory.createConnection();//connexion au JMS provider établie
        } catch (JMSException ex) {
        Logger.getLogger(GenService.class.getName()).log(Level.SEVERE, null, ex);
        throw new EJBException();
        }
    }
    
    //fermeture de la connexion dans une méthode déclenchée lorsque l'instance du session
    //bean est détruite
    @PreDestroy
    protected void clear(){
    try {
    cnx.close(); //il faut fermer la connexion
    } catch (JMSException ex) {
        Logger.getLogger(GenService.class.getName()).log(Level.SEVERE, null, ex);
        throw new EJBException();
        }
    }
    
    @WebMethod(operationName = "SendDocumentOperation")
    @TransactionAttribute(TransactionAttributeType.REQUIRES_NEW)
    public boolean processSendDocument(@WebParam(name = "name") String name, @WebParam(name = "content") String content, @WebParam(name = "key") String key)
    {
        //System.out.println(name +" envoyé avec succès !");
        System.out.println("test code : " + count);
        count--;
        sendDocument(name, content, key);
        return true;
    }
    
    private void sendDocument(String name, String content, String key)
    {
        try
        {
            Session session = cnx.createSession(true, 0);
            MessageProducer producer = session.createProducer(queue);
            Document document = new Document(name, content, key);  
            ObjectMessage obj = session.createObjectMessage(document);
            producer.send(obj);
            producer.close();
            session.close();
        }
        catch(JMSException ex)
        {
            Logger.getLogger(GenService.class.getName()).log(Level.SEVERE, null, ex);
            throw new EJBException();
        }
    }
}
