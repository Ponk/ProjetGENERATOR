/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package com.gen.data.cad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;


/**
 *
 * @author Hubiquitos
 */
public class CAD {

    private String Bdd;
    private String User;
    private String Password;
    
    public static ResultSet rs;
    public static Connection connect;
    
    //Chaine = "jdbc:mysql://localhost/dictionnairebdd", "root", "";
    public CAD(String Bdd, String User, String Password)
    {
        this.Bdd = Bdd;
        this.User = User;
        this.Password = Password;
    }
    
    private void LoadDriver() throws ClassNotFoundException
    {
        Class.forName("com.mysql.jdbc.Driver");
    }
    
    public Connection OpenConnection() throws SQLException, ClassNotFoundException
    {
        LoadDriver();
        connect = DriverManager.getConnection(Bdd, User, Password);
        return connect;
    }
    
    public void CloseConnection() throws SQLException
    {
        connect.close();
    }    
}
