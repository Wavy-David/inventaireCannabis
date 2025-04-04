package com.example.toto;

import android.content.Context;

import com.readystatesoftware.sqliteasset.SQLiteAssetHelper;
//creer une variable avec le nom de la bd
public class DatabaseOpenHelper extends SQLiteAssetHelper {
    private static final String DATABASE_NAME="HP-ELITEBOOK\\TEW_SQLEXPRESS";
    //constructeur
    public DatabaseOpenHelper(Context context) {
        super(context, DATABASE_NAME, null, 1);
    }
    //methode
}
