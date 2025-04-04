package com.example.toto;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

public class DatabaseAccess {

    private DatabaseOpenHelper openHelper; // On déclare un DatabaseOpenHelper
    private SQLiteDatabase db; // On crée un objet SQLite
    private static DatabaseAccess instance; // On crée une instance de la classe context
    Cursor c = null;

    // Constructeur privé pour éviter la création directe de l'instance
    private DatabaseAccess(Context context) {
        this.openHelper = new DatabaseOpenHelper(context);
    }

    // Méthode pour obtenir l'instance de DatabaseAccess
    public static DatabaseAccess getInstance(Context context) {
        if (instance == null) {
            instance = new DatabaseAccess(context);
        }
        return instance;
    }

    // Ouvre la connexion à la base de données
    public void open() {
        this.db = openHelper.getWritableDatabase();
    }

    // Ferme la connexion à la base de données
    public void close() {
        if (db != null) {
            this.db.close();
        }
    }

    // Méthode pour exécuter une requête SQL et obtenir les résultats
    public Cursor getData(String query) {
        c = db.rawQuery(query, null);
        return c;
    }
}
