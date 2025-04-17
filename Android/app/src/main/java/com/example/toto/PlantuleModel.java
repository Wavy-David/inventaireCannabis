package com.example.toto;

import java.util.Date;

public class PlantuleModel {
    private String idPlante;
    private String etatSante;
    private String dateAjout;
    private String provenance;
    private String description;
    private String stade;
    private String entreposage;
    private int active_Inactive;
    private String itemRetireInventaire;
    private String note;
    // private String codeQr; // Uncomment if needed
    private String responsable;

    // Add constructors, getters, and setters as needed

    public PlantuleModel(String idPlante, String responsable, String note, String itemRetireInventaire, int active_Inactive, String stade, String description, String provenance, String dateAjout, String etatSante, String entreposage) {
        this.idPlante = idPlante;
        this.responsable = responsable;
        this.note = note;
        this.itemRetireInventaire = itemRetireInventaire;
        this.active_Inactive = active_Inactive;
        this.stade = stade;
        this.description = description;
        this.provenance = provenance;
        this.dateAjout = dateAjout;
        this.etatSante = etatSante;
        this.entreposage = entreposage;
    }

    public PlantuleModel()
    {
    }

    //ToString() =========================================================================================================
    @Override
    public String toString() {
        return "PlantuleModel{" +
                "idPlante='" + idPlante + '\'' +
                ", etatSante='" + etatSante + '\'' +
                ", dateAjout=" + dateAjout +
                ", provenance='" + provenance + '\'' +
                ", description='" + description + '\'' +
                ", stade='" + stade + '\'' +
                ", entreposage='" + entreposage + '\'' +
                ", active_Inactive=" + active_Inactive +
                ", itemRetireInventaire='" + itemRetireInventaire + '\'' +
                ", note='" + note + '\'' +
                ", responsable='" + responsable + '\'' +
                '}';
    }

    //Getter and Setter ===================================================================================================
    public String getIdPlante() {
        return idPlante;
    }

    public void setIdPlante(String idPlante) {
        this.idPlante = idPlante;
    }

    public String getEtatSante() {
        return etatSante;
    }

    public void setEtatSante(String etatSante) {
        this.etatSante = etatSante;
    }

    public String getDateAjout() {
        return dateAjout;
    }

    public void setDateAjout(String dateAjout) {
        this.dateAjout = dateAjout;
    }

    public String getProvenance() {
        return provenance;
    }

    public void setProvenance(String provenance) {
        this.provenance = provenance;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getStade() {
        return stade;
    }

    public void setStade(String stade) {
        this.stade = stade;
    }

    public String getEntreposage() {
        return entreposage;
    }

    public void setEntreposage(String entreposage) {
        this.entreposage = entreposage;
    }

    public int getActive_Inactive() {
        return active_Inactive;
    }

    public void setActive_Inactive(int active_Inactive) {
        this.active_Inactive = active_Inactive;
    }

    public String getItemRetireInventaire() {
        return itemRetireInventaire;
    }

    public void setItemRetireInventaire(String itemRetireInventaire) {
        this.itemRetireInventaire = itemRetireInventaire;
    }

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public String getResponsable() {
        return responsable;
    }

    public void setResponsable(String responsable) {
        this.responsable = responsable;
    }
}
