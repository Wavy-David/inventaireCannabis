package com.example.toto;

public class HistoriqueModel {
    public int id;
    public String idPlante;
    public String action;
    public String date;
    public String champ;
    public String ancienneValeur;
    public String nouvelleValeur;

    public HistoriqueModel(int id, String idPlante, String action, String date, String champ, String ancienneValeur, String nouvelleValeur) {
        this.id = id;
        this.idPlante = idPlante;
        this.action = action;
        this.date = date;
        this.champ = champ;
        this.ancienneValeur = ancienneValeur;
        this.nouvelleValeur = nouvelleValeur;
    }
}
